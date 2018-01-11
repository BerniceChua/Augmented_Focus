using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodInTheScene {

    public static List<GameObject> m_TargetsList = new List<GameObject>();

    public static void AddToFoodList(GameObject targetWithThisName) {
        // Add target to the ArrayList.
        m_TargetsList.Add(targetWithThisName);
        Debug.Log(targetWithThisName + " has been added to the List.");
        //Debug.Log("CountThingsInArrayList() = " + CountThingsInArrayList());
    }

    public static void RemoveFromFoodList(GameObject targetWithThisName) {
        // Remove target from the ArrayList.
        m_TargetsList.Remove(targetWithThisName);
        Debug.Log(targetWithThisName + " has been removed from the List.");
        //Debug.Log("CountThingsInArrayList() = " + CountThingsInArrayList());
    }

    public static int CountThingsInFoodList() {
        if (m_TargetsList.Count == 0) {
            Debug.Log("m_TargetsList.Count is 0");
            return 0;
        } else {
            // Used for debugging
            Debug.Log("m_TargetsList.Count = " + m_TargetsList.Count);
            return m_TargetsList.Count;
        }
    }

    public static void IterateThroughFoodList() {

    }

    public static GameObject GetNearestInList() {
        Debug.Log("Inside GetNearestInList");
        float dist = Mathf.Infinity;
        //foreach (GameObject target in m_TargetsList) {
        //    if (target.transform.position.sqrMagnitude < nearest.transform.position.sqrMagnitude) {
        //        nearest = target;
        //        Debug.Log("nearest = " + nearest);
        //    }
        //}
        if (CountThingsInFoodList() == 0)
            return null;
        else {
            GameObject nearest = m_TargetsList[0];
            Debug.Log("inside the else of countthingsinfoodlist()");
            for (int i = 0; i < m_TargetsList.Count; i++) {
                if (m_TargetsList[i].activeSelf == false) {
                    continue;
                }

                float distanceBetween = Vector3.Distance(m_TargetsList[i].transform.position, nearest.transform.position);

                if (nearest == null || distanceBetween < dist) {
                    nearest = m_TargetsList[i];
                    dist = distanceBetween;
                }

                if (m_TargetsList[i].transform.position.sqrMagnitude < nearest.transform.position.sqrMagnitude) {
                    nearest = m_TargetsList[i];
                    Debug.Log("nearest = " + nearest);
                }
            }
            return nearest;
        }
    }
}
