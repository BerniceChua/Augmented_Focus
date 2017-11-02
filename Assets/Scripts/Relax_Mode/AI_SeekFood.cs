using UnityEngine;
using System.Collections;

public class AI_SeekFood : MonoBehaviour {

    public string critterType = "Vegetable";

    public float eatingRange = 1f;
    public float eatHPPerSecond = 5f;
    public float eatHP2Energy = 2f;

    Critter myCritter;

    string stringMosquito = "Mosquito";
    [SerializeField] Animator m_animator;

    // Use this for initialization
    void Start() {
        myCritter = GetComponent<Critter>();
    }

    public void DoAIBehaviour() {

        if (Critter.crittersByType.ContainsKey(critterType) == false) {
            // We have nothing to eat!
            return;
        }

        // Find the closest edible critter to us.
        Critter closest = null;
        //Critter closestGameObject = null;
        float dist = Mathf.Infinity;

        foreach (Critter c in Critter.crittersByType[critterType]) {
            Debug.Log("critter c in foreach of AI_SeekFood = " + c);
            if (c.health <= 0) {
                // This is already dead, ignore it.
                continue;
            }

            if (c.isInHedge) {
                // This possible target is hidden, so we can't chase it.
                continue;
            }

            //float d = Vector2.Distance(this.transform.position, c.transform.position);
            float d = Vector3.Distance(this.transform.position, c.transform.position);

            if (closest == null /*|| closestGameObject == null*/ || d < dist) {
                closest = c;
                //closestGameObject = c;
                dist = d;
            }

        }

        //closestGameObject = DetectFood();

        if (closest == null /*|| closestGameObject == null*/) {
            // No valid food targets exist.
            return;
        }

        if (dist < eatingRange) {
            float hpEaten = Mathf.Clamp(eatHPPerSecond * Time.deltaTime, 0, closest.health);
            closest.health -= hpEaten;
            myCritter.energy += hpEaten * eatHP2Energy;
        } else {
            //if (closest != null) {
                // Now we want to move towards this closest edible critter

                // Not a direction, it's the total distance of x & y (& possibly z) between the 2.
                //Vector2 dir = closest.transform.position - this.transform.position;
                Vector3 dir = closest.transform.position - this.transform.position;

                WeightedDirection wd = new WeightedDirection(dir, 5);

                myCritter.desiredDirections.Add(wd);
            //}

            //if (closestGameObject != null) {
            //    Vector3 dir = closestGameObject.transform.position - this.transform.position;

            //    WeightedDirection wd = new WeightedDirection(dir, 5);

            //    myCritter.desiredDirections.Add(wd);
            //}
        }
    }

    //Critter DetectFood() {
    //    Critter[] foods;
    //    foods = GameObject.FindGameObjectsWithTag(stringMosquito);
    //    Critter closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (Critter food in foods) {
    //        Vector3 diff = food.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance) {
    //            closest = food;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest;
    //}

    private void OnCollisionStay(Collision collision) {
        if (collision.transform.tag == stringMosquito) {
            //playerInRange = true;

            print("hit the mosquito collider.");

            StartCoroutine(Eat());
        }
    }

    IEnumerator Eat() {
        //animation = GetComponent<Animation>();

        m_animator.SetTrigger("Attack");
        //DetectFood().gameObject.SetActive(false);
        //yield return new WaitForSeconds(animation.clip.length);
        yield return new WaitForSeconds(3);
        //Destroy(DetectFood().gameObject);
        yield return new WaitForSeconds(2);
    }

}
