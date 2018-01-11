using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/FoodIsReachedDecision")]
public class FoodIsReachedDecision : Decision {

    public override bool Decide(StateController controller) {
        
        /// if creature has moved to and reached the location of food
        if (controller.transform.position == controller.DetectFood().transform.position) {
            return true;
        } else {
            return false;
        }
    }

}
