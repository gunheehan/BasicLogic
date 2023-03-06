using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundTest : MonoBehaviour
{
    private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField] private Transform target;

    private float distanceFromTarget = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, 0, 40);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
