using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIfGamePieceLeavesScreenView : MonoBehaviour {
    [SerializeField] GameManager m_gameManager;

    [SerializeField] Transform m_target;

    [SerializeField] TimeElapsed m_timeElapsed;

    [SerializeField] float m_angle;

    Camera m_camera { get { return GetComponent<Camera>(); } set { m_camera = value; } }
    Vector3 m_viewPosition;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        m_viewPosition = m_camera.WorldToViewportPoint(m_target.position);

        m_timeElapsed.enabled = true;

        if (m_viewPosition.x < 0.0f || m_viewPosition.x > 1.0f || m_viewPosition.y < 0.0f || m_viewPosition.y > 1.0f) {
            Debug.Log("It's game over, man; game over!");
            m_gameManager.GameOver();
        }

        return;
	}

    public Vector3 GetViewPosition() {
        return m_viewPosition;
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.cyan;

        if (m_selectedTarget != null) {
            Gizmos.DrawLine(transform.position, m_selectedTarget.transform.position);
        }*/

        Gizmos.color = Color.cyan;

        /// to the right of the normal line coming out of transform.position
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(m_angle / 2) * 50.0f);
        /// to the left of the normal line coming out of transform.position
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-m_angle / 2) * 50.0f);
    }

    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

}