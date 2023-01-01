using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NumberKeyClick : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Button Btn_Clear;

    [SerializeField] private Button Btn_0;
    [SerializeField] private Button Btn_1;
    [SerializeField] private Button Btn_2;
    [SerializeField] private Button Btn_3;
    [SerializeField] private Button Btn_4;
    [SerializeField] private Button Btn_5;
    [SerializeField] private Button Btn_6;
    [SerializeField] private Button Btn_7;
    [SerializeField] private Button Btn_8;
    [SerializeField] private Button Btn_9;
    
    // Start is called before the first frame update
    void Start()
    {
        Btn_Clear.onClick.AddListener(OnClickEraser);
        Btn_0.onClick.AddListener(()=>OnClickNumberKey(0));
        Btn_1.onClick.AddListener(()=>OnClickNumberKey(1));
        Btn_2.onClick.AddListener(()=>OnClickNumberKey(2));
        Btn_3.onClick.AddListener(()=>OnClickNumberKey(3));
        Btn_4.onClick.AddListener(()=>OnClickNumberKey(4));
        Btn_5.onClick.AddListener(()=>OnClickNumberKey(5));
        Btn_6.onClick.AddListener(()=>OnClickNumberKey(6));
        Btn_7.onClick.AddListener(()=>OnClickNumberKey(7));
        Btn_8.onClick.AddListener(()=>OnClickNumberKey(8));
        Btn_9.onClick.AddListener(()=>OnClickNumberKey(9));
    }

    private void OnClickNumberKey(int _number)
    {
        _inputField.text += _number.ToString();
    }

    private void OnClickClear()
    {
        _inputField.text = String.Empty;
    }

    private void OnClickEraser()
    {
        if (!string.IsNullOrEmpty(_inputField.text))
        {
            if(_inputField.text.Length <= 1)
                _inputField.text = String.Empty;
            else
                _inputField.text = _inputField.text.Substring(0, _inputField.text.Length - 1);
        }
    }
}
