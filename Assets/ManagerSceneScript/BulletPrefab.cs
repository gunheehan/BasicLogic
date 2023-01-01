using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    private Vector3 forward;
    private float speed = 20f;
    private Action returnObjectPoolAction;

    private Vector3[] endPoint;

    private void OnDisable()
    {
        returnObjectPoolAction = null;
    }

    void FixedUpdate()
    {
        transform.position += forward * Time.deltaTime * speed;
    }

    public void SetBullet(Vector3 startPosition, Quaternion rotation, Action _returnpoolAction, Bounds _bounds)
    {
        this.transform.position = startPosition;
        this.transform.rotation = rotation;
        forward = rotation * Vector3.forward;
        returnObjectPoolAction = _returnpoolAction;
        gameObject.SetActive(true);
        StartCoroutine(BulletMove(_bounds));
    }

    IEnumerator BulletMove(Bounds _bounds)
    {
        yield return new WaitUntil(() =>
            _bounds.Contains(this.gameObject.transform.position) == false
        );
        returnObjectPoolAction.Invoke();
        gameObject.SetActive(false);
    }
}