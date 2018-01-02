using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/EatFoodAction")]
public class EatFoodAction : Action {

    public override void Act(StateController controller) {
        Eat(controller);
    }

    private void Eat(StateController controller) {
        //RaycastHit hit;

        ///// Will draw a red line in front of where this is looking.
        //Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        //if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange) && hit.collider.CompareTag("Target")) {
        //    if (controller.CheckIfCountdownElapsed(controller.enemyStats.attackRate)) {
        //        controller.tankShooting.Fire(controller.enemyStats.attackForce, controller.enemyStats.attackRate);
        //    }
        //}



        //animation = GetComponent<Animation>();

        //DetectFood().gameObject.SetActive(false);
        //yield return new WaitForSeconds(animation.clip.length);
        //yield return new WaitForSeconds(3);
        new WaitForSeconds(3);
        Debug.Log("I am eating the food!");
        controller.m_animator.Play("Attack");
        //m_noFood = false;
        Debug.Log("I ate the food!");
        //yield return new WaitForSeconds(5);
        //Destroy(DetectFood());
        GameObject food = controller.DetectFood();
        FoodInTheScene.RemoveFromFoodList(controller.DetectFood());
        new WaitForSeconds(5);
        //Destroy(controller.DetectFood());
        //m_noFood = true;
        controller.DetectFood().SetActive(false);
    }

}
