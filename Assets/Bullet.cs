using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 forward;
    private float speed = 20f;
    private bool _isShoot = false;
    private Action EndCallback;
    
    private Vector3[] endPoint;

    private void OnDisable()
    {
        _isShoot = false;
        EndCallback = null;
    }

    void FixedUpdate()
    {
        transform.position += forward * Time.deltaTime * speed;
        if (transform.position.x > endPoint[0].x || transform.position.x < endPoint[1].x || transform.position.z > endPoint[0].z ||
            transform.position.z < endPoint[1].z)
        {
            EndCallback?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void SetBullet(Vector3 startPosition, Quaternion rotation, Vector3 _forward, Vector3[] _limitPoint, Action _endMotion)
    {
        Debug.Log(_forward);
        endPoint = new Vector3[2] {_limitPoint[0],_limitPoint[1]};
        this.transform.position = startPosition;
        this.transform.rotation = rotation;
        forward = _forward;
        EndCallback = _endMotion;
        gameObject.SetActive(true);
    }
}
