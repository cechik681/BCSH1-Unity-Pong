using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI fileInfo;
    public TMP_Dropdown resolutionDrop;
    public TMP_Dropdown playerDrop;

    //default level 1
    public static int chosenLevel = 1;

    public static bool AI = true;
    private Resolution[] resolutions;
    private static int usedResolutionIndex = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupResolutionsDropdown();
        SetupPlayerDropdown();

        fileInfo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPlayerDropdown()
    {
        List<string> options = new List<string> {"1 Player VS PC", "2 Players"};
        playerDrop.AddOptions(options);
    }

    public void SetupResolutionsDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDrop.ClearOptions();

        List<string> options = new List<string>();
        List<Resolution> uniqueResolutions = new List<Resolution>();
        int currentResolutionIndex = 0;
        int resWidth, resHeight;
        float sixteenToNine = 16f/9f; //1.7777777
        float currentAspectRatio;

        //loop through every resolution monitor supports
        for (int i = 0; i < resolutions.Length; i++)
        {
            resWidth = resolutions[i].width;
            resHeight = resolutions[i].height;
            currentAspectRatio = (float)resWidth/(float)resHeight;    //to show only 16:9
            
            string option = resWidth + "x" + resHeight;
            //check for unique resolutions AND aspect ratio must be 16:9;
            if ((!options.Contains(option)) && (sixteenToNine == currentAspectRatio))
            {
                options.Add(option);
                uniqueResolutions.Add(resolutions[i]);
                //default resolution is 720p
                if (resWidth == 1280 &&
                resHeight == 720)
                {
                    currentResolutionIndex = uniqueResolutions.Count-1;
                }
            }
        }
        resolutions = uniqueResolutions.ToArray();

        resolutionDrop.AddOptions(options);

        //set last used resolution
        if (usedResolutionIndex != -1)
        {
            resolutionDrop.value = usedResolutionIndex;
            SetResolution(usedResolutionIndex);
        }
        else    //otherwise set default (720p)
        {
            SetResolution(currentResolutionIndex);
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        usedResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void PlayLevel(int level)
    {
        chosenLevel = level;
        //playerDrop.value: 0 -> 1Player    1 -> 2Players
        int numOfPlayers = playerDrop.value+1;
        AI = (numOfPlayers == 1) ? true : false;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenTxt()
    {
        string path = Application.persistentDataPath + "/MatchLog.txt";

        if (File.Exists(path))
        {
            Application.OpenURL("file://" + path);
        }
        else
        {
            //file doesn't exists
            ShowFileErrorText("No match has been played, so match log doesn't exist");
        }
    }
    public void DeleteTxt()
    {
        string path = Application.persistentDataPath + "/MatchLog.txt";

        if (File.Exists(path))
        {
            File.Delete(path);
            ShowFileErrorText("Match logs has been cleared");
        }
        else
        {
            ShowFileErrorText("Match logs are already empty");
        }
    }
    
    //helper methods for calling after X seconds
    void ShowFileErrorText(string inputText)
    {
        fileInfo.text = inputText;
        fileInfo.enabled = true;
        Invoke("HideFileErrorText", 2f);
    }
    void HideFileErrorText()
    {
        fileInfo.enabled = false;
    }
}
