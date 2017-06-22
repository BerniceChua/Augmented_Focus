using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPhoneMovementInput : MonoBehaviour{

    [SerializeField] GameObject m_gamePiece;
    [SerializeField] Camera m_camera;
    [SerializeField] Orbit m_orbit;

    Text m_text { get { return GetComponent<Text>(); } set { m_text = value; } }

    Vector3 m_lastPosition = Vector3.zero;
    float m_previousGyroValue = 0.0f;
    float m_currentGyroValue = 0.0f;
    //[SerializeField] Camera m_cam;
    Vector3 m_whereCameraIsPointing;
    Vector3 m_screenPosition;

    // Use this for initialization
    void Start()
    {
        m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward);
        m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        string gyroAttitude = Input.gyro.attitude.ToString();
        m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward);

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
            "\n Input.gyro.attitude.x = " + Input.gyro.attitude.x.ToString() +
            "\ncurrentGyroValue = " + (Mathf.Round(Mathf.Abs(m_currentGyroValue) * 100)).ToString() +
            "\nm_previousSpeed = " + (Mathf.Round(Mathf.Abs(m_previousGyroValue) * 100)).ToString() +
            //"\nAccelerometer = " + Input.acceleration.ToString() +
            "\nm_orbit.GetSpeed() = " + m_orbit.GetSpeed()/*.ToString()*/ +
            "\nm_gamePiece.GetComponent<Orbit>().enabled = " + m_gamePiece.GetComponent<Orbit>().enabled +
            //"\nm_whereCameraIsPointing = " + m_whereCameraIsPointing +
            "\nm_screenPosition.x = " + m_screenPosition.x +
            //"\n(Input.gyro.attitude.x - m_previousSpeed) = " + (Input.gyro.attitude.x - m_previousSpeed).ToString() +
            "\nm_gamePiece.transform.position = " + m_gamePiece.transform.position.ToString() +
            //"\nm_gamePiece.transform.rotation = " + m_gamePiece.transform.rotation.ToString() +
            "\nm_gamePiece.Orbit.GetSpeed() = " + m_gamePiece.GetComponent<Orbit>().GetSpeed() +
           //"\nm_cameraPosition.transform = " + m_cameraPosition.transform.position.ToString() +
           "\nTime.timeScale = " + Time.timeScale; //+
            //"\nm_lastPosition = " + m_lastPosition.ToString();
        
        m_previousGyroValue = Input.gyro.attitude.x;
    }
}