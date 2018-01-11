using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RandomCreatureMovementAction")]
public class RandomCreatureMovementAction : Action {

    [SerializeField] float m_speed = 0.001f;

    float m_rotationSpeed = 4.0f;
    Vector3 m_averageHeading;
    Vector3 m_averagePosition;
    float m_neighborDistance = 3.0f;  // NPCs will only have flocking behavior if they are within this distance.

    bool turning = false;  // will become set to true when the NPCs reach the edge, so the NPCs will turn back.

    public override void Act(StateController controller) {
        ApplyFlockingAlgorithm(controller);
    }

    private void ApplyFlockingAlgorithm(StateController controller) {
        // to apply all the rules, each NPC that this code is attached to needs to know about the other NPCs that this code is attached to.
        // that's why we used public static for m_allNpcs.

        GameObject[] gameObjectFlocks = GlobalFlock.m_allNpcs;

        Vector3 vectorCenter = Vector3.zero;  // calculate center of the group
        Vector3 vectorAvoid = Vector3.zero;  // points away from neighbors, so it will avoid its neighbors
        float groupSpeed = 0.1f;

        Vector3 goalPosition = GlobalFlock.m_goalPosition;

        float myDistance;

        int groupSize = 0;  // group size is based on how many neighbors there are, and that's based on the m_neighborDistance.
        foreach (GameObject myGameObject in gameObjectFlocks) {
            if (myGameObject != controller.gameObject) {
                myDistance = Vector3.Distance(myGameObject.transform.position, controller.transform.position);

                if (myDistance <= m_neighborDistance) {
                    vectorCenter += myGameObject.transform.position;
                    groupSize++;

                    // if the distance is too short, avoid the other NPC by going to another direction
                    if (myDistance < 1.0f)
                        vectorAvoid = vectorAvoid + (controller.transform.position - myGameObject.transform.position);

                    Flock anotherFlock = myGameObject.GetComponent<Flock>();  // find the average speed of the entire group
                    groupSpeed += anotherFlock.m_speed;           // by adding the speed of all the NPCs that are in the flock.
                }
            }
        }

        // if NPC is in a group (group size is bigger than 1), calculate the average center and average speed of the group.
        if (groupSize > 0) {
            vectorCenter = vectorCenter / groupSize + (goalPosition - controller.transform.position);
            m_speed = groupSpeed / groupSize;

            Vector3 direction = (vectorCenter + vectorAvoid) - controller.transform.position;

            // change direction if the vector is not zero.
            if (direction != Vector3.zero)
                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(direction), m_rotationSpeed * Time.deltaTime);
        }
    }    

}
