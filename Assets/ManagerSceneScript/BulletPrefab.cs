using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    private Vector3 forward;
    private float speed = 20f;
    private Action<BulletPrefab> returnObjectPoolAction;
    private Bounds bounds;

    private Vector3[] endPoint;
    
    private Ray _pivotRay;
    private RaycastHit _raycastHit;
    private int layerMask;
    private Vector3 _rayOffset;

    private GameObject ObjObstacle;
    private Action<GameObject> objectRecycle;
    private float distance;
    private float time;
    
    private bool ismove = false;

    private void Start()
    {
        _rayOffset = new Vector3(0, 0, 5);
        _pivotRay = new Ray();
        _pivotRay.direction = Vector3.forward;
        layerMask = 1 << LayerMask.NameToLayer("Obstacle");
    }

    private void GetObstacle()
    {
        _pivotRay.origin = transform.position + transform.rotation * _rayOffset;
        if (Physics.Raycast(_pivotRay, out _raycastHit, 100f, layerMask))
        {
            Debug.Log("물체와의 거리 : " +  _raycastHit.distance);
            ObjObstacle = _raycastHit.transform.gameObject;
            distance = _raycastHit.distance;
            StartCoroutine(MoveObject());
        }
        else
        {
            StartCoroutine(BulletMove());
        }
        ismove = true;
    }

    // private void OnEnable()
    // {
    //     GetObstacle();
    // }

    private void OnDisable()
    {
        returnObjectPoolAction = null;
        ismove = false;
        objectRecycle = null;
    }

    void FixedUpdate()
    {
        if(ismove)
            transform.position += forward * Time.deltaTime * speed;
    }

    public void SetBullet(Vector3 startPosition, Quaternion rotation, Action<BulletPrefab> ReturnpoolAction, Bounds Bounds, Action<GameObject> ObstacleRecycle)
    {
        this.transform.position = startPosition;
        this.transform.rotation = rotation;
        forward = rotation * Vector3.forward;
        returnObjectPoolAction = ReturnpoolAction;
        bounds = Bounds;
        objectRecycle = ObstacleRecycle;
        gameObject.SetActive(true);
        GetObstacle();
    }

    IEnumerator BulletMove()
    {
        yield return new WaitUntil(() =>
            bounds.Contains(this.gameObject.transform.position) == false
        );
        returnObjectPoolAction.Invoke(this);
        gameObject.SetActive(false);
    }

    IEnumerator MoveObject()
    {
        time = distance / speed;

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        returnObjectPoolAction.Invoke(this);
        objectRecycle.Invoke(ObjObstacle);
        gameObject.SetActive(false);
    }
}