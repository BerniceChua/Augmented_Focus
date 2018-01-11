using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider m_LoadingBar;
    public GameObject m_LoadingImage;

    private AsyncOperation m_async;

    /// <summary>
    /// This has to be int, because Scene doesn't work in the UI.)s
    /// </summary>
    /// <param name="levelOfSceneToLoad"></param>
    public void ClickAsync(int levelOfSceneToLoad) {
        m_LoadingImage.SetActive(true);
        StartCoroutine( LoadLevelWithProgressBar(levelOfSceneToLoad) );
    }

    IEnumerator LoadLevelWithProgressBar(int levelOfSceneToLoad) {
        //m_async = SceneManager.LoadSceneAsync(sceneToLoad.name.ToString());
        m_async = SceneManager.LoadSceneAsync(levelOfSceneToLoad);

        while (!m_async.isDone) {
            m_LoadingBar.value = m_async.progress;
            yield return null;
        }
    }

}
