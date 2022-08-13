using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinHandler : MonoBehaviour
{
    [SerializeField] FadeImage fadeImage;
    [SerializeField] AudioClip fireplaceFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<MusicManager>().SetCrossFade(fireplaceFX);
        fadeImage.fadingOut = true;
    }
}
