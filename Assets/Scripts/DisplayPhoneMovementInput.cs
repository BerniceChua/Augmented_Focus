using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPhoneMovementInput : MonoBehaviour{

    [SerializeField] GameObject m_gamePiece;
    [SerializeField] Camera m_camera;
    [SerializeField] Orbit m_orbit;

    [SerializeField] DetectIfGamePieceLeavesScreenView m_detectGameOver;
    [SerializeField] SenseIfGamePieceIsCentered m_centeredGameObj;

    [SerializeField] TimeElapsed m_timeElapsed;

    Text m_text { get { return GetComponent<Text>(); } set { m_text = value; } }

    Vector3 m_lastPosition = Vector3.zero;
    float m_previousGyroValue = 0.0f;
    float m_currentGyroValue = 0.0f;
    //[SerializeField] Camera m_cam;
    Vector3 m_whereCameraIsPointing;
    Vector3 m_screenPosition;

    float m_radiusOfOrbit = 2.0f;

    // Use this for initialization
    void Start()
    {
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward);
        m_whereCameraIsPointing = m_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 2));
        m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        string gyroAttitude = Input.gyro.attitude.ToString();
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward);
        m_whereCameraIsPointing = m_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, m_radiusOfOrbit));

        //float speed = (Input.acceleration - m_lastPosition).magnitude / Time.deltaTime;
        //float speed = (Input.acceleration.x - m_lastPosition.x) / Time.deltaTime;
        //float speed = (Input.gyro.attitude.x - m_lastPosition.x) / Time.deltaTime;


        //m_previousSpeed = Input.acceleration.x;
        //m_lastPosition = Input.acceleration;
        m_previousGyroValue = Input.gyro.attitude.x;

        //float speed = (Input.gyro.attitude.x - m_previousSpeed) / Time.deltaTime;
        //float speed = Input.gyro.attitude.x;
        //float speed = Input.gyro.userAcceleration.x;

        //m_previousSpeed = Input.acceleration.x;
        //m_lastPosition = Input.acceleration;
        m_currentGyroValue = Mathf.Lerp(m_currentGyroValue, Input.gyro.attitude.x, 0.2f);
        float speed = m_currentGyroValue - m_previousGyroValue;

        m_text.text = "Gyro Attitude = " + gyroAttitude +
            //"\n Input.gyro.attitude.x = " + Input.gyro.attitude.x.ToString() +
            //"\ncurrentGyroValue = " + (Mathf.Round(Mathf.Abs(m_currentGyroValue) * 100)).ToString() +
            //"\nm_previousSpeed = " + (Mathf.Round(Mathf.Abs(m_previousGyroValue) * 100)).ToString() +
            //"\nAccelerometer = " + Input.acceleration.ToString() +
            "\nm_gamePiece.GetComponent<Orbit>().enabled = " + m_gamePiece.GetComponent<Orbit>().enabled +
            "\nm_orbit.m_difficultyMultiplier = " + m_orbit.m_difficultyMultiplier +
            "\nm_orbit.m_timeInterval = " + m_orbit.m_timeInterval +
            "\nm_orbit.m_flightSpeed = " + m_orbit.m_flightSpeed +
            "\nm_whereCameraIsPointing = " + m_whereCameraIsPointing +
            //"\nm_timeElapsed.Timer() = " + m_timeElapsed.Timer() +
            //"\nm_timeElapsed.m_RunningTime = " + m_timeElapsed.m_RunningTime +
            "\nm_orbit.GetTime() = " + m_orbit.GetTime() +
            "\nDetectIfGamePieceLeavesScreenView is active? = " + (m_detectGameOver.enabled == true) +
            "\nSenseIfGamePieceIsCentered is active? = " + (m_centeredGameObj.enabled == true) +
            "\nm_gamePiece.transform.position = " + m_gamePiece.transform.position.ToString() +
            "\nm_screenPosition.x = " + m_screenPosition.x +
            //"\n(Input.gyro.attitude.x - m_previousSpeed) = " + (Input.gyro.attitude.x - m_previousSpeed).ToString() +
            //"\nm_gamePiece.transform.rotation = " + m_gamePiece.transform.rotation.ToString() +
            //"\nm_cameraPosition.transform = " + m_cameraPosition.transform.position.ToString() +
           "\nTime.timeScale = " + Time.timeScale; //+
            //"\nm_lastPosition = " + m_lastPosition.ToString();
        
        m_previousGyroValue = Input.gyro.attitude.x;
    }
}