using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Ray _PhysicsRay;
    private RaycastHit _raycastHit;
    private Ray _ObserverRay;
    private RaycastHit _observerRaycastHit;
    private CapsuleCollider _collider;
    private int layerMask;
    private Vector3 _observerOffset;
    private float base_height;
    private float base_radius;
    private float speed = 1f;

    private bool _isjump = false;
    private float maxJumpHeight = 0f;

    private const float gravity = -9.81f;
    private void Start()
    {
        _PhysicsRay = new Ray();
        _PhysicsRay.direction = Vector3.down;
        _ObserverRay = new Ray();
        _ObserverRay.direction = Vector3.down;
        
        layerMask = 1 << LayerMask.NameToLayer("Plane");
        _collider = gameObject.GetComponent<CapsuleCollider>();
        base_height = _collider.height / 2;
        base_radius = _collider.radius / 2;
        _observerOffset = new Vector3(0, 0, base_radius);
        Debug.Log("사이즈 : " + base_height);
        PlayerInputManager.Instance.KeyboardPhysicsInputEvent += PlayerInputPhysicsEventClassification;
    }

    private void FixedUpdate()
    {
        if (!_isjump)
        {
            _PhysicsRay.origin = transform.position;
            _ObserverRay.origin = transform.position + transform.rotation * _observerOffset;
        
            if (!Physics.Raycast(_PhysicsRay, out _raycastHit, 1f, layerMask))
            {
                //transform.position -= new Vector3(0, Time.deltaTime * 0.5f, 0);
                transform.position += new Vector3(0,  gravity * Time.fixedDeltaTime, 0);
            }
            
            if(Physics.Raycast(_ObserverRay, out _observerRaycastHit, 10f, layerMask))
            {
                if(_raycastHit.point.y != _observerRaycastHit.point.y)
                    transform.rotation = Quaternion.LookRotation(_observerRaycastHit.point - _raycastHit.point);
            }
        }
        else
        {
            Jump();
        }
    }

    private void PlayerInputPhysicsEventClassification()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MoveVector(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveVector(Vector3.back);
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveVector(Vector3.right);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            MoveVector(Vector3.left);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            RotateAngle(-1f);
        }

        else if (Input.GetKey(KeyCode.E))
        {
            RotateAngle(1f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!_isjump)
            {
                _isjump = true;
                maxJumpHeight = transform.position.y + 10f;
                Debug.Log(maxJumpHeight);
            }
        }
    }
    private void MoveVector(Vector3 forward)
    {
        transform.position += transform.rotation * forward * Time.deltaTime * speed;
    }

    private void RotateAngle(float _angle)
    {
        transform.Rotate(0, _angle, 0);
    }

    private Vector3 velocity;
    
    private void Jump()
    {
        Debug.Log("증가");
            transform.position -= new Vector3(0,  gravity * Time.fixedDeltaTime, 0);
            if (transform.position.y > maxJumpHeight)
                _isjump = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_PhysicsRay.origin,_PhysicsRay.direction*10f,Color.red);
        Debug.DrawRay(_ObserverRay.origin,_PhysicsRay.direction*10f,Color.blue);
    }
}
