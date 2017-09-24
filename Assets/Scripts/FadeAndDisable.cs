using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndDisable : MonoBehaviour {

    [SerializeField] GameObject m_panelForFading;
    [SerializeField] CanvasGroup m_titleToFade;

    float m_progress = 0.0f;
    bool m_fadeIn = true;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("FadeIn", 0.05f, 20);

        StartCoroutine(FadeIn());
    }
	
	// Update is called once per frame
	void Update() {
        //if (m_fadeIn) {
        //    m_progress += Time.deltaTime * 0.25f;

        //    if (m_progress >= 1.0f) {
        //        m_progress = 1.0f;
        //        m_fadeIn = false;
        //        m_panelForFading.SetActive(false);
        //    }
        //    m_titleToFade.alpha = Mathf.Lerp(0.0f, 1.0f, m_progress);
        //}
    }

    IEnumerator FadeIn() {
        //m_progress += 0.05f;
        //m_titleToFade.alpha = Mathf.Lerp(0.0f, 1.0f, m_progress);

        yield return new WaitForSecondsRealtime(3);

        if (m_fadeIn) {
            m_progress += Time.deltaTime * 0.25f;

            if (m_progress >= 1.0f) {
                m_progress = 1.0f;
                m_fadeIn = false;
                m_panelForFading.SetActive(false);
            }
            m_titleToFade.alpha = Mathf.Lerp(0.0f, 1.0f, m_progress);
        }

    }

}
