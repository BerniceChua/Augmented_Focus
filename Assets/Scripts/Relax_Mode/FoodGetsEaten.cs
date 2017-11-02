using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGetsEaten : MonoBehaviour {

    [SerializeField] float m_radius = 2f;
    [SerializeField] LayerMask m_layerMask;

    [SerializeField] Animator m_animator;

    //[SerializeField] ta

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void LateUpdate() {
        //if (Physics.OverlapSphere(transform.position, m_radius, m_layerMask, QueryTriggerInteraction.Collide) == collision.transform.t stringMosquito) {
        //    m_animator.Play("Attack");
        //}
    }

}
