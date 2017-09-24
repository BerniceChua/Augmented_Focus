using UnityEngine;
using System.Collections;

public class CeratocanthusCharacter : MonoBehaviour {
	Animator ceratocanthusAnimator;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset = 0.01f;
	public bool isGrounded;
	public bool isFlying = false;
	public bool isBalling=false;
	public float forward=0f;
	public float turn=0f;
	public float maxFlyForwardSpeed=10f;
	public float maxFlyTurnSpeed=3f;
	public float maxFlyUpDownSpeed=3F;
	public PhysicMaterial ceratocanthusPhysicMat;
	Collider ceratocanthusCol;
	Collider[] sphereColliders;
	public bool overturned=false;
	public GameObject ceratocanthusGyro;
	Rigidbody ceratocanthusRigid;
	public bool isLived=true;
	public float getUpSpeed=10f;

	void Start()
	{
		ceratocanthusGyro = new GameObject ("ceratocanthusGyro");
		ceratocanthusAnimator = GetComponent<Animator>();
		ceratocanthusRigid = GetComponent<Rigidbody>();
		ceratocanthusCol = GetComponent<SphereCollider> ();
		ceratocanthusPhysicMat = new PhysicMaterial ();
		ceratocanthusPhysicMat.dynamicFriction = 0f;
		ceratocanthusPhysicMat.staticFriction = 0f;
		ceratocanthusCol.material = ceratocanthusPhysicMat;
		sphereColliders=GetComponentsInChildren<SphereCollider>();
	}
	
	void FixedUpdate()
	{
		if(isLived){
			Move ();
			if(!isBalling){
				CheckGroundStatus();
			}
		}
		ceratocanthusGyro.transform.position = transform.position;
		if (Vector3.Dot (ceratocanthusGyro.transform.forward, transform.right) > 0f) {
			ceratocanthusGyro.transform.RotateAround (ceratocanthusGyro.transform.position, Vector3.up, -Time.deltaTime*100f);
		} else {
			ceratocanthusGyro.transform.RotateAround (ceratocanthusGyro.transform.position, Vector3.up, Time.deltaTime*100f);
		}
	}
	
	public void Attack()
	{
		ceratocanthusAnimator.SetTrigger("Attack");
	}
	
	public void Hit()
	{
		ceratocanthusAnimator.SetTrigger("Hit");
	}
	
	public void Eat()
	{
		ceratocanthusAnimator.SetTrigger("Eat");
	}
	
	public void Death()
	{
		ceratocanthusAnimator.SetBool("IsLived",false);
		ceratocanthusAnimator.SetBool("IsBall",false);
		ceratocanthusAnimator.SetBool("IsFlying",false);

		isFlying = false;
		isBalling = false;
		isLived = false;
		ceratocanthusAnimator.applyRootMotion = false;	
		ceratocanthusPhysicMat.staticFriction = 0f;
		ceratocanthusPhysicMat.dynamicFriction = 0f;
		ceratocanthusRigid.drag = 2f;		
		ceratocanthusRigid.angularDrag = 2f;
		ceratocanthusRigid.useGravity = true;
		ceratocanthusRigid.constraints=	RigidbodyConstraints.None;	
	}
	
	public void Rebirth()
	{
		ceratocanthusAnimator.SetBool("IsLived",true);
		ceratocanthusAnimator.applyRootMotion = true;	
		isFlying = false;
		isBalling = false;
		isLived = true;

	}

	public void BallStart()
	{
		if(!isFlying && isLived){
			ceratocanthusAnimator.SetBool("IsBall",true);
			ceratocanthusRigid.constraints=	RigidbodyConstraints.None;	
			ceratocanthusAnimator.applyRootMotion = false;
			ceratocanthusPhysicMat.staticFriction = 1f;
			ceratocanthusPhysicMat.dynamicFriction = 1f;
			ceratocanthusRigid.drag = 0f;
			ceratocanthusRigid.angularDrag = 0f;
			isBalling = true;
			foreach(Collider col in sphereColliders){
				col.enabled=false;
			}
			ceratocanthusCol.enabled=true;
		}

	}
	
	public void BallEnd()
	{
		if (!isFlying && isLived) {
			ceratocanthusAnimator.SetBool ("IsBall", false);
			ceratocanthusAnimator.applyRootMotion = true;	
			ceratocanthusPhysicMat.staticFriction = 0f;
			ceratocanthusPhysicMat.dynamicFriction = 0f;
			ceratocanthusRigid.drag = 2f;		
			ceratocanthusRigid.angularDrag = 2f;
			isBalling = false;
		}

		foreach(Collider col in sphereColliders){
			col.enabled=true;
		}
		ceratocanthusCol.enabled=false;
	}
	
	public void Landing()
	{
		if (isFlying && isLived)
		{
			ceratocanthusAnimator.SetBool("IsFlying",false);
			isFlying = false;
			ceratocanthusAnimator.applyRootMotion = true;
			ceratocanthusRigid.useGravity = true;
			ceratocanthusRigid.constraints=	RigidbodyConstraints.None;	
		}
	}

	public void Soar()
	{
		
		if (!isBalling && isLived)
		{
			ceratocanthusAnimator.SetBool("IsFlying",true);
			isFlying = true;
			ceratocanthusAnimator.applyRootMotion = false;
			ceratocanthusRigid.useGravity=false;
			ceratocanthusRigid.constraints=	RigidbodyConstraints.None;	
		}
		
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hit;
		isGrounded = Physics.Raycast(transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hit, groundCheckDistance);
	
		
		if (isGrounded)
		{
			ceratocanthusAnimator.applyRootMotion = true;

		}
		else
		{
			ceratocanthusAnimator.applyRootMotion = false;
		}


		if (isGrounded || isFlying) {
			float mag;
			if(isFlying){
				mag=Vector3.Cross(transform.up,Vector3.up).sqrMagnitude;
			}else{
				mag=Vector3.Cross(transform.up,hit.normal).sqrMagnitude;
			}

			if(overturned){
				if(transform.up.y>0f){
					transform.rotation=Quaternion.Lerp(transform.rotation,ceratocanthusGyro.transform.rotation,getUpSpeed*Time.deltaTime);
					if(mag<.01f){
						overturned=false;
						if(!isFlying){
							ceratocanthusRigid.constraints=RigidbodyConstraints.FreezeRotation;
							ceratocanthusAnimator.applyRootMotion = true;
						}
					}
				}else{
					transform.rotation=Quaternion.Lerp(transform.rotation,ceratocanthusGyro.transform.rotation,getUpSpeed*Time.deltaTime);
				}
			}else if(mag>.02f || transform.up.y<0f){
				overturned=true;
				ceratocanthusRigid.constraints=RigidbodyConstraints.None;
			}
		} else {
			ceratocanthusAnimator.applyRootMotion=false;
			ceratocanthusRigid.constraints=RigidbodyConstraints.None;
			overturned=true;
		}		
	}
	
	public void Move()
	{
		ceratocanthusAnimator.SetFloat("Forward", forward);
		ceratocanthusAnimator.SetFloat("Turn", turn);
		
		if (isFlying) {
			ceratocanthusRigid.AddForce (transform.forward * maxFlyForwardSpeed + transform.up * forward*maxFlyUpDownSpeed);
			ceratocanthusRigid.AddTorque (transform.up * turn*maxFlyTurnSpeed);
		}
	}
}
