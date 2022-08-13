using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] bool alwaysPlaySounds;
    [SerializeField] AudioClip spawnClip;
    [SerializeField] float spawnVolume;
    [SerializeField] AudioClip alertClip;
    [SerializeField] float alertVolume;
    [SerializeField] AudioClip attackClip;
    [SerializeField] float attackVolume;
    [SerializeField] AudioClip runAwayClip;
    [SerializeField] float runAwayVolume;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
    }

    public void EnemySpawnSound(Vector3 playerPos)
    {
        var r = GetRandomFloat();
        if (Vector3.Distance(playerPos, transform.position) < 10 & (r < 0.5f || alwaysPlaySounds))
        {
            audioSource.clip = spawnClip;
            audioSource.volume = spawnVolume;
            audioSource.Play();
        }
    }

    public void EnemyAlertSound(bool playSound, bool playOneShot = false)
    {
        var r = GetRandomFloat();
        if (playSound & (r < 0.5f || alwaysPlaySounds) & !playOneShot)
        {
            audioSource.clip = alertClip;
            audioSource.volume = alertVolume;
            audioSource.Play();
        }
        else if (playSound & (r < 0.5f || alwaysPlaySounds))
        {
            audioSource.PlayOneShot(alertClip, alertVolume);
        }
    }

    public void EnemyAttackSound(bool playOneShot = false, bool looping = false)
    {
        var r = GetRandomFloat();
        if ((r < 0.5f || alwaysPlaySounds) & !playOneShot)
        {
            audioSource.clip = attackClip;
            audioSource.volume = spawnVolume;
            audioSource.Play();
        }
        else if ((r < 0.5f || alwaysPlaySounds))
        {
            audioSource.PlayOneShot(attackClip, attackVolume);
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
    public void EnemyRunAwaySound(bool playSound)
    {
        var r = GetRandomFloat();
        if (playSound & (r < 0.5f || alwaysPlaySounds))
        {
            audioSource.clip = runAwayClip;
            audioSource.volume = runAwayVolume;
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
}
