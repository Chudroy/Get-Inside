using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] bool alwaysPlaySounds;
    [SerializeField] AudioClip torchWaveClip;
    [SerializeField] float torchWaveVolume;
    public bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        FadeOut();
    }
    public void PlayTorchWaveSound()
    {
        var r = GetRandomFloat();
        if ((r < 0.5f || alwaysPlaySounds))
        {
            audioSource.volume = torchWaveVolume;
            audioSource.clip = torchWaveClip;
            audioSource.Play();
        }
    }
    public void StopAudio()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    float GetRandomFloat()
    {
        float f = Random.Range(0f, 1f);
        return f;
    }

    void FadeOut()
    {
        if(fadeOut)
        {
            audioSource.volume -= 0.4f * Time.deltaTime;
        }

        if(audioSource.volume <= 0)
        {
            fadeOut = false;
            StopAudio();
        }
    }
}
