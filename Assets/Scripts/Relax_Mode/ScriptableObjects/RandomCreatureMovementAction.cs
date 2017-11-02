//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCreatureMovementAction : Action {

    [SerializeField] float m_speed;

    [SerializeField] float m_maxSpeed;

    [SerializeField] float xMax;
    [SerializeField] float xMin;
    [SerializeField] float yMax;
    [SerializeField] float yMin;
    [SerializeField] float zMax;
    [SerializeField] float zMin;

    float x;
    float y;
    float z;
    float m_time;
    float m_angle;

    public override void Act(StateController controller) {
        CreatureMovementWithinBounds(controller);
    }

    private void CreatureMovementWithinBounds(StateController controller) {
        DoNotGoTooFar(controller);

        controller.gameObject.transform.Translate(Vector3.forward * Random.Range(0.0f, 2.0f) * Time.deltaTime);

    }

    void DoNotGoTooFar(StateController controller) {
        if (controller.gameObject.transform.localPosition.x > xMax) {
            x = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (controller.transform.localPosition.x < xMin) {
            x = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (controller.transform.localPosition.y > yMax) {
            y = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (controller.transform.localPosition.y < yMin) {
            y = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (controller.transform.localPosition.z > zMax) {
            z = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (controller.transform.localPosition.z < zMin) {
            z = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            controller.transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        controller.transform.localPosition = new Vector3(controller.transform.localPosition.x + x, controller.transform.localPosition.y + y, controller.transform.localPosition.z + z);
    }

}
