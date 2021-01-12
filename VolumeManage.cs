using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManage : MonoBehaviour
{
    public AudioMixer masterMixer;

    

    public void SetSFXLvl(float sfxLvl)
    {
        masterMixer.SetFloat("SFXVol", sfxLvl);
    }

    public void SetMusicLvl(float sliderValue)
    {
        masterMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
