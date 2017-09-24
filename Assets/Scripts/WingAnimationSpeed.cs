using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAnimationSpeed : MonoBehaviour {
    [SerializeField] float m_speed = 99999.0f;

    Animator m_hummingbirdAnimator { get { return GetComponent<Animator>(); } set { m_hummingbirdAnimator = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GetComponent<Animation>().Play("walk");
        //m_hummingbirdAnimator.speed = m_speed;
    }

    private void OnEnable() {
        m_hummingbirdAnimator.speed = m_speed;
    }
}