using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningEnemyAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] bool alwaysPlaySounds;
    [SerializeField] AudioClip scanClip;
    [SerializeField] float scanVolume;
    [SerializeField] AudioClip detectedClip;
    [SerializeField] float detectedVolume;
    [SerializeField] AudioClip stopScanningClip;
    [SerializeField] float stopScanningVolume;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    public void PlayScanSound(bool playSound, bool looping = false)
    {

        var r = GetRandomFloat();
        if ((r < 0.5f || alwaysPlaySounds) & playSound)
        {
            audioSource.clip = scanClip;
            audioSource.volume = scanVolume;
            audioSource.Play();
        }
        else if ((r < 0.5f || alwaysPlaySounds) & playSound)
        {
            audioSource.PlayOneShot(scanClip, scanVolume);
        }
        if (looping)
        {
            audioSource.loop = true;
        }
        else if (!looping)
        {
            audioSource.loop = false;
        }
    }

    public void PlayDetectedSound(bool playSound, bool playOneShot = false)
    {
        var r = GetRandomFloat();
        if ((r < 0.5f || alwaysPlaySounds) & !playOneShot)
        {
            audioSource.clip = detectedClip;
            audioSource.volume = detectedVolume;
            audioSource.Play();
        }
        else if ((r < 0.5f || alwaysPlaySounds))
        {
            audioSource.PlayOneShot(detectedClip, detectedVolume);
        }
    }

    public void PlayStopScanningSound()
    {
        var r = GetRandomFloat();
        if ((r < 0.5f || alwaysPlaySounds))
        {
            audioSource.clip = stopScanningClip;
            audioSource.volume = stopScanningVolume;
            audioSource.Play();
        }
    }
    float GetRandomFloat()
    {
        float f = Random.Range(0f, 1f);
        return f;
    }
}
