using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool isinit = false;
    private Camera _mainCamera;
    private CharacterMove characterMove = null;
    private PlayerAttack _playerAttack = null;
    private CameraFollow _cameraFollow = null;
    
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

        characterMove = this.gameObject.AddComponent<CharacterMove>();
        _playerAttack = this.gameObject.AddComponent<PlayerAttack>();
        _cameraFollow = _mainCamera.gameObject.AddComponent<CameraFollow>();
        _cameraFollow.target = this.gameObject.transform;
    }
}
