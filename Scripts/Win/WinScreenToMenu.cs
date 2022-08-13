using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScreenToMenu : MonoBehaviour
{
    [SerializeField] GameObject returnToMenuText;
    bool returnToMenu;
    void Start()
    {
        StartCoroutine(ActivateClickToContinue());
    }

    void Update()
    {
        BackToMainMenu();
    }

    void BackToMainMenu()
    {
        if (returnToMenu & Input.GetMouseButtonDown(0))
        {
            Destroy(FindObjectOfType<MusicManager>().gameObject);
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator ActivateClickToContinue()
    {
        yield return new WaitForSeconds(9f);
        returnToMenu = true;
        returnToMenuText.SetActive(true);
    }
}
