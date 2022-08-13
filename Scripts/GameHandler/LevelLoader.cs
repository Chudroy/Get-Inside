using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject restartText;
    PlayerResourceManager playerResourceManager;
    PlayerStatsManager playerStatsManager;
    PlayerManager playerManager;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameHandler.Instance.transform.GetComponentInChildren<PlayerManager>().LoadPlayer();
        // FindObjectOfType<PlayerManager>().LoadPlayer();
    }

    void Awake()
    {
        playerResourceManager = GameHandler.Instance.transform.GetComponentInChildren<PlayerResourceManager>();
        playerStatsManager = GameHandler.Instance.transform.GetComponentInChildren<PlayerStatsManager>();
        playerManager = GameHandler.Instance.transform.GetComponentInChildren<PlayerManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (FindObjectOfType<Player>() != null)
        {
            CheckRestart();
        }
    }

    void CheckRestart()
    {
        if (FindObjectOfType<Player>().isDead & Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void LoadNextLevel()
    {
        var idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx + 1);
        playerResourceManager.SaveResourceValues();
    }

    public void RestartLevel()
    {
        var idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx);
        restartText.SetActive(false);
        playerResourceManager.PlayerAmmoReset(SceneManager.GetActiveScene().buildIndex);
        playerResourceManager.PlayerHealthReset();
        playerStatsManager.PlayerStatsReset();

        // load flaregun and ricochetgun ammo recorded at start of level;
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(9);
    }

}
