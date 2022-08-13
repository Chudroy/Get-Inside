using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeText : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    Text text;
    public bool fadingOut = false;
    public bool fadingIn = false;
    float a;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        SetAlphaForFade();
    }

    // Update is called once per frame
    void Update()
    {
        FlipFlopFadeBools();
        ClampAlpha();
        FadeIn();
        FadeOut();
    }

     void FadeIn()
    {
        if (fadingIn)
        {
            text.color = new Color32(255, 255, 255, (byte)a);
            a -= Time.deltaTime * fadeSpeed;
        }
    }

    void FadeOut()
    {
        if (fadingOut)
        {
            text.color = new Color32(255, 255, 255, (byte)a);
            a += Time.deltaTime * fadeSpeed;
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
    void ClampAlpha()
    {
        a = Mathf.Clamp(a, 0, 255);
    }
}
