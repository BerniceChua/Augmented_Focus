using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public Transform target;

    [SerializeField] float m_speedMultiplier = 2.0f;
    [SerializeField] float m_slideBackwards = 1.0f;
    [SerializeField]  Camera m_camera;

    Vector3 m_screenPosition;
    float m_previousGyroValue = 0;
    float m_currentGyroValue = 0;

    float m_thrust;
    Rigidbody m_rigidbody { get { return GetComponent<Rigidbody>(); } set { m_rigidbody = value; } }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);
        //speed = GetSpeed();

        //transform.RotateAround(target.position, target.up, speed * Time.deltaTime);

        if (this.enabled)
        {
            //float speedOfGamePiece = GetSpeed();

            Vector3 targetDir = target.position - transform.position;

            float angle = Vector3.Angle(targetDir, transform.forward);

            //m_currentGyroValue = (Input.gyro.attitude.x - m_previousGyroValue) * 100;

            //if (m_screenPosition.x < 1.0f) {
            //if (angle <= 45 && angle >= -45) {
            //transform.RotateAround(target.position, target.up, speedOfGamePiece);
            //transform.RotateAround(target.position, Vector3.up, -m_slideBackwards + speedOfGamePiece);
            //transform.RotateAround(target.position, Vector3.right, -m_slideBackwards + speedOfGamePiece);

            transform.RotateAround(m_camera.transform.position, Vector3.up, ClampMovements());
            
            //}

            //Waiting(1);
            //m_previousGyroValue = Input.gyro.attitude.x * 100;
        }
    }

    public float GetSpeed() {
        //float speed = Mathf.Clamp(Mathf.Abs(Input.gyro.attitude.x) * m_speedMultiplier, 0.0f, 1.5f);

        //float speed;
        ////m_previousGyroValue = Input.gyro.attitude.x * 100;
        //Waiting(1);
        ////float currentGyroValue = (Input.gyro.attitude.x - m_previousGyroValue) * 100;

        //if (Mathf.Round(Mathf.Abs(currentGyroValue)) == Mathf.Round(Mathf.Abs(m_previousGyroValue)))
        //    speed = 0;
        //else
        //    speed = 1;

        float speed;
        //float currentGyroValue = Mathf.Lerp(m_previousGyroValue, Input.gyro.attitude.x, 0.2f) * 100;
        m_currentGyroValue = Mathf.Lerp(m_currentGyroValue, Input.gyro.attitude.x, 0.75f) * 100;
        m_previousGyroValue = Input.gyro.attitude.x * 100;
        //Waiting(1);

        if (Mathf.Round(Mathf.Abs(m_currentGyroValue)) == Mathf.Round(Mathf.Abs(m_previousGyroValue)))
            speed = 0;
        else
            speed = 1;

        return speed;
    }

    public float ClampMovements() {
        //return Mathf.Clamp(m_screenPosition.x, 0.0f, 1.1f);

        if (m_screenPosition.x > 1.1f) {
            return -m_slideBackwards * 3.0f;
        } else if (m_screenPosition.x < 1.1f && m_screenPosition.x > 0.9f) {
            return -m_slideBackwards * 2.0f;
        } else {
            return -m_slideBackwards + GetSpeed();
        }

        //if (m_screenPosition.x >= 1.1f) {
        //    return -m_slideBackwards * 2.0f;
        //} else {
        //    return -m_slideBackwards + GetSpeed();
        //}
    }

    IEnumerator Waiting(float time) {
        yield return new WaitForSeconds(time);
    }
}