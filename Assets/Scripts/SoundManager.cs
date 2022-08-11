/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    AudioClip ac; 
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeTextUI = null;
    public GameObject AudioManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<AudioClip>(); 
        LoadValues();
        DontDestroyOnLoad(AudioManager);
    }
    public void VolumeSlider(float volume)
    {
        volume = volume * 100;
        volume = (float)Math.Round(volume);
        volumeTextUI.text = volume.ToString();
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/