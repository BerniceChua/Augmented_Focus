using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/CreatureStats")]
public class CreatureStats : ScriptableObject {

    public float lookSphereCastRadius = 2f;

    public float moveSpeed = 2.0f;
	public float lookRange = 40f;
	//public float lookSphereCastRadius = 1f;

	public float attackRange = 1f;
	public float attackRate = 1f;
	public float attackForce = 15f;
	public int attackDamage = 50;

	public float searchDuration = 4f;
	public float searchingTurnSpeed = 120f;

}
