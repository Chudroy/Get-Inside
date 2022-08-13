using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeImage : MonoBehaviour
{
    Image image;
    [SerializeField] float fadeSpeed;
    [SerializeField] bool LoadWinScreen;
    public bool fadingOut = false;
    public bool fadingIn = false;
    float a;
    void Start()
    {
        image = GetComponent<Image>();
        SetAlphaForFade();
    }

    // Update is called once per frame
    void Update()
    {
        FlipFlopFadeBools();
        FadeIn();
        FadeOut();
        ClampAlpha();
    }
    void FadeIn()
    {
        if (fadingIn)
        {
            image.color = new Color32(0, 0, 0, (byte)a);
            a -= Time.deltaTime * fadeSpeed;
        }
    }

    void FadeOut()
    {
        if (fadingOut)
        {
            image.color = new Color32(0, 0, 0, (byte)a);
            a += Time.deltaTime * fadeSpeed;
        }

        if (a >= 255 & LoadWinScreen)
        {
            FindObjectOfType<LevelLoader>().LoadWinScreen();
            Destroy(FindObjectOfType<GameHandler>().gameObject);
        }
    }
    void ClampAlpha()
    {
        a = Mathf.Clamp(a, 0, 255);
    }

    void FlipFlopFadeBools()
    {
        if (fadingIn == true)
        {
            fadingOut = false;
        }
        else if (fadingOut == true)
        {
            fadingIn = false;
        }

        if (fadingIn & fadingOut)
        {
            Debug.LogError("fading In and Out cannot be both true");
            fadingIn = false;
            fadingOut = false;
        }
    }

    void SetAlphaForFade()
    {
        if (fadingIn)
        {
            a = 255f;
        }
        else if (fadingOut)
        {
            a = 0f;
        }
    }
}
