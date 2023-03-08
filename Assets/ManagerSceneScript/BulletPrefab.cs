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
    
    RaycastHit hit;

    private int layerMask;

    private Action<GameObject> objectRecycle;
    private float distance;
    private float time;
    
    private bool ismove = false;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Obstacle");
    }

    private void OnDisable()
    {
        returnObjectPoolAction = null;
        ismove = false;
        objectRecycle = null;
    }

    void Update()
    {
        transform.position += forward * Time.deltaTime * speed;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.deltaTime, layerMask))
        {
            returnObjectPoolAction.Invoke(this);
            objectRecycle.Invoke(hit.collider.gameObject);
            gameObject.SetActive(false);
        }
        else if (!bounds.Contains(gameObject.transform.position))
        {
            returnObjectPoolAction.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void SetBullet(Vector3 StartPosition, Quaternion Rotation, Action<BulletPrefab> ReturnpoolAction, Bounds Bounds, Action<GameObject> ObstacleRecycle)
    {
        transform.position = StartPosition;
        transform.rotation = Rotation;
        forward = Rotation * Vector3.forward;
        returnObjectPoolAction = ReturnpoolAction;
        bounds = Bounds;
        objectRecycle = ObstacleRecycle;
        gameObject.SetActive(true);
    }
}