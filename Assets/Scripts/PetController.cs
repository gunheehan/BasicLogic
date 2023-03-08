using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 targetPosition;
    public Transform target;

    private float targetMaxDistance = 3f;
    private float distance;

    void Start()
    {
        offset = new Vector3(1, 0, -3f);
    }

    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        targetPosition = target.position + offset;

        if (distance > targetMaxDistance || target.position.z > targetPosition.z)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
        }
        //RotateObjectToTargetForward();
    }

    private void RotateObjectToTargetForward()
    {
        float angle = Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up);
        transform.Rotate(Vector3.up, angle);
    }
}
