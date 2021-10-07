using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    [Range(1, 10)]
    public float SmoothFactor;
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 TargetPosition = Target.position + Offset;
        Vector3 SmoothedCam = Vector3.Lerp(transform.position, TargetPosition, SmoothFactor * Time.fixedDeltaTime);
        transform.position = TargetPosition;

    }
}
