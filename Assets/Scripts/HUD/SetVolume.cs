using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public AudioMixer voiceMixer;
    [Header("Mixer Volume")]
    public float musicVol = 0.03254725f;
    public float sfxVol = 0.14f;
    public float voiceVol = 0.1f;
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider voiceSlider;

    private void Start()
    {
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
        voiceSlider.value = voiceVol;

        SetLevel(musicVol);
        SetSfxVolume(sfxVol);
        SetVoiceVolume(voiceVol);
    }

    //Changes the volume for the master volume slider
    public void SetLevel(float sliderValue)
    {
        musicMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        musicVol = sliderValue;
    }

    //Changes the volume for the SFX volume slider
    public void SetSfxVolume(float sliderValue)
    {
        sfxMixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        sfxVol = sliderValue;
    }

    //Changes the volume for the voice audio volume slider
    public void SetVoiceVolume(float sliderValue)
    {
        voiceMixer.SetFloat("voiceVol", Mathf.Log10(sliderValue) * 20);
        voiceVol = sliderValue;
    }
}
