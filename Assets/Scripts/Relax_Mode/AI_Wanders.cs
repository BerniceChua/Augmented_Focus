using UnityEngine;
using System.Collections;

public class AI_Wanders : MonoBehaviour {

    Critter myCritter;

    //Vector2 fromDir;
    //Vector2 toDir;
    Vector3 fromDir;
    Vector3 toDir;

    // Use this for initialization
    void Start() {
        myCritter = GetComponent<Critter>();

        fromDir = Random.onUnitSphere * 0.125f;
        toDir = Random.onUnitSphere * 0.125f;
    }


    void DoAIBehaviour() {
        //Vector2 dir = fromDir;
        Vector3 dir = fromDir;

        WeightedDirection wd = new WeightedDirection(dir, 0.01f);

        myCritter.desiredDirections.Add(wd);
    }

}
