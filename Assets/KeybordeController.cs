using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.IO;

public class KeybordeController : MonoBehaviour
{
    private float speed = 1f;
    [SerializeField] private GameObject Camera;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private GameObject cube;
    [SerializeField] private Transform Rect_Plane;

    private float height;
    private float radius;
    private Vector3 _rayOffset;
    private Ray _pivotRay;
    private Ray _playerRay;
    private RaycastHit _raycastHit;
    private RaycastHit _playerHit;
    private int layerMask;

    [SerializeField] private Bullet Prefab;
    private Stack<Bullet> pull_bullet;
    private int magazine = 10;
    private int shootCount = 0;
    private float reloadTime = 0;
    
    private Vector3 currentPosition;
    private bool _isshoot = false;
    
    private Vector3[] Localvertices;
    private Vector3[] Globalvertices;
    private void Start()
    {
        pull_bullet = new Stack<Bullet>();
        currentPosition = Vector3.zero;
        SetLay();
        SetPlaneEdgePoint();
        SetBulletPool();
    }

    private void SetLay()
    {
        height = _collider.height / 2;
        radius = _collider.radius / 2;
        _rayOffset = new Vector3(0, 0, radius);
        _pivotRay = new Ray();
        _playerRay = new Ray();
        _pivotRay.direction = Vector3.down;
        _playerRay.direction = Vector3.down;
        layerMask = 1 << LayerMask.NameToLayer("Plane");
    }
    private void SetPlaneEdgePoint()
    {
        Localvertices = Rect_Plane.GetComponent<MeshFilter>().sharedMesh.vertices;

        Globalvertices = new Vector3[2]
        {
            Rect_Plane.TransformPoint(Localvertices[0]), Rect_Plane.TransformPoint(Localvertices[120])
        };
    }

    private void SetBulletPool()
    {
        for (int i = 0; i < magazine; i++)
        {
            Bullet newBullet = Instantiate(Prefab);
            pull_bullet.Push(newBullet);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!_isshoot)
                StartCoroutine(Shoot());
            // if(!_isshoot)
            //     _isshoot = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _pivotRay.origin = transform.position + transform.rotation * _rayOffset;
        _playerRay.origin = transform.position;

        if (Physics.Raycast(_playerRay, out _playerHit, 30f, layerMask))
        {
            if (Vector3.Distance(transform.position, _playerHit.point) >= 0.2f)
            {
                if (currentPosition != transform.position)
                {
                    if (transform.position.y <= _playerHit.point.y + height)
                        transform.position += new Vector3(0, Time.deltaTime * 0.2f, 0);
                    else if (transform.position.y > _playerHit.point.y + height)
                        transform.position += new Vector3(0, Time.deltaTime * -0.2f, 0);
                    currentPosition = transform.position;
                }
                else if (transform.position.y - _playerHit.point.y >= 0f)
                {
                    transform.position += new Vector3(0, Time.deltaTime * -0.2f, 0);
                }
            }
        }

        if (Physics.Raycast(_pivotRay, out _raycastHit, 30f, layerMask))
        {
            if (transform.position.y != _raycastHit.point.y)
            {
                float slopeAngle = Vector3.Angle(_raycastHit.normal, Vector3.up);

                Vector3 forward = Quaternion.AngleAxis(slopeAngle, Vector3.back) * transform.forward;
                //transform.rotation = Quaternion.AngleAxis(slopeAngle, transform.rotation * Vector3.forward);
                //transform.rotation = transform.rotation * Quaternion.Euler(Vector3.up * slopeAngle);
                transform.rotation = Quaternion.LookRotation(_raycastHit.point - _playerHit.point);
            }
        }

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveVector(Vector3.forward);
                //MoveAngle(transform.rotation.y);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveVector(Vector3.back);
                //MoveAngle(transform.rotation.y - 180);
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveVector(Vector3.right);
                //MoveAngle(transform.rotation.y + 90);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                MoveVector(Vector3.left);
                //MoveAngle(transform.rotation.y - 90);
            }
            
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -1, 0);
            }

            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, 1, 0);
            }
        }

        // if (_isshoot && shootCount < magazine)
        // {
        //     if (reloadTime > 0.5f)
        //     {
        //         Shooting();
        //         shootCount++;
        //         reloadTime = 0f;
        //     }
        //     else
        //     {
        //         reloadTime += Time.deltaTime;
        //     }
        // }
        // else
        // {
        //     shootCount = 0;
        //     _isshoot = false;
        // }
    }
    
    private void MoveVector(Vector3 forward)
    {
        transform.position += transform.rotation * forward * Time.deltaTime * speed;
        //transform.position += transform.TransformDirection(forward) * Time.deltaTime * speed;
    }

    private void MoveAngle(float _angle)
    {
        float cosZ = Mathf.Cos(_angle);
        float sinX = Mathf.Sin(_angle);
        Vector3 _forward = new Vector3(sinX, 0, cosZ);

        transform.position += _forward * Time.deltaTime * speed;
    }
    IEnumerator Shoot()
    {
        _isshoot = true;

        for (int i = 0; i < magazine; i++) 
        {
            Bullet _bullet = GetBullet();
            _bullet.SetBullet(this.transform.position,transform.rotation, transform.rotation * Vector3.forward, Globalvertices, () =>
            {
                pull_bullet.Push(_bullet);
            });
            yield return new WaitForSeconds(0.4f);
        }
        _isshoot = false;
    }

    private void Shooting()
    {
        Bullet _bullet = GetBullet();
        _bullet.SetBullet(this.transform.position,transform.rotation, transform.rotation * Vector3.forward, Globalvertices, () =>
        {
            pull_bullet.Push(_bullet);
        });
    }

    private Bullet GetBullet()
    {
        if (pull_bullet.Count > 0)
            return pull_bullet.Pop();
        else
        {
            Bullet newBullet = Instantiate(Prefab);
            return newBullet;
        }
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(_pivotRay.origin,_pivotRay.direction*2f,Color.red);
        Debug.DrawRay(_playerRay.origin,_playerRay.direction*2f,Color.blue);
    }
}
