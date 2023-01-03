using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 2, -5f);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(offset);
        transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, 0.02f);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.01f);
    }

    public void SetCurrentOffset(Vector3 _offset)
    {
        offset = _offset;
    }
}
