using UnityEngine;
using System;
using System.Collections.Generic;

public class Critter : MonoBehaviour {

    public float health = 100f;
    public float energy = 100f;

    //public float energyPerSecond = 5f;

    public float speed;

    public string critterType = "Vegetable";

    static public Dictionary<string, List<Critter>> crittersByType;

    //Vector2 velocity;
    Vector3 velocity;

    public bool isInHedge = false;

    public List<WeightedDirection> desiredDirections;

    // Use this for initialization
    void Start() {
        // Make sure we're in the crittersByType list.
        AddToDesiredWeightedDirections();
    }

    //private void OnEnable() {
    //    crittersByType[critterType].Add(this);
    //}

    void OnDestroy() {
        // Remove us from the crittersByType list.
        crittersByType[critterType].Remove(this);
    }

    // Update is called once per frame
    void FixedUpdate() {
        //AddToDesiredWeightedDirections();

        //Debug.Log(crittersByType);
        // Critters lose energy per second.
        //energy = Mathf.Clamp(energy - Time.deltaTime * energyPerSecond, 0, 100);

        if (energy <= 0) {
            // Lose health per second if we are starving
            health = Mathf.Clamp(health - Time.deltaTime * 5f, 0, 100);
        }

        if (health <= 0) {
            // We have been killed.
            // TODO: We could Instantiate a "death" object that has a
            // a play-once sound and a "splatter" effect.  Maybe leave
            // bones behind?
            Destroy(gameObject);
            return;
        }


        // Ask all of our AI scripts to tell us in which direction we should move
        desiredDirections = new List<WeightedDirection>();
        Debug.Log("desiredDirections.Count = " + desiredDirections.Count);
        Debug.Log("crittersByType.Count = " + crittersByType.Count);
        BroadcastMessage("DoAIBehaviour", SendMessageOptions.DontRequireReceiver);

        // Add up all the desired directions by weight
        //Vector2 dir = Vector2.zero;
        Vector3 dir = Vector3.zero;
        foreach (WeightedDirection wd in desiredDirections) {
            // NOTE: If you are implementing EXCLUSIVE/FALLBACK blend modes, check here.

            dir += wd.direction * wd.weight;
        }

        //velocity = Vector2.Lerp(velocity, dir.normalized * speed, Time.deltaTime * 5f);
        velocity = Vector3.Lerp(velocity, dir.normalized * speed, Time.deltaTime * 5f);

        // Move in the desired direction at our top speed.
        // NOTE: WeightedDirection does include a currently unused parameter for speed
        transform.Translate(velocity * Time.deltaTime);
    }

    // The base critter script probably doesn't need to know about collisions.
    // The response to collisions is going to be influenced by which
    // behaviours are running.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Hedge>() != null) {
            isInHedge = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Hedge>() != null) {
            isInHedge = false;
        }
    }


    public void AddToDesiredWeightedDirections() {
        // Make sure we're in the crittersByType list.
        if (crittersByType == null) {
            crittersByType = new Dictionary<string, List<Critter>>();
        }
        if (crittersByType.ContainsKey(critterType) == false) {
            crittersByType[critterType] = new List<Critter>();
        }
        crittersByType[critterType].Add(this);
        //Debug.Log(this);
        //Debug.Log(this.GetType());
    }

}
