using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumePreferences : MonoBehaviour {

    [SerializeField] AudioSource m_audioSource;
    [SerializeField] Slider m_volumeSlider;
    [SerializeField] GameObject m_muteButton;
    [SerializeField] GameObject m_unmuteButton;

    string m_volume = "volume";
    string m_volumeSliderPosition = "volumeSliderPosition";

    string m_volumeMuteOrNot = "volumeMuteOrNot";

    string m_muteButtonActive = "muteButtonActive";
    string m_unmuteButtonActive = "unmuteButtonActive";
    string m_muteCheckbox = "muteCheckbox";
    string m_soundPlaying = "isPlaying";

    //bool m_isMuteButtonActive;
    //bool m_isUnmuteButtonActive;
    //bool m_isMuteCheckboxed;
    //bool m_isAudioPlaying;

    private void Awake() {
        if (!PlayerPrefs.HasKey(m_volume)) {
            PlayerPrefs.SetFloat(m_volume, 1.0f);
        }

        LoadVolumeSettings();
    }

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {

    }

    public void SaveVolumeSettings() {
        PlayerPrefs.SetFloat(m_volume, m_audioSource.volume);
        PlayerPrefs.SetFloat(m_volumeSliderPosition, m_volumeSlider.value);

        //if (m_muteButton.activeInHierarchy && m_unmuteButton.activeInHierarchy == false) {
        //    PlayerPrefs.SetInt(m_volumeMuteOrNot, 1);
        //} else if (m_muteButton.activeInHierarchy == false && m_unmuteButton.activeInHierarchy) {
        //    PlayerPrefs.SetInt(m_volumeMuteOrNot, 0);
        //    //PlayerPrefs.SetInt(m_volumeMuteOrNot, m_volumeSlider.interactable);
        //}
        //PlayerPrefs.SetInt(m_muteButtonActive, Convert.ToInt32(m_muteButton.activeInHierarchy));
        //PlayerPrefs.SetInt(m_unmuteButtonActive, Convert.ToInt32(m_unmuteButton.activeInHierarchy));
        //PlayerPrefs.SetInt(m_muteCheckbox, Convert.ToInt32(m_audioSource.mute));
        //PlayerPrefs.SetInt(m_soundPlaying, Convert.ToInt32(m_audioSource.isPlaying));

        PlayerPrefs.Save();
    }

    public void LoadVolumeSettings() {
        m_audioSource.volume = PlayerPrefs.GetFloat(m_volume);
        m_volumeSlider.value = PlayerPrefs.GetFloat(m_volumeSliderPosition);

        //if (PlayerPrefs.GetInt(m_volumeMuteOrNot) == 1) {
        //    m_muteButton.SetActive(false);
        //    m_unmuteButton.SetActive(true);
        //    m_audioSource.mute = true;
        //    m_audioSource.Stop();
        //    m_volumeSlider.interactable = false;
        //} else if (PlayerPrefs.GetInt(m_volumeMuteOrNot) == 0) {
        //    print("I'm in the else of LoadVolumeSettings().");
        //    m_muteButton.SetActive(true);
        //    m_unmuteButton.SetActive(false);
        //    m_audioSource.mute = false;
        //    m_audioSource.Play();
        //    m_volumeSlider.interactable = true;
        //}
        //m_muteButton.enab = Convert.ToBoolean(PlayerPrefs.GetInt(m_muteButtonActive));
        //m_unmuteButton.SetActive(Convert.ToBoolean(PlayerPrefs.GetInt(m_unmuteButtonActive)));
        //m_audioSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(m_muteCheckbox));
        //m_audioSource.enabled = Convert.ToBoolean(PlayerPrefs.GetInt(m_soundPlaying));
    }

    public void ResetVolumeSettings() {
        PlayerPrefs.DeleteAll();
    }

}