using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleInitController : MonoBehaviour
{
    private Ray _pivotRay;
    private RaycastHit _raycastHit;
    private int layerMask;
    private Vector3 _rayOffset;

    private ObstacleState ObstaclePrefab;
    private Bounds BoundsPlane;

    private int initPrefabCount = 10;
    
    private void Start()
    {
        _rayOffset = new Vector3(0, 0, 5);
        _pivotRay = new Ray();
        _pivotRay.direction = Vector3.down;
        layerMask = 1 << LayerMask.NameToLayer("Plane");

        GetPlaneBounds();
    }

    private void OnEnable()
    {
        GetPlaneBounds();
    }

    private void GetPlaneBounds()
    {
        _pivotRay.origin = transform.position + transform.rotation * _rayOffset;
        if (Physics.Raycast(_pivotRay, out _raycastHit, 100f, layerMask))
        {
            Collider collider = _raycastHit.collider;
            BoundsPlane = collider.bounds;
        }

        SetObstaclePrefab();
    }

    private void SetObstaclePrefab()
    {
        ObstaclePrefab = Resources.Load<ObstacleState>("Prefabs/ObstaclePrefab");
        Vector3 randomPosision = Vector3.zero;
        for (int i = 0; i < initPrefabCount; i++)
        {
            ObstacleState obstacle = Instantiate(ObstaclePrefab,this.transform);
            randomPosision.x = Random.Range(BoundsPlane.min.x, BoundsPlane.max.x);
            randomPosision.z = Random.Range(BoundsPlane.min.z, BoundsPlane.max.z);

            obstacle.gameObject.transform.position = randomPosision;

            if(!obstacle.CheckPlacedClear())
                Debug.Log("오브젝트 겹침");
        }
    }
}
