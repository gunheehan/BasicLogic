using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterFire : MonoBehaviour
{
    private Bullet bulletPrefab;
    private Stack<Bullet> pool_bullet;
    private int magazine = 10;
    
    public Bounds _bounds = new Bounds();
    private Vector3[] Localvertices;
    private Vector3[] Globalvertices;
    [SerializeField]private Transform Rect_Plane;
    
    private Ray _pivotRay;
    private RaycastHit _raycastHit;
    private int layerMask;
    private Vector3 _rayOffset;

    private bool isEnableRay = false;

    void Start()
    {
        init();
        _bounds.center = Vector3.zero;
        _bounds.size = new Vector3(200f, 5f, 200f);
    }

    private void init()
    {
        _rayOffset = new Vector3(0, 0, 5);
        _pivotRay = new Ray();
        _pivotRay.direction = Vector3.down;
        layerMask = 1 << LayerMask.NameToLayer("Plane");
        isEnableRay = true;

         bulletPrefab = Resources.Load<Bullet>("Prefabs/JoyStick");
         SetPlaneEdgePoint();
         SetBulletPool();
    }

    private void Update()
    {
        if (isEnableRay)
            RayJudgment();
    }

    private void RayJudgment()
    {
        _pivotRay.origin = transform.position + transform.rotation * _rayOffset;
        if (Physics.Raycast(_pivotRay, out _raycastHit, 30f, layerMask))
        {
            Rect_Plane = _raycastHit.transform;
            Debug.Log("레이 판별");
        }

        isEnableRay = false;
    }

    private void SetBulletPool()
    {
        pool_bullet = new Stack<Bullet>();

        for (int i = 0; i < magazine; i++)
        {
            Bullet newBullet = Instantiate(bulletPrefab);
            pool_bullet.Push(newBullet);
        }
    }
    
    private void SetPlaneEdgePoint()
    {
        Localvertices = Rect_Plane.GetComponent<MeshFilter>().sharedMesh.vertices;

        Globalvertices = new Vector3[2]
        {
            Rect_Plane.TransformPoint(Localvertices[0]), Rect_Plane.TransformPoint(Localvertices[120])
        };
    }

    public void Shoot(Transform shootTransform, Quaternion shootQuaternion)
    {
        
    }
    
    private Bullet GetBullet()
    {
        if (pool_bullet.Count > 0)
            return pool_bullet.Pop();
        return null;
    }
}
