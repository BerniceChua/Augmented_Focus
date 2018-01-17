using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour {

    //[SerializeField] float m_mouthPositionX = 4.66f;
    //[SerializeField] float m_mouthPositionX = 0.0f;
    //[SerializeField] float m_mouthPositionY = -0.16f;
    //[SerializeField] float m_mouthPositionZ = 0.75f;

    [SerializeField] Animator m_animator;
    //[SerializeField] Animation animation;

    [SerializeField] float m_speed;

    [SerializeField] float m_maxSpeed;

    [SerializeField] float xMax;
    [SerializeField] float xMin;
    [SerializeField] float yMax;
    [SerializeField] float yMin;
    [SerializeField] float zMax;
    [SerializeField] float zMin;

    float x;
    float y;
    float z;
    float m_time;
    float m_angle;

    GameObject m_mosquito;

    string stringMosquito = "Mosquito";

    bool m_noFood = true;

    Flock m_flockingAlgorithm;

    // Use this for initialization
    void Start() {
        m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;

        m_mosquito = GameObject.FindGameObjectWithTag(stringMosquito);

        //StartCoroutine("Move");

        m_flockingAlgorithm = this.GetComponent<Flock>();
    }

    // Update is called once per frame
    //void Update() {
    //    m_time += Time.deltaTime;

    //    //if (m_mosquito != null) {
    //    //    if (m_mosquito.activeInHierarchy == true) {
    //    //        print("Mosquito is active in the hierarchy");

    //    //        transform.LookAt(m_mosquito.transform);

    //    //        transform.position = Vector3.MoveTowards(transform.position, m_mosquito.transform.position, m_speed * Time.deltaTime);

    //    //    }
    //    //}

    //    //DoNotGoTooFar();

    //    //transform.Translate(Vector3.forward * 3f * Time.deltaTime);
    //    transform.Translate(Vector3.forward * Random.Range(0.0f, 3.0f) * Time.deltaTime);
    //    //transform.Translate(Vector3.forward * Time.deltaTime);

    //    transform.eulerAngles += new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));

    //    Vector3 movePosition = transform.position;

    //    if (DetectFood() != null) {
    //        movePosition.x = Mathf.MoveTowards(transform.position.x, DetectFood().gameObject.transform.position.x, m_speed * Time.deltaTime);
    //    }
    //    GetComponent<Rigidbody>().MovePosition(movePosition);
    //}

    void FixedUpdate() {
        //Vector3 targetPosition = transform.position + new Vector3(m_mouthPositionX, m_mouthPositionY, m_mouthPositionZ);
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
            //DoNotGoTooFar();

            //transform.Translate(Vector3.forward * Random.Range(0.0f, 2.0f) * Time.deltaTime);

            m_flockingAlgorithm.ApplyFlockingAlgorithm();

        }

    }

    IEnumerator Move() {

        //if (m_mosquito != null) {
        //    if (m_mosquito.activeInHierarchy == true) {
        //        print("Mosquito is active in the hierarchy");

        //        transform.LookAt(m_mosquito.transform);

        //        transform.position = Vector3.MoveTowards(transform.position, m_mosquito.transform.position, m_speed * Time.deltaTime);
        //    }
        //}

        //DoNotGoTooFar();

        while (m_noFood) {
            yield return new WaitForSeconds(3.5f);
            //transform.eulerAngles += new Vector3(0, 180f, 0);
            transform.eulerAngles += new Vector3(Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f));
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

    void DoNotGoTooFar() {
        m_time += Time.deltaTime;

        if (transform.localPosition.x > xMax) {
            x = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (transform.localPosition.x < xMin) {
            x = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (transform.localPosition.y > yMax) {
            y = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (transform.localPosition.y < yMin) {
            y = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (transform.localPosition.z > zMax) {
            z = Random.Range(-m_maxSpeed, 0.0f);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (transform.localPosition.z < zMin) {
            z = Random.Range(0.0f, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        if (m_time > 1.0f) {
            x = Random.Range(-m_maxSpeed, m_maxSpeed);
            y = Random.Range(-m_maxSpeed, m_maxSpeed);
            z = Random.Range(-m_maxSpeed, m_maxSpeed);
            m_angle = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, m_angle, 0);
            m_time = 0.0f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z + z);
        Debug.Log("transform.localPosition = " + transform.localPosition);
        Debug.Log(x);
        Debug.Log(y);
        Debug.Log(z);
    }

    void ChangeDirectionTo() {

    }

    private void OnCollisionStay(Collision collision) {
        if (collision.transform.tag == stringMosquito) {
            //playerInRange = true;

            print("hit the mosquito collider.");

            StartCoroutine(Eat());
        }
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