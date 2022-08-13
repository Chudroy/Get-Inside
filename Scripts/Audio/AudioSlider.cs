using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSlider : MonoBehaviour
{
    const string audioVolume = "audioVolume";
    MusicManager musicManager;
    Slider slider;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        musicManager = MusicManager.Instance;
        audioSource = musicManager.GetComponent<AudioSource>();
        slider.value = PlayerPrefs.GetFloat(audioVolume, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        SetMusicVolume();
    }

    void SetMusicVolume()
    {
        audioSource.volume = slider.value;
        PlayerPrefs.SetFloat(audioVolume, slider.value);
    }


}
