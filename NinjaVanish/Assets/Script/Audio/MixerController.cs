using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    public Slider MusicSlider;
    public Slider SFXslider;
    public Slider IntensitySlider;
    public string Name_Dir_Light = "Directional Light";

    private string Music_Volume_Pref = "Music_Volume";
    private string SFX_Pref = "SFX_Volume";
    private string Light_Intensity_Pref = "Light_Intensity";

    private void Awake()
    {
        //Retrieve saved volume preferences
        float sav_MusicVol = PlayerPrefs.GetFloat(Music_Volume_Pref, GetMusicVolLevel());
        float sav_SFX = PlayerPrefs.GetFloat(SFX_Pref, GetSFXLevel());
        float sav_LightIntensity = PlayerPrefs.GetFloat(Light_Intensity_Pref, 1);

        Debug.Log("Going to set the slider to" + sav_MusicVol + " Which in dB is: " + Mathf.Log10(sav_MusicVol) * 20);

        //Set slider initial positions
        MusicSlider.value = sav_MusicVol;
        SFXslider.value = sav_SFX;
        IntensitySlider.value = sav_LightIntensity;

        //Set the internal volumes 
        SetMusicVolume(sav_MusicVol);
        SetSFXVolume(sav_SFX);
        SetLightIntensity(sav_LightIntensity);

    }
    private float GetMusicVolLevel()
    {
        //Get music volume from mixer
        float value;
        bool result = myAudioMixer.GetFloat("Music", out value);
        if (result)
        {
            Debug.Log("Retreived dB" + value + " which in float is" + Mathf.Pow(10, (value / 20)));
            return Mathf.Pow(10, (value/20));
        }
        else
        {
            return 0.5f;
        }
    }

    private float GetSFXLevel()
    {
        //Get SFX volume from mixer
        float value;
        bool result = myAudioMixer.GetFloat("SoundEffects", out value);
        if (result)
        {
            return Mathf.Pow(10, value / 20);
        }
        else
        {
            return 0.5f;
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(Music_Volume_Pref, sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("SoundEffects", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(SFX_Pref, sliderValue);
    }

    public void SetLightIntensity(float sliderValue)
    {
        PlayerPrefs.SetFloat(Light_Intensity_Pref, sliderValue);
        try
        {
            GameObject.Find("/" + Name_Dir_Light).GetComponent<Light>().intensity = sliderValue;
        }
        catch { }
    }
}
