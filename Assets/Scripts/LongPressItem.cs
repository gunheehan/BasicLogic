using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongPressItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LongPressDirection itemDirection;
    private Action<LongPressDirection> changeLongPressState;

    public void SetItem(LongPressDirection _direction, Action<LongPressDirection> _chageLongPressState)
    {
        itemDirection = _direction;
        changeLongPressState = _chageLongPressState;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        changeLongPressState?.Invoke(itemDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        changeLongPressState?.Invoke(LongPressDirection.None);
    }
}
