using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TorchUpdater : MonoBehaviour
{

    Image image;
    Color32 greyedOut = new Color32(50, 50, 50, 255);
    Color32 defaultColour = new Color32(255, 255, 255, 255);
    int r;
    int g;
    int b;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTorchSprite();
    }

    void UpdateTorchSprite()
    {
        if (!FindObjectOfType<Torch>().recharged)
        {
            r = 50; g = 50; b = 50;
            image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
        }
        else
        {
            r += 1;
            g += 1;
            b += 1;

            r = Mathf.Clamp(r, 0, 255);
            g = Mathf.Clamp(g, 0, 255);
            b = Mathf.Clamp(b, 0, 255);

            image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
        }
    }
}
