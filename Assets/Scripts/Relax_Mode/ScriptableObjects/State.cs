using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public Action[] m_actionsArray;

    /// <summary>
    /// Added after Transition.cs was created.
    /// The transitions contain decisions, but they are not themselves the decisions.
    /// </summary>
    public Transition[] m_transitionsArray;

    public Color sceneGizmoColor = Color.grey;

    /// <summary>
    /// Each time UpdateState() is called, it will evaluate each of the actions & decisions when it's added.
    /// </summary>
    /// <param name="controller"></param>
    public void UpdateState(StateController controller) {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller) {
        for (int i = 0; i < m_actionsArray.Length; i++) {
            m_actionsArray[i].Act(controller);
        }
    }

    /// <summary>
    /// Added after creating Transition.cs.
    /// This is the logic based on the results of the decision, to 
    /// cause the state controller to transition to a new state.
    /// </summary>
    /// <param name="controller"></param>
    private void CheckTransitions(StateController controller) {
        for (int i = 0; i < m_transitionsArray.Length; i++) {
            bool decisionSucceeded = m_transitionsArray[i].decision.Decide(controller);

            if (decisionSucceeded) {
                controller.TransitionToState(m_transitionsArray[i].trueState);
            } else {
                controller.TransitionToState(m_transitionsArray[i].falseState);
            }
        }


    }

}
