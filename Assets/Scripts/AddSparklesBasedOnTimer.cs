using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    GameObject sparkleInstance;

    // Use this for initialization
    void Start () {
        print("m_imagesArray.Length = " + m_imagesArray.Length);
    }
	
	// Update is called once per frame
	void Update () {
        CalculateTimeInterval();
    }

    public void GetRandomSparkleSprite() {
        int arrayIndex = Random.Range(0, m_sparkleSpritesArray.Length);
        Sprite sparkleSprite = m_sparkleSpritesArray[arrayIndex];
        string sparkleSpriteName = sparkleSprite.name;

        sparkleInstance = Instantiate(m_sparkleImagePrefab);
        sparkleInstance.name = sparkleSpriteName;
        sparkleInstance.GetComponent<SparkleSpriteImage>().m_sparkleSpriteName = sparkleSpriteName;
        sparkleInstance.GetComponent<SpriteRenderer>().sprite = sparkleSprite;
    }

    public float GetTime() {
        //Debug.Log("m_timeElapsed.Timer() = " + m_timeElapsed.Timer());
        return m_timeElapsed.Timer();
    }

    public void CalculateTimeInterval() {
        if (m_detectGameOver.enabled == true) {
            if (GetTime() >= 1.0f) {
                if (Mathf.Floor(GetTime()) % m_timeInterval == 0) {
                    //Debug.Log("Mathf.Floor(GetTime()) = " + Mathf.Floor(GetTime()));

                    StartCoroutine(AddSparkles());
                }
            }
        }
    }

    IEnumerator AddSparkles() {
        print("Inside 'AddSparkles()' IEnumerator.");
        m_index = Random.Range(0, m_imagesArray.Length);

        //sparkleInstance.GetComponent<SpriteRenderer>().enabled = true;
        //GetRandomSparkleSprite();
        //yield return new WaitForSecondsRealtime(3);
        //sparkleInstance.GetComponent<SpriteRenderer>().enabled = false;

        GetRandomSparkleSprite();
        yield return new WaitForSecondsRealtime(3);
        Destroy(sparkleInstance);
    }
}
