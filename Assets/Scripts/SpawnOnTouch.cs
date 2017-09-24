using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnTouch : MonoBehaviour {

    public GameObject itemToSpawn;
    private GameObject itemToSpawnInstance;

    [SerializeField] Camera m_camera;

    [SerializeField] Button m_foodButton;

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
        print("hello");

        //GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
        itemToSpawnInstance = Instantiate(itemToSpawn);
        Rigidbody rb = itemToSpawn.GetComponent<Rigidbody>();
        itemToSpawnInstance.transform.rotation = m_camera.transform.rotation;
        itemToSpawnInstance.transform.position = m_camera.transform.forward;
        rb.AddForce(m_camera.transform.forward * 100.0f);
        //Destroy(bullet, 3);
    }

}