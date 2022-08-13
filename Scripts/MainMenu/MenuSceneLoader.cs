using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSceneLoader : MonoBehaviour
{
    const string savedGameIndex = "saveGameIndex";

    // Start is called before the first frame update
    public void NewGame()
    {
        PlayerPrefs.SetFloat(savedGameIndex, 0);
        SceneManager.LoadScene(8);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat(savedGameIndex, SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGame()
    {
        var loadSceneIdx = PlayerPrefs.GetFloat(savedGameIndex, 0);
        SceneManager.LoadScene((int)loadSceneIdx);
    }
   
    public void LoadMainMenu()
    {
        Destroy(GameHandler.Instance.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
