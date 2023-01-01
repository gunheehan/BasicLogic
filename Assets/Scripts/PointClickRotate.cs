using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _CameraTransform;
    private bool _isdrag = false;
    private bool _isrotateR = false;
    private bool _isrotateL = false;
    private Vector3 _startPoint = Vector3.zero;

    private void FixedUpdate()
    {
        if (_isdrag)
        {
            if (_isrotateL)
            {
                Debug.Log("좌로 회전");
                RotateAngle(-1f);
            }
            else
            {
                Debug.Log("우로 회전");
                RotateAngle(1f);
            }
        }
    }
    private void RotateAngle(float _angle)
    {
        _CameraTransform.Rotate(0, _angle, 0);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("클릭 들어옴");
        _startPoint = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(_startPoint.x - eventData.position.x) > 50f)
        {
            _isdrag = true;
            if (Mathf.Abs(_startPoint.x) > Mathf.Abs(eventData.position.x))
            {
                Debug.Log("좌측 회전 승인");
                _isrotateR = false;
                _isrotateL = true;
            }
            else
            {
                Debug.Log("우측 회전 승인");
                _isrotateL = false;
                _isrotateR = true;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isdrag = false;
        _isrotateR = false;
        _isrotateL = false;
    }
}
