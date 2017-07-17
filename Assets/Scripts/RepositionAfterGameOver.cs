using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionAfterGameOver : MonoBehaviour {
    [SerializeField] TimeElapsed m_timeElapsed;
    [SerializeField] GameObject m_gamePiece;
    [SerializeField] Camera m_camera;
    [SerializeField] float m_radiusOfOrbit = 5.0f;

    Vector3 m_whereCameraIsPointing;

    private Vector3 m_initialPosition;
    //private Quaternion m_initialRotation;

    float zPlane;

    // Use this for initialization
    void Start () {
        //m_initialRotation = m_gamePiece.transform.rotation;
        //m_initialRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        m_initialPosition = m_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, m_radiusOfOrbit));
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward * m_initialPosition.z);
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(m_camera.transform.forward * 144.77f);
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(Vector3.forward * 144.77f);
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(Vector3.forward * m_radiusOfOrbit);
        m_whereCameraIsPointing = m_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, m_radiusOfOrbit));

        //zPlane = m_camera.farClipPlane - ((m_camera.farClipPlane - m_camera.nearClipPlane) / 2);
        //m_whereCameraIsPointing = m_camera.ViewportToWorldPoint(new Vector3(.5f, .5f, zPlane));
        
        //ResetPosition();
    }

    // Update is called once per frame
    void Update () {
        if (m_timeElapsed.m_gameOver) {
            m_gamePiece.SetActive(false);
            m_gamePiece.transform.position = new Vector3(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
            //m_gamePiece.transform.position.Set(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
            //m_gamePiece.transform.position = m_whereCameraIsPointing;
        }
    }

    public void ResetPosition() {
        //if (m_timeElapsed.m_gameOver) {
        //m_gamePiece.transform.position.Set(0, -5, 44.77f);
        Debug.Log("m_whereCameraIsPointing = " + m_whereCameraIsPointing);
        m_gamePiece.transform.position = new Vector3(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
        //m_gamePiece.transform.position = m_whereCameraIsPointing;
        //m_gamePiece.transform.position.Set(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
        //m_gamePiece.transform.rotation = m_initialRotation;
        //m_gamePiece.SetActive(false);
        //m_gamePiece.GetComponent<Orbit>().enabled = false;
    //}
    }

    public void ReEnable() {
        m_gamePiece.transform.position = new Vector3(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
        //m_gamePiece.transform.position = m_whereCameraIsPointing;
        //m_gamePiece.transform.position.Set(m_whereCameraIsPointing.x, m_whereCameraIsPointing.y, m_whereCameraIsPointing.z);
        //m_gamePiece.transform.rotation = m_initialRotation;
        m_gamePiece.SetActive(true);
        m_gamePiece.GetComponent<Orbit>().enabled = true;
    }
}