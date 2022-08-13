using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpriteBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float a = 255f;
    [SerializeField] float fadeSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeOut();
    }

    void FadeOut()
    {

        spriteRenderer.color = new Color32(255, 255, 255, (byte)a);
        a -= fadeSpeed;

        if(a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
