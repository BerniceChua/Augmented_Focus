using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseIfGamePieceIsCentered : MonoBehaviour {
    [SerializeField] Transform m_target;

    [SerializeField] TimeElapsed m_timeElapsed;

    [SerializeField] GameObject m_introPanel;

    DetectIfGamePieceLeavesScreenView m_detectIfLeaveScreenView { get { return GetComponent<DetectIfGamePieceLeavesScreenView>(); } set { m_detectIfLeaveScreenView = value; } }

    Camera m_camera { get { return GetComponent<Camera>(); } set { m_camera = value; } }

    Vector3 m_whereCameraCenterIsPointing;


    // Use this for initialization
    void Start () {
        m_detectIfLeaveScreenView.enabled = false;
        m_timeElapsed.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        m_whereCameraCenterIsPointing = m_camera.WorldToViewportPoint(m_target.position);

        //if (m_whereCameraCenterIsPointing.x > 0.4f && m_whereCameraCenterIsPointing.x < 0.6f && m_whereCameraCenterIsPointing.y > 0.4f && m_whereCameraCenterIsPointing.y < 0.6f) {
        if (m_introPanel.activeInHierarchy == false && m_whereCameraCenterIsPointing.x > 0.4f && m_whereCameraCenterIsPointing.x < 0.6f && m_whereCameraCenterIsPointing.y > 0.4f && m_whereCameraCenterIsPointing.y < 0.6f) {
        //if (m_introPanel.activeInHierarchy == false && m_whereCameraCenterIsPointing.x == 0.5f && m_whereCameraCenterIsPointing.y == 0.5f) {
            m_detectIfLeaveScreenView.enabled = true;
            m_timeElapsed.enabled = true;
        }

        return;
    }
}