using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

    public override bool Decide(StateController controller) {
        //bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
        //bool chaseTargetIsActive = FindObjectOfType<Target>().gameObject.activeSelf;
        //if (FindObjectOfType<Target>().gameObject.activeSelf != null) {
        //if (FindObjectOfType<Target>()) {
        //    TargetsAndArrayList.CountThingsInArrayList();
        //    Debug.Log("Inside ActiveStateDecision1.Decide()");
        //    Debug.Log("FindObjectOfType<Target>().name = " + FindObjectOfType<Target>().name);
        //    return true;
        //} else {
        //    return false;
        //}

        //bool chaseTargetIsActive = TargetsAndArrayList.GetNearestInArrayList().activeSelf;
        //return chaseTargetIsActive;

        Debug.Log("FoodInTheScene.GetNearestInList() = " + FoodInTheScene.GetNearestInList());

        //bool targetIsActive = FoodInTheScene.GetNearestInList().activeSelf;
        //bool targetIsActive = controller.DetectFood().gameObject.activeSelf;
        //if (chaseTargetIsActive) {
        //Target target = FindObjectOfType<Target>();
        if (/*target != null &&*/ FoodInTheScene.CountThingsInFoodList() != 0 /*&& targetIsActive*/) {
        //if (targetIsActive) {
            //Debug.Log(TargetsAndArrayList.CountThingsInArrayList());
            Debug.Log("Inside ActiveStateDecision1.Decide() \n FoodInTheScene.GetNearestInList().name = " + FoodInTheScene.GetNearestInList().name);
            return true;
        }
        else {
            return false;
        }

    }

}
