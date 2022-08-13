using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameExplanation : MonoBehaviour
{
    [SerializeField] Text[] paragraphs;
    int paragraphIdx = 0;

    void Start()
    {
        foreach (Text p in paragraphs)
        {
            p.enabled = false;
        }
    }

    void Update()
    {
        OnClickEvent();
    }

    void OnClickEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!AllPragraphsVisible())
            {
                paragraphs[paragraphIdx].enabled = true;
                paragraphIdx += 1;
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    bool AllPragraphsVisible()
    {
        foreach (Text p in paragraphs)
        {
            if (!p.enabled)
            {
                return false;
            }
        }
        return true;
    }
}
