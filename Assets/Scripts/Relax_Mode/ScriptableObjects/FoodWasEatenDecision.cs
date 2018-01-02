using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/FoodWasEatenDecision")]
public class FoodWasEatenDecision : Decision {

    public override bool Decide(StateController controller) {
        //throw new NotImplementedException();

        if (FoodInTheScene.CountThingsInFoodList() != 0 || controller.DetectFood().activeSelf == false) {
            return true;
        } else {
            return false;
        }
    }

}
