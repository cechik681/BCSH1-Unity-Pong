using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int chosenLevel = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel(int level)
    {
        chosenLevel = level;
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
            Debug.Log("opening");
        }
    }
    public void DeleteTxt()
    {
        string path = Application.persistentDataPath + "/MatchLog.txt";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("deleted");
        }
        else
        {
            Debug.Log("something else");
        }
    }
}
