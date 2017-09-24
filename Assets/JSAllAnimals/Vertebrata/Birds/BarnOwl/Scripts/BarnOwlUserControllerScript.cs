using UnityEngine;
using System.Collections;

public class BarnOwlUserControllerScript : MonoBehaviour {

	public BarnOwlCharacterScript barnOwlCharacter;
	public float upDownInputSpeed=3f;


	void Start () {
		barnOwlCharacter = GetComponent<BarnOwlCharacterScript> ();	
	}

	void Update(){
		if (Input.GetButtonDown ("Jump")) {
			barnOwlCharacter.Soar ();
		}

		if (Input.GetButtonDown ("Fire1")) {
			barnOwlCharacter.Attack ();
		}
		if (Input.GetKey (KeyCode.N)) {
			barnOwlCharacter.upDown=Mathf.Clamp(barnOwlCharacter.upDown-Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
		if (Input.GetKey (KeyCode.U)) {
			barnOwlCharacter.upDown=Mathf.Clamp(barnOwlCharacter.upDown+Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
	}
	
	void FixedUpdate(){
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");	
		barnOwlCharacter.forwardAcceleration = v;
		barnOwlCharacter.yawVelocity = h;

	}
}
