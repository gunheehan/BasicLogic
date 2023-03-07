using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private Vector3 offset;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(2, 0, -2f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, 0.02f);
        // RotateObjectToTargetForward();
    }

    private void RotateObjectToTargetForward()
    {
        Vector3 delta = transform.position - target.position;

        float angle = Vector3.Angle(target.forward, delta);

        if (Vector3.Cross(target.forward, delta).y < 0)
            angle = -angle;

        transform.rotation = transform.rotation * Quaternion.Euler(0, angle, 0);
    }
}
