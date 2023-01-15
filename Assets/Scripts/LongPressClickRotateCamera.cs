using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LongPressDirection
{
    None,
    Left,
    Right,
    Up,
    Dowm,
    Reset
}

public class LongPressClickRotateCamera : MonoBehaviour
{
    [SerializeField] private Transform _CameraTransform;
    [SerializeField] private Image imageBtn_Left;
    [SerializeField] private Image imageBtn_Right;
    [SerializeField] private Image imageBtn_Up;
    [SerializeField] private Image imageBtn_Down;
    [SerializeField] private Image imageBtn_Reset;

    private Vector3 _startRotate;

    private void Start()
    {
        _startRotate = _CameraTransform.eulerAngles;
        
        imageBtn_Left.gameObject.AddComponent<LongPressItem>().SetItem(LongPressDirection.Left, (value) =>
        {
            CurrentPressDirection = value;
        });
        imageBtn_Right.gameObject.AddComponent<LongPressItem>().SetItem(LongPressDirection.Right, (value) =>
        {
            CurrentPressDirection = value;
        });
        imageBtn_Up.gameObject.AddComponent<LongPressItem>().SetItem(LongPressDirection.Up, (value) =>
        {
            CurrentPressDirection = value;
        });
        imageBtn_Down.gameObject.AddComponent<LongPressItem>().SetItem(LongPressDirection.Dowm, (value) =>
        {
            CurrentPressDirection = value;
        });
        imageBtn_Reset.gameObject.AddComponent<LongPressItem>().SetItem(LongPressDirection.Reset, (value) =>
        {
            CurrentPressDirection = value;
        });
    }

    private LongPressDirection _currentPressDirection;
    public LongPressDirection CurrentPressDirection
    {
        set
        {
            _currentPressDirection = value;
        }
        get
        {
            return _currentPressDirection;
        }
    }
    private void FixedUpdate()
    {
        if (_currentPressDirection != LongPressDirection.None)
        {
            if (_currentPressDirection == LongPressDirection.Left)
            {
                RotateHorizontalAngle(-1f);
            }
            else if(_currentPressDirection == LongPressDirection.Right)
            {
                RotateHorizontalAngle(1f);
            }
            else if(_currentPressDirection == LongPressDirection.Up)
            {
                RotateVerticalAngle(-1f);
            }
            else if(_currentPressDirection == LongPressDirection.Dowm)
            {
                RotateVerticalAngle(1f);
            }
            else if(_currentPressDirection == LongPressDirection.Reset)
            {
                RotateReset();
            }
        }
    }
    private void RotateHorizontalAngle(float _angle)
    {
        _CameraTransform.transform.RotateAround(_CameraTransform.position, Vector3.up, _angle);
    }
    private void RotateVerticalAngle(float _angle)
    {
        _CameraTransform.Rotate(_angle, 0, 0);
    }

    private void RotateReset()
    {
        _CameraTransform.eulerAngles = _startRotate;
        _currentPressDirection = LongPressDirection.None;
    }
}
