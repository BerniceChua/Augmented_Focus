using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void Intro() {
        SceneManager.LoadScene("Intro");
    }

    public void RelaxMode() {
        SceneManager.LoadScene("Relax_Mode");
    }
    public void FocusMode() {
        SceneManager.LoadScene("Augmented_Focus");
    }

    public void ChallengeMode() {
        SceneManager.LoadScene("Challenge_Mode");
    }

}
