using UnityEngine;
using System.Collections;

public class CeratocanthusUserController : MonoBehaviour {
	CeratocanthusCharacter ceratocanthusCharacter;
	
	void Start()
	{
		ceratocanthusCharacter = GetComponent<CeratocanthusCharacter>();
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			ceratocanthusCharacter.Attack();
		}
		
		if (Input.GetButtonDown("Jump"))
		{
			ceratocanthusCharacter.Soar();
		}
		
		if (Input.GetKeyDown(KeyCode.L))
		{
			ceratocanthusCharacter.Landing();
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			ceratocanthusCharacter.Hit();
		}
		
		if (Input.GetKeyDown(KeyCode.E))
		{
			ceratocanthusCharacter.Eat();
		}
		
		if (Input.GetKeyDown(KeyCode.K))
		{
			ceratocanthusCharacter.Death();
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			ceratocanthusCharacter.Rebirth();
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			ceratocanthusCharacter.BallStart();
		}
		
		if (Input.GetKeyUp(KeyCode.B))
		{
			ceratocanthusCharacter.BallEnd();
		}	
	}
	
	private void FixedUpdate()
	{
		ceratocanthusCharacter.turn = Input.GetAxis("Horizontal");
		ceratocanthusCharacter.forward= Input.GetAxis ("Vertical");
	}
}
