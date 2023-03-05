using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform target;
    private Vector3 offset;

    private bool isDrag = false;

    void Start()
    {
        offset = new Vector3(0, 2, -5f);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (isDrag)
        {
            Debug.Log(offset);
            transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, 0.02f);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 0.01f);   
        }
    }

    public void SetCurrentOffset(Vector3 _offset)
    {
        offset = _offset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        BeginDrag(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDrag(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = transform.position;
        isDrag = false;
    }
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    public float xAngle = 0f;
    public float yAngle = 55f;
    float xAngleTemp;
    float yAngleTemp;
    public void BeginDrag(Vector2 a_FirstPoint)
    {
        FirstPoint = a_FirstPoint;
        xAngleTemp = xAngle;
        yAngleTemp = yAngle;
    }

    public void OnDrag(Vector2 a_SecondPoint)
    {
        SecondPoint = a_SecondPoint;
        xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
        yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.

        // 회전값을 40~85로 제한
        if (yAngle < 40f)
            yAngle = 40f;
        if (yAngle > 85f)
            yAngle = 85f;
    }
}
