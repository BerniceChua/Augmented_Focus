
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovementAIController : MonoBehaviour {
    public float m_sensorLength = 5.0f;
    public float m_speed = 10.0f;
    public float m_turnSpeed = 50.0f;

    float m_directionValue = 1.0f; // only controls forward and backward
    float m_turnValue = 0.0f;
    Collider m_myCollider;

	// Use this for initialization
	void Start () {
        m_myCollider = transform.GetComponent<Collider>();  // makes sure that sensor is not hitting its own collider.
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        int flag = 0; // simple check that there has been a sensor detection.

        // right sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, (m_sensorLength + transform.localScale.x))) {
            if (hit.collider.tag != "Obstacle" || hit.collider == m_myCollider)   // makes sure that sensor is not hitting its own collider.
                return;

            Debug.Log("I found the wall.");
            m_turnValue -= 1;
            flag++;
        }

        // left sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, (m_sensorLength + transform.localScale.x))) {
            if (hit.collider.tag != "Obstacle" || hit.collider == m_myCollider)  // makes sure that sensor is not hitting its own collider.
                return;

            Debug.Log("I found the wall.");
            m_turnValue += 1;
            flag++;
        }

        // front sensor
        if (Physics.Raycast(transform.position, transform.forward, out hit, (m_sensorLength + transform.localScale.z))) {
            if (hit.collider.tag != "Obstacle" || hit.collider == m_myCollider)  // makes sure that sensor is not hitting its own collider.
                return;

            Debug.Log("I found the wall.");

            if (m_directionValue == 1.0f)
                m_directionValue = -1;
            
            flag++;
        }

        // back sensor
        if (Physics.Raycast(transform.position, -transform.forward, out hit, (m_sensorLength + transform.localScale.z))) {
            if (hit.collider.tag != "Boundary" || hit.collider == m_myCollider)  // makes sure that sensor is not hitting its own collider.
                return;

            Debug.Log("I found the wall.");

            if (m_directionValue == -1.0f)
                m_directionValue = 1;
            
            flag++;
        }


        if (flag == 0) {
            m_turnValue = 0;
        }
        transform.Rotate(Vector3.up * m_turnSpeed * m_turnValue * Time.deltaTime);

        transform.position += transform.forward * (m_speed * m_directionValue) * Time.deltaTime;
	}

    // monodevelop behavior that visualizes in editor but not in final game.  used for prototyping.
    private void OnDrawGizmos() {
        //// forward sensor
        //Gizmos.DrawRay(transform.position, transform.forward * (m_sensorLength + transform.localScale.z));

        //// backward sensor
        //Gizmos.DrawRay(transform.position, -transform.forward * (m_sensorLength + transform.localScale.z));


        //Gizmos.DrawRay(transform.position, transform.right * (m_sensorLength + transform.localScale.x));
        //Gizmos.DrawRay(transform.position, -transform.right * (m_sensorLength + transform.localScale.x));

        // forward sensor
        Gizmos.DrawRay(transform.position, transform.forward * (m_sensorLength));

        // backward sensor
        Gizmos.DrawRay(transform.position, -transform.forward * (m_sensorLength));


        Gizmos.DrawRay(transform.position, transform.right * (m_sensorLength));
        Gizmos.DrawRay(transform.position, -transform.right * (m_sensorLength));
    }
}