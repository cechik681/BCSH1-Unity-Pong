using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    //default level 1
    public static int chosenLevel = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressingEsc = Keyboard.current.escapeKey.isPressed;
        if (isPressingEsc)
        {
            Debug.Log("Escaping game");
            Application.Quit();
        }
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
