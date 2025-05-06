using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum SliderKind
    {
        Music,Effect
    }
    Slider slider;
    [SerializeField] SliderKind kind;
    float value;
    [SerializeField]bool dodat = false;
    public void Setup()
    {
        slider = GetComponent<Slider>();
        switch (kind)
        {
            case SliderKind.Effect:
                AudioManager.instance.mixer.GetFloat("EffectVol", out value);
                break;
            case SliderKind.Music:
                AudioManager.instance.mixer.GetFloat("MusicVol", out value);
                break;

        }
        slider.value = DBToNormalized(value);
        dodat = true;
    }
    float DBToNormalized(float dB)
    {
        return Mathf.Pow(10, dB / 20f);
    }
    public void OnValueChange()
    {
        if (!dodat) return;
        switch (kind)
        {
            case SliderKind.Music:
                AudioManager.instance.MusicVolumeChange(slider.value);
                break;
            case SliderKind.Effect:
                AudioManager.instance.EffectVolumeChange(slider.value);
                break;
        }
    }
}
