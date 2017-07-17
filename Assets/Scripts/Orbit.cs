using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Orbit : MonoBehaviour {

    //[SerializeField] float m_slideBackwards = 1.0f;
    [SerializeField] Camera m_camera;

    [SerializeField] SenseIfGamePieceIsCentered m_foundGamePiece;
    [SerializeField] DetectIfGamePieceLeavesScreenView m_detectGameOver;

    Vector3 m_screenPosition;
    //float m_previousGyroValue = 0;
    //float m_currentGyroValue = 0;

    Rigidbody m_rigidbody { get { return GetComponent<Rigidbody>(); } set { m_rigidbody = value; } }

    //[SerializeField] float m_angularSpeed;
    //[SerializeField] float m_targetRotationalAngle = 90;
    //Vector3 m_rotationAxis;

    // Use this for initialization
    void Start() {
        m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 flightDirection = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));

        if (m_detectGameOver.enabled == false) {
            RandomizePositionAtBeginning(flightDirection);
        } else {
            StartCoroutine( Flight(flightDirection) );
        }
    }

    public void RandomizePositionAtBeginning(Vector3 flightDirection) {
        // beginning flightSpeed must be faster, so that it will give players the challenge of finding the hummingbird.
        float flightSpeed = Random.Range(3.0f, 8.0f);

        transform.RotateAround(m_camera.transform.position, flightDirection, 8.0f);
    }

    public IEnumerator Flight(Vector3 flightDirection) {
        float flightSpeed = Random.Range(0.1f, 4.5f);

        transform.RotateAround(m_camera.transform.position, flightDirection, flightSpeed);

        if (flightSpeed < 2.5f) {
            yield return new WaitForSecondsRealtime(Random.Range(0, 5));
        } else {
            yield return new WaitForSecondsRealtime(Random.Range(6, 360));
        }
    }

}