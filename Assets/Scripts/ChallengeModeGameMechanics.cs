using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeModeGameMechanics : MonoBehaviour {
    [SerializeField] GameManager m_gameManager;

    [SerializeField] GameObject m_introPanel;

    [SerializeField] Transform m_target;

    [SerializeField] TimeElapsed m_timeElapsed;

    Camera m_camera { get { return GetComponent<Camera>(); } set { m_camera = value; } }
    Vector3 m_positionInScreen;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_positionInScreen = m_camera.WorldToViewportPoint(m_target.position);

        SenseIfGamePieceIsCentered();
    }

    public void SenseIfGamePieceIsCentered() {
        if (m_introPanel.activeInHierarchy == false && m_positionInScreen.x > 0.4f && m_positionInScreen.x < 0.6f && m_positionInScreen.y > 0.4f && m_positionInScreen.y < 0.6f) {

            DetectIfGamePieceLeavesScreenView();
            m_timeElapsed.ResetTime();
        }
    }

    public void DetectIfGamePieceLeavesScreenView() {
        m_timeElapsed.enabled = true;

        if (m_positionInScreen.x < 0.0f || m_positionInScreen.x > 1.0f || m_positionInScreen.y < 0.0f || m_positionInScreen.y > 1.0f) {
            Debug.Log("It's game over, man; game over!");
            m_gameManager.GameOver();
        }
    }

}