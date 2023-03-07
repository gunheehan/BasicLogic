using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFireController : MonoBehaviour
{
    public delegate void FireEventHandler();

    private FireEventHandler fireInputEvent = null;
    
    public event FireEventHandler FireInputEvent
    {
        add     { fireInputEvent += value; }
        remove  { fireInputEvent -= value; }
    }
    
    [SerializeField] private Button Btn_Fire;
    [SerializeField] private Text txt_magazine;

    private void Start()
    {
        Btn_Fire.onClick.AddListener(OnClickFireEvent);
    }

    private void OnDisable()
    {
        txt_magazine.text = null;
    }

    public void OnClickFireEvent()
    {
        fireInputEvent?.Invoke();
    }

    public void SetMagazineText(string magazine)
    {
        txt_magazine.text = magazine;
    }
}
