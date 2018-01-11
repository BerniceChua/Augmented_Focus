using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour {

    [SerializeField] GameObject m_itemToSpawn;

    [SerializeField] Camera m_camera;

    [SerializeField] Button m_foodButton;

    Ray ray;
    RaycastHit hit;

    int m_targetCounter = 0;

    // Use this for initialization
    void Start() {
        m_foodButton.onClick.AddListener(OnButtonDown);
    }

    //// Update is called once per frame
    //void Update () {

    //}

    public void OnButtonDown() {
        Debug.Log("Clicked on the button.");
        //m_navMeshAgent.isOnNavMesh
        //m_targetCube;
        GameObject spawnedTarget;
        spawnedTarget = Instantiate(m_itemToSpawn, m_camera.transform.forward, m_camera.transform.rotation);
        spawnedTarget.name = (spawnedTarget.name + TargetCounterAddToName());

        FoodInTheScene.AddToFoodList(spawnedTarget);
    }

    public string TargetCounterAddToName() {
        m_targetCounter++;
        return m_targetCounter.ToString();
    }

}
