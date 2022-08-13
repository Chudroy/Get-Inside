using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] bool alwaysPlaySounds;
    [SerializeField] AudioClip[] attackClip;
    [SerializeField] float attackVolume;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayerAttackSound()
    {
        if (attackClip.Length > 1)
        {
            PlayRandomSound(attackClip, attackVolume);
        }
        else
        {
            PlaySingle();
        }
    }
   
    void PlayRandomSound(AudioClip[] audioArr, float audioArrVol)
    {
        var rf = GetRandomFloat();
        var ri = GetRandomInt();

        if ((rf < 0.5f || alwaysPlaySounds))
        {
            audioSource.volume = audioArrVol;
            audioSource.clip = audioArr[ri];
            audioSource.Play();
        }
    }
    void PlaySingle()
    {
        var r = GetRandomFloat();

        if ((r < 0.5f || alwaysPlaySounds))
        {
            audioSource.volume = attackVolume;
            audioSource.clip = attackClip[0];
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
    int GetRandomInt()
    {
        int i = Random.Range(0, attackClip.Length);
        return i;
    }
}
