using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isinit = false;
    private Camera _mainCamera;
    private PlayerMove _playermove = null;
    private PlayerAttack _playerAttack = null;
    private CameraFollow _cameraFollow = null;
    [SerializeField] private UICameraControll _uiCameraControll = null;
    
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        if (isinit)
            return;
        _mainCamera = Camera.main;

        _playermove = this.gameObject.AddComponent<PlayerMove>();
        _playerAttack = this.gameObject.AddComponent<PlayerAttack>();
        _cameraFollow = _mainCamera.gameObject.AddComponent<CameraFollow>();
        _cameraFollow.target = this.gameObject.transform;
        _uiCameraControll._CameraFollow = _cameraFollow;
    }
}
