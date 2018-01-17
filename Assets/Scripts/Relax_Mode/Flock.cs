using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    [SerializeField] public float m_MinSpeed = 0.05f;
    [SerializeField] public float m_MaxSpeed = 2.0f;

    public float m_speed;
    float m_rotationSpeed = 4.0f;
    Vector3 m_averageHeading;
    Vector3 m_averagePosition;
    float m_neighborDistance = 3.0f;  // NPCs will only have flocking behavior if they are within this distance.

    bool turning = false;  // will become set to true when the NPCs reach the edge, so the NPCs will turn back.








    [SerializeField] Animator m_animator;
    string stringMosquito = "Mosquito";
    bool m_noFood = true;



    // Use this for initialization
    void Start() {
        m_speed = Random.Range(m_MinSpeed, m_MaxSpeed);
    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPosition = transform.position;
        if (DetectFood() != null) {
            print("INSIDE 'if (DetectFood() != null)' ++");

            targetPosition.y = Mathf.MoveTowards(transform.position.y, DetectFood().gameObject.transform.position.y, m_speed * Time.deltaTime);
            targetPosition.z = Mathf.MoveTowards(transform.position.z, DetectFood().gameObject.transform.position.z, m_speed * Time.deltaTime);
            targetPosition.x = Mathf.MoveTowards(transform.position.x, DetectFood().gameObject.transform.position.x, m_speed * Time.deltaTime);

            transform.Translate(Vector3.forward * Random.Range(0.0f, 1.0f) * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(targetPosition);

            StartCoroutine(Eat());
            //OnCollisionStay(collision);
        } else {

            if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.m_areaSize)
                turning = true;
            else
                turning = false;

            if (turning) {
                Vector3 thisDirection = Vector3.zero - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(thisDirection), m_rotationSpeed * Time.deltaTime);

                m_speed = Random.Range(0.5f, 1.0f);
            } else {
                if (Random.Range(0, 10) < 1)
                    ApplyFlockingAlgorithm();
            }

            //transform.Translate(0, 0, ( Random.Range(0.5f, 5.0f) ) * Time.deltaTime );
            transform.Translate(0, 0, m_speed * Time.deltaTime);
        }
    }

    public void ApplyFlockingAlgorithm() {
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
            if (myGameObject != this.gameObject) {
                myDistance = Vector3.Distance(myGameObject.transform.position, this.transform.position);

                if (myDistance <= m_neighborDistance) {
                    vectorCenter += myGameObject.transform.position;
                    groupSize++;

                    // if the distance is too short, avoid the other NPC by going to another direction
                    if (myDistance < 1.0f)
                        vectorAvoid = vectorAvoid + (this.transform.position - myGameObject.transform.position);

                    Flock anotherFlock = myGameObject.GetComponent<Flock>();  // find the average speed of the entire group
                    groupSpeed += anotherFlock.m_speed;           // by adding the speed of all the NPCs that are in the flock.
                }
            }
        }

        // if NPC is in a group (group size is bigger than 1), calculate the average center and average speed of the group.
        if (groupSize > 0) {
            vectorCenter = vectorCenter / groupSize + (goalPosition - this.transform.position);
            m_speed = groupSpeed / groupSize;

            Vector3 direction = (vectorCenter + vectorAvoid) - transform.position;

            // change direction if the vector is not zero.
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), m_rotationSpeed * Time.deltaTime);
        }
    }






    GameObject DetectFood() {
        GameObject[] foods;
        foods = GameObject.FindGameObjectsWithTag(stringMosquito);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject food in foods) {
            Vector3 diff = food.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = food;
                distance = curDistance;
            }
        }
        return closest;
    }

    IEnumerator Eat() {
        //animation = GetComponent<Animation>();

        //DetectFood().gameObject.SetActive(false);
        //yield return new WaitForSeconds(animation.clip.length);
        yield return new WaitForSeconds(3);
        Debug.Log("I am eating the food!");
        m_animator.Play("Attack");
        m_noFood = false;
        Debug.Log("I ate the food!");
        yield return new WaitForSeconds(5);
        Destroy(DetectFood());
        m_noFood = true;
    }

}