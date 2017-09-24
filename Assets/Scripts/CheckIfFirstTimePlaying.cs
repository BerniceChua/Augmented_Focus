using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckIfFirstTimePlaying : MonoBehaviour {

    [SerializeField] GameObject m_instructionsPanel;
    [SerializeField] GameObject m_introPanel;

    string m_didGameRunBefore = "autoStartTutorial";

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey(m_didGameRunBefore)) {
            PlayerPrefs.SetInt(m_didGameRunBefore, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt(m_didGameRunBefore) == 0) {
            AutoStartTheTutorial();
        }
	}

    public void AutoStartTheTutorial() {
        //if (m_introPanel.activeInHierarchy)
        //    m_introPanel.SetActive(false);

        m_instructionsPanel.SetActive(true);
    }

    public void SetFlagThatFirstPlayWasComplete() {
        PlayerPrefs.SetInt(m_didGameRunBefore, 1);

        PlayerPrefs.Save();
    }

    public void ResetFlagThatFirstPlayWasComplete() {
        PlayerPrefs.SetInt(m_didGameRunBefore, 0);

        PlayerPrefs.Save();
    }

    public void StartMainGame() {
        //if (PlayerPrefs.GetInt(m_didGameRunBefore) == 0 && m_instructionsPanel.activeInHierarchy)
        //    m_instructionsPanel.SetActive(false);

        m_introPanel.SetActive(true);
    }

}