using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSparklesBasedOnTimer : MonoBehaviour {

    [SerializeField] TimeElapsed m_timeElapsed;

    [SerializeField] DetectIfGamePieceLeavesScreenView m_detectGameOver;

    [SerializeField] Orbit m_orbit;

    [SerializeField] float m_timeInterval = 10;

    [SerializeField] GameObject m_imagesContainer;
    private Transform m_imagesContainerTransform;
    [SerializeField] GameObject[] m_imagesArray;

    private int m_index;


    [SerializeField] GameObject m_sparkleImagePrefab;
    [SerializeField] Sprite[] m_sparkleSpritesArray;


    GameObject m_sparkleInstance;

    // Use this for initialization
    void Start () {
        print("m_sparkleSpritesArray.Length = " + m_sparkleSpritesArray.Length);
    }
	
	// Update is called once per frame
	void Update () {
        CalculateTimeInterval();
    }

    public void GetRandomSparkleSprite() {
        int arrayIndex = Random.Range(0, m_sparkleSpritesArray.Length);
        Sprite sparkleSprite = m_sparkleSpritesArray[arrayIndex];
        string sparkleSpriteName = sparkleSprite.name;

        //sparkleInstance = Instantiate(m_sparkleImagePrefab);
        m_sparkleInstance = Instantiate(m_sparkleImagePrefab, m_imagesContainerTransform, false);
        m_sparkleInstance.transform.SetParent(m_imagesContainerTransform, false);
        m_sparkleInstance.GetComponent<RectTransform>().position = new Vector3(7.629395e-06f, -2.288818e-05f, 0);
        m_sparkleInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        m_sparkleInstance.name = sparkleSpriteName;
        m_sparkleInstance.GetComponent<SparkleSpriteImage>().m_sparkleSpriteName = sparkleSpriteName;
        m_sparkleInstance.GetComponent<SpriteRenderer>().sprite = sparkleSprite;
        //sparkleInstance.GetComponent<Image>().sprite = sparkleSprite;

        print("Inside 'GetRandomSparkleSprite()' function.");
    }

    public float GetTime() {
        Debug.Log("m_timeElapsed.Timer() = " + m_timeElapsed.Timer());
        return m_timeElapsed.Timer();
    }

    public void CalculateTimeInterval() {
        if (m_detectGameOver.enabled == true) {
            if (GetTime() >= 1.0f) {
                if (Mathf.Floor(GetTime()) % m_timeInterval == 0) {
                    Debug.Log("Mathf.Floor(GetTime()) = " + Mathf.Floor(GetTime()));

                    StartCoroutine(AddSparkles());
                }
            }
        }
    }

    IEnumerator AddSparkles() {
        print("Inside 'AddSparkles()' IEnumerator.");
        m_index = Random.Range(0, m_sparkleSpritesArray.Length);
        Sprite sparkleSprite = m_sparkleSpritesArray[m_index];

        //sparkleInstance.GetComponent<SpriteRenderer>().enabled = true;
        //GetRandomSparkleSprite();
        //yield return new WaitForSecondsRealtime(3);
        //sparkleInstance.GetComponent<SpriteRenderer>().enabled = false;

        //GetRandomSparkleSprite();
        //yield return new WaitForSecondsRealtime(3);
        //Destroy(m_sparkleInstance);

        print("sparkleSprite.name = " + sparkleSprite.name);

        m_sparkleImagePrefab.SetActive(true);
        //m_sparkleImagePrefab.GetComponent<SpriteRenderer>().sprite = sparkleSprite;
        m_sparkleImagePrefab.GetComponent<Image>().sprite = sparkleSprite;
        yield return new WaitForSecondsRealtime(3);
        m_sparkleImagePrefab.SetActive(false);

        print("After the yield return new inside 'AddSparkles()' IEnumerator.");

    }
}
