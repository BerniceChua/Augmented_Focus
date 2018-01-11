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
    [HideInInspector] public Transform m_FoodTarget;

    /// <summary>
    /// This variable was added after creating CheckIfCountdownElapsed(float duration) function
    /// </summary>
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    [SerializeField] public Animator m_animator;
    bool m_noFood = true;
    string stringMosquito = "Mosquito";


    public float m_sensorLength = 0.05f;


    void Awake () 
	{
		//tankShooting = GetComponent<Complete.TankShooting> ();
		//navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	//public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
	//{
	//	wayPointList = wayPointsFromTankManager;
	//	aiActive = aiActivationFromTankManager;
	//	if (aiActive) 
	//	{
	//		navMeshAgent.enabled = true;
	//	} else 
	//	{
	//		navMeshAgent.enabled = false;
	//	}
	//}

    private void Update() {
        //if (!aiActive)
        //    return;

        /// if AI is active, call this and pass `this`, which is the reference to the state controller.
        m_currentState.UpdateState(this);
    }

    private void OnDrawGizmos() {
        // forward sensor
        Gizmos.DrawRay(transform.position, transform.forward * (m_sensorLength));

        // backward sensor
        Gizmos.DrawRay(transform.position, -transform.forward * (m_sensorLength));


        Gizmos.DrawRay(transform.position, transform.right * (m_sensorLength));
        Gizmos.DrawRay(transform.position, -transform.right * (m_sensorLength));

        if (m_currentState != null && eyes != null) {
            Gizmos.color = m_currentState.sceneGizmoColor;
            //Gizmos.DrawWireSphere(eyes.position, creatureStats.lookSphereCastRadius);
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


    //GameObject DetectFood() {
    //    GameObject[] foods;
    //    foods = GameObject.FindGameObjectsWithTag(stringMosquito);
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject food in foods) {
    //        Vector3 diff = food.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance) {
    //            closest = food;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest;
    //}

    public GameObject DetectFood() {
        Debug.Log("FoodInTheScene.GetNearestInList() = " + FoodInTheScene.GetNearestInList());
        return FoodInTheScene.GetNearestInList();
    }

    public IEnumerator Eat() {
        //animation = GetComponent<Animation>();

        //DetectFood().gameObject.SetActive(false);
        //yield return new WaitForSeconds(animation.clip.length);
        yield return new WaitForSeconds(3);
        Debug.Log("I am eating the food!");
        m_animator.Play("Attack");
        m_noFood = false;
        Debug.Log("I ate the food!");
        yield return new WaitForSeconds(5);
        Destroy(DetectFood());
        m_noFood = true;
    }

}
