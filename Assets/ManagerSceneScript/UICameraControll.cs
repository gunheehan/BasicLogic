using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICameraControll : MonoBehaviour
{
    public CameraFollow _CameraFollow;
    
    [SerializeField] private Button Btn_ChangeAngle;
    [SerializeField] private Vector3 nearOffset;
    [SerializeField] private Vector3 farOffset;

    private bool _isfarangle = false;
    void Start()
    {
        nearOffset = new Vector3(0, 2, -5f);
        farOffset = new Vector3(0, 5, -15f);
        Btn_ChangeAngle.onClick.AddListener(OnClickChangeAngle);
    }

    private void OnClickChangeAngle()
    {
        if (!_isfarangle)
        {
            _CameraFollow.SetCurrentOffset(farOffset);
            _isfarangle = true;
        }
        else
        {
            _CameraFollow.SetCurrentOffset(nearOffset);
            _isfarangle = false;
        }
    }
}
