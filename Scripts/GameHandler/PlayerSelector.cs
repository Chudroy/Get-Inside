using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerSelector : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    Camera mainCamera;
    [SerializeField] GameObject playerFlaregunPrefab;
    [SerializeField] GameObject playerAxePrefab;
    [SerializeField] GameObject playerRicochetPrefab;
    Player currentPlayer;
    int playerIdx;
    void Update()
    {
        PlayerSelectShortcuts();

    }

    void PlayerSelectShortcuts()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerIdx = 0;
            PlayerSelect();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            playerIdx = 1;
            PlayerSelect();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            playerIdx = 2;
            PlayerSelect();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerIdx++;
            PlayerSelect();

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            playerIdx--;
            PlayerSelect();
        }

        playerIdx = Mathf.Clamp(playerIdx, 0, 2);

    }

    void PlayerSelect()
    {
        if (playerIdx == 0)
        {
            OnPlayerAxeSelect();
        }
        else if (playerIdx == 1)
        {
            OnPlayerFlareGunSelect();
        }
        else if (playerIdx == 2)
        {
            OnPlayerRicochetGunSelect();
        }
    }

    public void OnPlayerFlareGunSelect()
    {

        currentPlayer = FindObjectOfType<Player>();
        if (currentPlayer != null)
        {
            FindCameras();
            if (currentPlayer.gameObject.transform.parent.tag != "Player Flaregun")
            {
                var newPlayer = Instantiate(playerFlaregunPrefab, currentPlayer.gameObject.transform.position, Quaternion.identity);
                cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
                newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;

                Destroy(currentPlayer.gameObject.transform.parent.gameObject);
                RefreshPlayerInstance();
                FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(0);
            }
        }
        else
        {
            FindCameras();
            Transform playerSpawnTransform = GameObject.Find("PlayerSpawnTransform").transform;
            var newPlayer = Instantiate(playerFlaregunPrefab, playerSpawnTransform.position, Quaternion.identity);
            cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
            newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;
            RefreshPlayerInstance();
            FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(0);
        }

        FindObjectOfType<PlayerManager>().GetCurrentPlayerType();
    }

    public void OnPlayerAxeSelect()
    {
        currentPlayer = FindObjectOfType<Player>();
        if (currentPlayer != null)
        {
            FindCameras();
            if (currentPlayer.gameObject.transform.parent.tag != "Player Axe")
            {
                var newPlayer = Instantiate(playerAxePrefab, currentPlayer.gameObject.transform.position, Quaternion.identity);
                cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
                newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;

                Destroy(currentPlayer.gameObject.transform.parent.gameObject);
                RefreshPlayerInstance();
                FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(1);
            }
        }
        else
        {
            FindCameras();
            Transform playerSpawnTransform = GameObject.Find("PlayerSpawnTransform").transform;
            var newPlayer = Instantiate(playerAxePrefab, playerSpawnTransform.position, Quaternion.identity);
            cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
            newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;
            RefreshPlayerInstance();
            GameHandler.Instance.transform.GetChild(4).GetChild(0).GetComponentInChildren<EquippedWeaponUpdater>().UpdateWeaponSprite(1);
            // FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(1);
        }

        FindObjectOfType<PlayerManager>().GetCurrentPlayerType();
    }

    public void OnPlayerRicochetGunSelect()
    {

        currentPlayer = FindObjectOfType<Player>();
        if (currentPlayer != null)
        {
            FindCameras();
            if (currentPlayer.gameObject.transform.parent.tag != "Player Ricochet Gun")
            {
                var newPlayer = Instantiate(playerRicochetPrefab, currentPlayer.gameObject.transform.position, Quaternion.identity);
                cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
                newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;

                Destroy(currentPlayer.gameObject.transform.parent.gameObject);
                RefreshPlayerInstance();
                FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(2);
            }
        }
        else
        {
            FindCameras();
            Transform playerSpawnTransform = GameObject.Find("PlayerSpawnTransform").transform;
            var newPlayer = Instantiate(playerRicochetPrefab, playerSpawnTransform.position, Quaternion.identity);
            cinemachineVirtualCamera.Follow = newPlayer.transform.GetChild(0).transform;
            newPlayer.transform.GetChild(0).GetComponent<Player>().cam = mainCamera;
            RefreshPlayerInstance();
            FindObjectOfType<EquippedWeaponUpdater>().UpdateWeaponSprite(2);
        }

        FindObjectOfType<PlayerManager>().GetCurrentPlayerType();
    }

    void FindCameras()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void RefreshPlayerInstance()
    {
        FindObjectOfType<GenericEnemyBehaviour>().FindInstanceOfPlayer();
    }
}
