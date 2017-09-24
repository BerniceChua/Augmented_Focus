using UnityEngine;
using System.Collections;

public class BarnOwlCharacterScript : MonoBehaviour {
	public Animator barnOwlAnimator;
	public float barnOwlSpeed=1f;
	Rigidbody barnOwlRigid;
	public bool isFlying=false;
	public float upDown=0f;
	public float forwardAcceleration=0f;
	public float yawVelocity=0f;
	public float groundCheckDistance=5f;
	public bool soaring=false;
	public bool isGrounded=true;
	public float forwardSpeed=0f;
	public float maxForwardSpeed=3f;
	public float meanForwardSpeed=1.5f;
	public float speedDumpingTime=.1f;

	void Start(){
		barnOwlAnimator = GetComponent<Animator> ();
		barnOwlAnimator.speed = barnOwlSpeed;
		barnOwlRigid = GetComponent<Rigidbody> ();
	}	

	void Update(){
		Move ();
		if (barnOwlAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "Hover1" ) {
			if(soaring){
				soaring=false;
				forwardAcceleration=0f;
				forwardSpeed=meanForwardSpeed;
				barnOwlAnimator.SetBool ("IsSoaring", false);
				barnOwlAnimator.applyRootMotion = false;
				isFlying = true;
			}
		}
		GroundedCheck ();
	}

	void GroundedCheck(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, groundCheckDistance)) {
			if (!soaring) {
				Landing ();
				isGrounded = true;
			}
		} else {
			isGrounded=false;
		}
	}

	public void SpeedSet(float animSpeed){
		barnOwlAnimator.speed = animSpeed;
	}

	public void Landing(){
		barnOwlAnimator.SetBool ("Landing",true);
		barnOwlAnimator.applyRootMotion = true;
		barnOwlRigid.useGravity = true;
		isFlying = false;
	}
	
	public void Soar(){
		if(isGrounded){
			barnOwlAnimator.SetBool ("Landing",false);
			barnOwlAnimator.SetBool ("IsSoaring", true);
			barnOwlRigid.useGravity = false;
			soaring = true;
			isGrounded = false;
		}
	}

	public void Attack(){
		barnOwlAnimator.SetTrigger ("Attack");
	}
	
	public void Move(){
		barnOwlAnimator.SetFloat ("Forward",forwardAcceleration);
		barnOwlAnimator.SetFloat ("Turn",yawVelocity);
		barnOwlAnimator.SetFloat ("UpDown",upDown);
	//	barnOwlAnimator.SetFloat ("Angularvelocity",barnOwlRigid.angularVelocity.y);
		barnOwlAnimator.SetFloat ("UpVelocity",barnOwlRigid.velocity.y);

		if(isFlying ) {
			if(forwardAcceleration<0f){
				barnOwlRigid.velocity=transform.up*upDown+transform.forward*forwardSpeed;
			}else{
				barnOwlRigid.velocity=transform.up*(upDown+(forwardSpeed-meanForwardSpeed))+transform.forward*forwardSpeed;
			}
			transform.RotateAround(transform.position,Vector3.up,Time.deltaTime*yawVelocity*100f);

			forwardSpeed=Mathf.Lerp(forwardSpeed,0f,Time.deltaTime*speedDumpingTime);
			forwardSpeed=Mathf.Clamp( forwardSpeed+forwardAcceleration*Time.deltaTime,0f,maxForwardSpeed);
			upDown=Mathf.Lerp(upDown,0,Time.deltaTime*3f);	
		}
	}
}
