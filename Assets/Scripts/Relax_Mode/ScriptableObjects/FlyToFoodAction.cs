using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FlyToFoodAction")]
public class FlyToFoodAction : Action {

    [SerializeField] float m_speed;

    public override void Act(StateController controller) {
        FlyToFood(controller);
    }

    private void FlyToFood(StateController controller) {
        //controller.navMeshAgent.destination = controller.m_FoodTarget.position;
        //Debug.Log("Inside ChaseAction1.Chase()");
        //Debug.Log("controller.navMeshAgent.destination = " + controller.navMeshAgent.destination);
        //controller.navMeshAgent.Resume();

        Vector3 targetPosition = controller.transform.position;
        Vector3 myPosition = controller.transform.position;
        Vector3 foodPosition = FoodInTheScene.GetNearestInList().transform.position;

        controller.transform.LookAt(foodPosition);

        targetPosition.y = Mathf.MoveTowards(myPosition.y, foodPosition.y, m_speed * Time.deltaTime);
        targetPosition.z = Mathf.MoveTowards(myPosition.z, foodPosition.z, m_speed * Time.deltaTime);
        targetPosition.x = Mathf.MoveTowards(myPosition.x, foodPosition.x, m_speed * Time.deltaTime);

        controller.transform.Translate(Vector3.forward * UnityEngine.Random.Range(0.0f, 1.0f) * Time.deltaTime);
        controller.GetComponent<Rigidbody>().MovePosition(targetPosition);
    }

}
