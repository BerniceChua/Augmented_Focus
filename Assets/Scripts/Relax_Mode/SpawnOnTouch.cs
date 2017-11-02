using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnTouch : MonoBehaviour {

    public GameObject itemToSpawn;
    private GameObject itemToSpawnInstance;

    [SerializeField] Camera m_camera;

    [SerializeField] Button m_foodButton;

    [SerializeField] Critter m_critter;

    [SerializeField] string[] m_critterTypesNamesArray;

    [SerializeField] GameObject[] m_crittersThatEatThisCritterArray;

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        m_foodButton.onClick.AddListener(OnButtonDown);
    }

    // Update is called once per frame
    void Update() {
        //if (Input.touchCount == 1) {
        //    Touch touch = Input.touches[0];
        //    Ray ray = Camera.main.ScreenPointToRay(touch.position);

        //    switch (touch.phase) {
        //        case TouchPhase.Began:
        //            itemToSpawnInstance = (GameObject)Instantiate(itemToSpawn, Vector3.zero, Quaternion.identity);
        //            break;
        //        case TouchPhase.Moved:
        //            // track touch movement and doStuff here
        //            break;
        //        case TouchPhase.Ended:
        //            // track touch lifted off screen and doStuff here
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //for (int i = 0; i < Input.touchCount; i++) {
        //    if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
        //        Touch touch = Input.touches[0];
        //        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));



        //        Vector3 p = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
        //        Debug.Log(p);

        //        if (Physics.Raycast(ray)) {
        //            itemToSpawnInstance = (GameObject)Instantiate(itemToSpawn, p, Quaternion.identity);
        //        }

        //        //Touch touchPos = Input.GetTouch(0).position;
        //        //Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Vector3(touchPos.x, touchPos.y, 0));

        //        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)))) {
        //            Instantiate(itemToSpawn, new Vector3(p.x, p.y, 0), Quaternion.identity);
        //        }
        //    }
        //}

        //print("test");
        //if (Input.GetMouseButtonDown(0)) {
        //    ray = m_camera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit)) {
        //        print("hello!!!");
        //        //GameObject obj = Instantiate(itemToSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
        //        //Instantiate(itemToSpawn, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);

        //        itemToSpawnInstance = (GameObject)Instantiate(itemToSpawn, Vector3.zero, Quaternion.identity);
        //    }

        //}

        ////if (Input.touchCount == 1) {
        //if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
        //    Touch touch = Input.GetTouch(0);

        //    print("I'm inside the if");

        //    Vector2 touchPos = m_camera.ScreenToWorldPoint(touch.position);

        //    if (touch.phase == TouchPhase.Began) {
        //        Instantiate(itemToSpawn, touchPos, Quaternion.identity);
        //    }
        //}

        //if (Input.touchCount == 1) {
        //    Touch touch = Input.touches[0];
        //    Ray ray = Camera.main.ScreenPointToRay(touch.position);

        //    if (touch.phase == TouchPhase.Began) {
        //        itemToSpawnInstance = (GameObject)Instantiate(itemToSpawn, Vector3.zero, Quaternion.identity);
        //    }
        //}
    }

    public void OnButtonDown() {
        print("Clicked OnTheButton().");

        //GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
        itemToSpawnInstance = Instantiate(itemToSpawn);
        Rigidbody rb = itemToSpawn.GetComponent<Rigidbody>();
        itemToSpawnInstance.transform.rotation = m_camera.transform.rotation;
        itemToSpawnInstance.transform.position = m_camera.transform.forward;
        rb.AddForce(m_camera.transform.forward * 100.0f);
        //Destroy(bullet, 3);
        //string thisTag = this.transform.gameObject.tag;
        string thisTag = itemToSpawnInstance.gameObject.tag;
        //string thisTag = tag;
        Debug.Log("thisTag = " + thisTag);
        //Debug.Log(itemToSpawnInstance.GetComponent<Critter>().critterType);

        //if (crittersByType == null) {
        //    crittersByType = new Dictionary<string, List<Critter>>();
        //}
        //if (crittersByType.ContainsKey(critterType) == false) {
        //    crittersByType[critterType] = new List<Critter>();
        //}
        //Critter.crittersByType[critterType].Add(this);

        if (Critter.crittersByType == null) {
            Critter.crittersByType = new Dictionary<string, List<Critter>>();
            Debug.Log("Inside if (Critter.crittersByType == null)");
        }
        if (Critter.crittersByType.ContainsKey(thisTag) == false) {
            Critter.crittersByType[thisTag] = new List<Critter>();
            Debug.Log("Inside if (Critter.crittersByType.ContainsKey(thisTag) == false)");
        }
        Critter.crittersByType[thisTag].Add(itemToSpawnInstance.GetComponent<Critter>());
        //Debug.Log(itemToSpawnInstance.GetComponent<Critter>());
        Debug.Log(itemToSpawnInstance.GetComponent<Critter>().GetType());
        Debug.Log(itemToSpawnInstance.GetType());
        Debug.Log(Critter.crittersByType.Count);


        AddThisToArrayOfDesiredOrWeightedDirections();
        
    }

    void AddThisToArrayOfDesiredOrWeightedDirections() {
        for (int i = 0; i < m_critterTypesNamesArray.Length; i++) {
            if (Critter.crittersByType.ContainsKey(m_critterTypesNamesArray[i]) == false) {
                // We have nothing to eat!
                return;
            }

            // Find the closest edible critter to us.
            Critter closest = null;
            //GameObject closestGameObject = null;
            float dist = Mathf.Infinity;

            foreach (Critter c in Critter.crittersByType[m_critterTypesNamesArray[i]]) {
                Debug.Log("critter c in foreach of AI_SeekFood = " + c);
                if (c.health <= 0) {
                    // This is already dead, ignore it.
                    continue;
                }

                //float d = Vector2.Distance(this.transform.position, c.transform.position);
                float d = Vector3.Distance(this.transform.position, c.transform.position);

                if (closest == null || d < dist) {
                    closest = c;
                    dist = d;
                }

            }




            if (closest == null) {
                // No valid food targets exist.
                return;
            }

            if (dist < m_crittersThatEatThisCritterArray[i].GetComponent<AI_SeekFood>().eatingRange) {
                float hpEaten = Mathf.Clamp(m_crittersThatEatThisCritterArray[i].GetComponent<AI_SeekFood>().eatHPPerSecond * Time.deltaTime, 0, closest.health);
                closest.health -= hpEaten;
                m_critter.energy += hpEaten * m_crittersThatEatThisCritterArray[i].GetComponent<AI_SeekFood>().eatHP2Energy;
            } else {
                // Now we want to move towards this closest edible critter

                // Not a direction, it's the total distance of x & y (& possibly z) between the 2.
                //Vector2 dir = closest.transform.position - this.transform.position;
                Vector3 dir = closest.transform.position - this.transform.position;

                WeightedDirection wd = new WeightedDirection(dir, 5);

                m_crittersThatEatThisCritterArray[i].GetComponent<AI_SeekFood>().DoAIBehaviour();
            }

            foreach (Critter c in Critter.crittersByType[m_critterTypesNamesArray[i]]) {
                c.AddToDesiredWeightedDirections();
            }

        }


    }

}