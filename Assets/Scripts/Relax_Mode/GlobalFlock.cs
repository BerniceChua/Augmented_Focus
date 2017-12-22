using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {
    [SerializeField] GameObject m_npcPrefab;
    [SerializeField] GameObject m_goalPrefab;

    [SerializeField] static int m_npcAmount = 1;

    public static int m_areaSize = 5;
    public static GameObject[] m_allNpcs = new GameObject[m_npcAmount];

    public static Vector3 m_goalPosition = Vector3.zero;

    // Use this for initialization
    void Start() {
        //for (int i = 0; i < m_npcAmount; i++) {
        //    Vector3 position = new Vector3(Random.Range(-m_areaSize, m_areaSize), 0.5f, Random.Range(-m_areaSize, m_areaSize));
        //    m_allNpcs[i] = Instantiate(m_npcPrefab, position, Quaternion.identity) as GameObject;
        //}
        Vector3 position = new Vector3(Random.Range(-m_areaSize, m_areaSize), 0.5f, Random.Range(-m_areaSize, m_areaSize));
    }

    // Update is called once per frame
    void Update() {
        if (Random.Range(0, 50000) < 50) {
            m_goalPosition = new Vector3(Random.Range(-m_areaSize, m_areaSize), 0.5f, Random.Range(-m_areaSize, m_areaSize));
        }

        m_goalPrefab.transform.position = m_goalPosition;
    }
}