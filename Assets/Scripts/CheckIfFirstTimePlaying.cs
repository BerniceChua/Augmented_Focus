using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckIfFirstTimePlaying : MonoBehaviour {

    string m_didGameRunBefore = "autoStartTutorial";
    string m_didBumbleBeeGetUnlocked = "unlockBumbleBee";

    // Use this for initialization
    void Start () {
        if (!PlayerPrefs.HasKey(m_didGameRunBefore)) {
            PlayerPrefs.SetInt(m_didGameRunBefore, 0);
        }

        if (!PlayerPrefs.HasKey(m_didBumbleBeeGetUnlocked)) {
            PlayerPrefs.SetInt(m_didBumbleBeeGetUnlocked, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt(m_didGameRunBefore) == 0) {
            
        }
	}

    public void SetFlagThatFirstPlayWasComplete() {
        PlayerPrefs.SetInt(m_didGameRunBefore, 1);

        PlayerPrefs.Save();
    }

    public void ResetFlagThatFirstPlayWasComplete() {
        PlayerPrefs.SetInt(m_didGameRunBefore, 0);

        PlayerPrefs.SetInt(m_didBumbleBeeGetUnlocked, 0);

        PlayerPrefs.Save();
    }

}