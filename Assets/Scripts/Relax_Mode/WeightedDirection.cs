using UnityEngine;
using System.Collections;

public class WeightedDirection {

    //public readonly Vector2 direction;
    public readonly Vector3 direction;
    public readonly float weight;

    //public WeightedDirection(Vector2 dir, float wgt) {
    public WeightedDirection(Vector3 dir, float wgt) {
        direction = dir.normalized;
        weight = wgt;
    }

    public enum BlendingType { BLEND, EXCLUSIVE, FALLBACK };
    public BlendingType blending = BlendingType.BLEND;  // UNUSED

    public float speed = -1f; // UNUSED

}
