using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State m_currentState;

	public CreatureStats creatureStats;
	public Transform eyes;

    /// <summary>
    /// Added after creating TransitionToState(State nextState).
    /// </summary>
    public State m_remainState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
	//[HideInInspector] public Complete.TankShooting tankShooting;
	[HideInInspector] public List<Transform> wayPointList;

    /// <summary>
    /// This variable was added after creating PatrolAction.cs
    /// </summary>
    [HideInInspector] public int nextWaypoint;

    /// <summary>
    /// This variable was added after creating LookDecision.cs
    /// </summary>
    [HideInInspector] public Transform chaseTarget;

    /// <summary>
    /// This variable was added after creating CheckIfCountdownElapsed(float duration) function
    /// </summary>
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


	void Awake () 
	{
		//tankShooting = GetComponent<Complete.TankShooting> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
	{
		wayPointList = wayPointsFromTankManager;
		aiActive = aiActivationFromTankManager;
		if (aiActive) 
		{
			navMeshAgent.enabled = true;
		} else 
		{
			navMeshAgent.enabled = false;
		}
	}

    private void Update() {
        if (!aiActive)
            return;

        /// if AI is active, call this and pass `this`, which is the reference to the state controller.
        m_currentState.UpdateState(this);
    }

    private void OnDrawGizmos() {
        if (m_currentState != null && eyes != null) {
            Gizmos.color = m_currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, creatureStats.lookSphereCastRadius);
        }
    }

    /// <summary>
    /// Added after creating CheckTransitions(StateController controller) in State.cs.
    /// This is the logic based on the results of the decision, to 
    /// cause the state controller to transition to a new state.
    /// </summary>
    /// <param name="nextState"></param>
    public void TransitionToState(State nextState) {
        /// Check if it's time to transition to a new state
        if (nextState != m_remainState) {
            m_currentState = nextState;
            OnExitState();
        }
    }

    /// <summary>
    /// This method was added after creating ActiveStateDecision.cs
    /// </summary>
    public bool CheckIfCountdownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;

        return (stateTimeElapsed >= duration);
    }

    /// resets the timer from CheckIfCountdownElapsed(float duration)
    private void OnExitState() {
        stateTimeElapsed = 0;
    }

}
