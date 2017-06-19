using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public Transform target;
    [SerializeField]
    float m_speedMultiplier = 10.0f;
    [SerializeField]
    float m_slideBackwards = 1.0f;
    [SerializeField]
    Camera m_camera;
    Vector3 m_screenPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_screenPosition = m_camera.WorldToViewportPoint(this.transform.position);
        //speed = GetSpeed();

        //transform.RotateAround(target.position, target.up, speed * Time.deltaTime);

        if (this.enabled)
        {
            //float speedOfGamePiece = GetSpeed();

            Vector3 targetDir = target.position - transform.position;

            float angle = Vector3.Angle(targetDir, transform.forward);

            //if (m_screenPosition.x < 1.0f) {
            //if (angle <= 45 && angle >= -45) {
                //transform.RotateAround(target.position, target.up, speedOfGamePiece);
                //transform.RotateAround(target.position, Vector3.up, -m_slideBackwards + speedOfGamePiece);
                //transform.RotateAround(target.position, Vector3.right, -m_slideBackwards + speedOfGamePiece);
                transform.RotateAround(m_camera.transform.position, Vector3.up, ClampMovements());
            //}
        }
    }

    public float GetSpeed()
    {
        //float speed = (Input.acceleration.x - m_previousSpeed) / Time.deltaTime;

        //m_previousSpeed = Input.acceleration.x;

        float speed = Mathf.Clamp(Mathf.Abs(Input.gyro.attitude.x) * m_speedMultiplier, 0.0f, 1.5f);

        return speed;
    }

    public float ClampMovements() {
        //return Mathf.Clamp(m_screenPosition.x, 0.0f, 1.1f);

        if (m_screenPosition.x > 1.1f) {
            return -m_slideBackwards * 1.2f;
        } else if (m_screenPosition.x < 1.1f) {
            return -m_slideBackwards + GetSpeed();
        } else {
            return -m_slideBackwards;
        }
    }
}