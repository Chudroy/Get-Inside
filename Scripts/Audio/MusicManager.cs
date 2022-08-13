using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonClass<MusicManager>
{
    AudioSource audioSource;
    AudioClip crossFadeClip;
    float currentVolume;
    bool setting = true;
    bool fadingIn = false, fadingOut = true, finishedCrossFade = false;
    const string audioVolume = "audioVolume";
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(audioVolume, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ClampVolume();
        CrossFade();
    }

    void CheckIfNotPlaying()
    {
        if (audioSource.isPlaying == false)
        {
            Debug.Log("audio stopped");
            audioSource.Play();
        }
    }

    public void SetCrossFade(AudioClip newClip)
    {
        crossFadeClip = newClip;
    }

    void CrossFade()
    {
        if (crossFadeClip != null)
        {
            SetCurrentVolume();
            if (fadingOut)
            {
                FadeOut();
                if (audioSource.volume <= 0)
                {
                    fadingOut = false;
                    fadingIn = true;
                    audioSource.clip = crossFadeClip;
                    audioSource.Play();
                }
            }
            if (fadingIn)
            {
                FadeIn();
                if (audioSource.volume >= currentVolume)
                {
                    fadingIn = false;
                    finishedCrossFade = true;
                }
            }
            if (finishedCrossFade)
            {
                Debug.Log("finished");
                finishedCrossFade = false;
                crossFadeClip = null;
                fadingOut = true;
            }
        }
    }

    void FadeIn()
    {
        audioSource.volume += Time.deltaTime * 0.2f;
    }
    void FadeOut()
    {
        audioSource.volume -= Time.deltaTime * 0.2f;
    }

    void SetCurrentVolume()
    {
        if (setting)
        {
            currentVolume = audioSource.volume;
        }

        setting = false;
    }

    void ClampVolume()
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume, 0f, 1f);
    }
}
