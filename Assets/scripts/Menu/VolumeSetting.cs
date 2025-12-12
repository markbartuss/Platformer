using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{

    //This holds the slides
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masSlider;

    bool isMuted;

    public static VolumeSetting instance;

    //this is incase a mute buttom will be added it just makes sure the audio is turned off
    public void muteToggle(Toggle muted)
    {

        if (muted.isOn)
        {
            myMixer.SetFloat("Music", -80);
            isMuted = true;
        }
        else
        {
            LoadVolume();
            isMuted = false;
            SetMusicVolume();
        }
    }
    private void Start()
    {
        //this checks is the volume has been changed before if not its being set
        if (PlayerPrefs.HasKey("masVolume"))
        {
            LoadVolume1();
        }
        else
        {
            SetMasVolume();
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSfxVolume();
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }


        SetMusicVolume();
    }

    //this is what changes the volume when the slider is being dragged 
    //player prefs are what remember the previous volume that it was on and save it

    public void SetMasVolume()
    {
        float volume = masSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masVolume", volume);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetMusicVolume()
    {
        if (isMuted == false)
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }

    }

    //Loads the volume, this means that the volume will be remembered even if the application is closed
    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSfxVolume();

    }
    private void LoadVolume1()
    {
        masSlider.value = PlayerPrefs.GetFloat("masVolume");
        SetMasVolume();

    }

}