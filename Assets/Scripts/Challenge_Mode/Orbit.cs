using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Orbit : MonoBehaviour {

    //[SerializeField] float m_slideBackwards = 1.0f;
    [SerializeField] Camera m_camera;

    [SerializeField] SenseIfGamePieceIsCentered m_foundGamePiece;
    [SerializeField] DetectIfGamePieceLeavesScreenView m_detectGameOver;

    [SerializeField] RepositionAfterGameOver m_repositionAfterGameOver;

    [SerializeField] TimeElapsed m_timeElapsed;
    [SerializeField] public float m_timeInterval = 10;
    [SerializeField] public float m_difficultyMultiplier;

    [SerializeField] float m_minSpeed;
    [SerializeField] float m_maxSpeed;

    //[SerializeField] public float m_startingSpeed = m_minSpeed;
    public float m_startingSpeed;

    [SerializeField] GameObject m_gameOverDisplay;

    [SerializeField] Animator m_animator;


    [HideInInspector] public float m_flightSpeed;

    [SerializeField] GameObject m_imagesContainer;
    private Transform m_imagesContainerTransform;
    [SerializeField] GameObject[] m_imagesArray;
    private int m_index;

    Vector3 m_screenPosition;
    //float m_previousGyroValue = 0;
    //float m_currentGyroValue = 0;

    Rigidbody m_rigidbody { get { return GetComponent<Rigidbody>(); } set { m_rigidbody = value; } }

    //[SerializeField] float m_angularSpeed;
    //[SerializeField] float m_targetRotationalAngle = 90;
    //Vector3 m_rotationAxis;

    Vector3 flightDirection;

    private void Awake() {
        //m_imagesContainerTransform = m_imagesContainer.transform;
        //m_imagesArray = new GameObject[transform.childCount];

        //for (int i = 0; i < (m_imagesContainerTransform.childCount - 1); i++) {
        //    m_imagesArray[i] = m_imagesContainer.transform.GetChild(i).gameObject;
        //    print(m_imagesArray[i]);
        //}

        //print("m_imagesArray.Length = " + m_imagesArray.Length);
    }

    // Use this for initialization
    void Start() {
        ResetDifficultyMultiplier();

        //m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);

        //while (m_gameOverDisplay.activeInHierarchy == true) {
        //    if (m_detectGameOver.enabled == false) {
        //        RandomizePositionAtBeginning(flightDirection);
        //    } else {
        //        StartCoroutine( Flight(flightDirection) );
        //    }
        //}
    }

    private void OnEnable() {
        //m_difficultyMultiplier = 1;

        //while (m_gameOverDisplay.activeSelf == true) {
        //    if (m_detectGameOver.enabled == false) {
        //        RandomizePositionAtBeginning(flightDirection);
        //    } else {
        //        StartCoroutine(Flight(flightDirection));
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 flightDirection = new Vector3(Random.Range(-180, 180), Random.Range(-360, 360), Random.Range(-360, 360));

        if (m_detectGameOver.enabled == false) {
            RandomizePositionAtBeginning(flightDirection);
        } else {
            StartCoroutine(Flight(flightDirection));
        }
    }

    public void RandomizePositionAtBeginning(Vector3 flightDirection) {
        // beginning flightSpeed must be faster, so that it will give players the challenge of finding the hummingbird.
        m_flightSpeed = Random.Range(3.0f, 8.0f);

       transform.RotateAround(m_camera.transform.position, flightDirection, m_flightSpeed);
        //m_repositionAfterGameOver.ResetPosition();
        //m_repositionAfterGameOver.ReEnable();
    }

    public IEnumerator Flight(Vector3 flightDirection) {
        m_flightSpeed = m_startingSpeed + Random.Range(0.0f, 0.9f) + DifficultyMultiplier();
        //Debug.Log("m_flightSpeed = " + m_flightSpeed);

        //transform.RotateAround(m_camera.transform.position, flightDirection, m_flightSpeed);
        //transform.RotateAround(m_camera.transform.position, new Vector3(1.0f, 1.0f, 0.0f), 4.0f);
        transform.RotateAround(m_camera.transform.position, flightDirection, Mathf.Clamp(m_flightSpeed * Time.deltaTime, m_minSpeed, m_maxSpeed));

        if (m_flightSpeed < 2.5f) {
            yield return new WaitForSecondsRealtime(Random.Range(0, 3));
        } else {
            yield return new WaitForSecondsRealtime(Random.Range(6, 360));
        }

        m_animator.SetTrigger("ChangeDirectionsAnim");
    }

    public float GetTime() {
        //Debug.Log("m_timeElapsed.Timer() = " + m_timeElapsed.Timer());
        return m_timeElapsed.Timer();
    }

    public float DifficultyMultiplier() {
        if (m_detectGameOver.enabled == true) {
            if (GetTime() >= 1.0f) {
                if ( Mathf.Floor(GetTime()) % m_timeInterval == 0) {
                    //Debug.Log("Mathf.Floor(GetTime()) = " + Mathf.Floor(GetTime()));
                    //m_difficultyMultiplier += m_difficultyMultiplier;
                    print("m_difficultyMultiplier = " + m_difficultyMultiplier);

                    //StartCoroutine(AddSparkles());

                    //return m_difficultyMultiplier + m_startingSpeed;
                    return m_difficultyMultiplier++;
                }
            }
        }

        Debug.Log("m_difficultyMultiplier = " + m_difficultyMultiplier);
        return m_difficultyMultiplier;
    }

    IEnumerator AddSparkles() {
        print("Inside 'AddSparkles()' IEnumerator.");
        m_index = Random.Range(0, m_imagesArray.Length - 1);

        m_imagesArray[m_index].SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        m_imagesArray[m_index].SetActive(false);
    }

    public void ResetDifficultyMultiplier() {
        //m_difficultyMultiplier = m_startingSpeed;
        m_flightSpeed = m_minSpeed;
        m_difficultyMultiplier = 0;
    }

}