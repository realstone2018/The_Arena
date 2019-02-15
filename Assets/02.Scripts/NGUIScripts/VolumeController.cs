using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour {

    public UIToggle soundToggle;
    public UISlider slider;

    private void Awake()
    {
        slider = GetComponent<UISlider>();
        slider.value = NGUITools.soundVolume;

        if (NGUITools.soundVolume == 0f)
            soundToggle.value = false;
    }

    public void OnVolumeChange()
    {
        NGUITools.soundVolume = UISlider.current.value;
        AudioListener.volume = UISlider.current.value;
    }

    
    public void OnSoundToggle()
    {
        float newVolume = 0;

        if (UIToggle.current.value)
            newVolume = slider.value;
 
        NGUITools.soundVolume = newVolume;
        AudioListener.volume = newVolume;
    }
}
