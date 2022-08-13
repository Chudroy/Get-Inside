using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressMarker : MonoBehaviour
{
    [SerializeField] float fadeOutSpeed = 0; 
    int buildIdx;
    Text progressText;
    string levelProgress;
    float a = 255;
    // Start is called before the first frame update
    void Start()
    {
        buildIdx = SceneManager.GetActiveScene().buildIndex;
        levelProgress = String.Format("{0}/5", buildIdx.ToString());
        progressText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        progressText.text = levelProgress;
    }

    // Update is called once per frame
    void Update()
    {
        FadeOut();
    }

    void FadeOut()
    {
        progressText.color = new Color32(255, 255, 255, (byte)a);
        a -= Time.deltaTime * fadeOutSpeed;
        a = Mathf.Clamp(a, 0, 255);
        if(a<1)
        {
            // Destroy(gameObject);
            // Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
