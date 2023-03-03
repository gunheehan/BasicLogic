using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CharacterMove characterMove;
    [SerializeField] private RectTransform lever;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    private void Update()
    {
        if (isInput)
            InputController();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControllJoyStickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControllJoyStickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        characterMove.Move(Vector2.zero);
    }

    private void ControllJoyStickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    private void InputController()
    {
        characterMove.Move(inputDirection);
    }
}
