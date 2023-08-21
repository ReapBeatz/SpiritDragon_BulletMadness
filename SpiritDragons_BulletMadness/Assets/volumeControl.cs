using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeControl : MonoBehaviour
{
    [SerializeField] string volumeName = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float multiplier = 30f;
    [SerializeField] Toggle toggle;
    bool disableToggle;
    private void Awake()
    {
        slider.onValueChanged.AddListener(valueChange);
        toggle.onValueChanged.AddListener(toggleChange);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeName, slider.value);
    }

    void valueChange(float value)
    {
        mixer.SetFloat(volumeName, Mathf.Log10(value) * multiplier);
        disableToggle = true;
        toggle.isOn = slider.value > slider.minValue;
        disableToggle = false;
    }

    void toggleChange(bool enableSound)
    {
        if (disableToggle)
        {
            return;
        }
        if (enableSound)
        {
            slider.value = slider.maxValue;
        }
        else 
        {
            slider.value = slider.minValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeName, slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
