using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleThroughImages : MonoBehaviour {

    [SerializeField] GameObject m_parentPanel;
    [SerializeField] float m_timeToWait = 3;

    GameObject[] m_imagesList;
    int m_index = 0;
    
    // Use this for initialization
    void Start () {
        m_imagesList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            m_imagesList[i] = transform.GetChild(i).gameObject;
            m_imagesList[i].SetActive(false);
            Debug.Log(m_imagesList[i]);
        }

        if (m_imagesList[m_index])
            m_imagesList[m_index].SetActive(true);

        Debug.Log(m_imagesList);
        Debug.Log(transform);
        Debug.Log(transform.childCount);

    }
	
	// Update is called once per frame
	void Update () {
        WhileThisSectionIsActive();
    }

    private void WhileThisSectionIsActive() {
        while (m_parentPanel.activeInHierarchy)
            NextImage();
    }

    private IEnumerator NextImage() {
        yield return new WaitForSecondsRealtime(m_timeToWait);
        Debug.Log("I'm at the next picture!");

        m_imagesList[m_index].SetActive(false);

        m_index++;

        if (m_index > m_imagesList.Length - 1)
            m_index = 0;

        m_imagesList[m_index].SetActive(true);
    }
}