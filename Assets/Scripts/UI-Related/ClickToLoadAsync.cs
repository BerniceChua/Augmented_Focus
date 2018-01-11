using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider m_LoadingBar;
    public GameObject m_LoadingImage;

    private AsyncOperation m_async;

    public void ClickAsync(Scene sceneToLoad) {
        m_LoadingImage.SetActive(true);
        StartCoroutine( LoadLevelWithProgressBar(sceneToLoad) );
    }

    IEnumerator LoadLevelWithProgressBar(Scene sceneToLoad) {
        m_async = SceneManager.LoadSceneAsync(sceneToLoad.name.ToString());

        while (!m_async.isDone) {
            m_LoadingBar.value = m_async.progress;
            yield return null;
        }
    }

}
