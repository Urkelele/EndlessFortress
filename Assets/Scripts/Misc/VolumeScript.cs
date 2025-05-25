using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider m_Slider;
    public float m_VolumeValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Slider.value = PlayerPrefs.GetFloat("AudioVolume", 0.5f);
        AudioListener.volume = m_Slider.value;
    }

    public void ChangeSlider(float value)
    {
        m_VolumeValue = value;
        PlayerPrefs.SetFloat("AudioVolume", m_VolumeValue);
        AudioListener.volume = m_Slider.value;
    }
}
