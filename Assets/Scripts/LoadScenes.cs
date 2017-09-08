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

    public void ChallengeMode() {
        SceneManager.LoadScene("Augmented_Focus");
    }

    public void RelaxMode() {
        SceneManager.LoadScene("Relax_Mode");
    }

}