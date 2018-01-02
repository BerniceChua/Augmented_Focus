using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWireMeshGizmo : MonoBehaviour {

    Mesh m_thisMesh { get { return GetComponent<Mesh>(); } set { value = m_thisMesh; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireMesh(m_thisMesh);
    }

}
