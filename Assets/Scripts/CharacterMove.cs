using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    private Transform characterBody;

    private Camera mainCamera;
    private PetController petController;

    private void LateUpdate()
    {
        if(mainCamera != null)
            targetObject.LookAt(mainCamera.transform);
    }

    private void Start()
    {
        GameObject stickObject = Resources.Load<GameObject>("Prefabs/JoyStick");
        JoyStick joyStick = Instantiate(stickObject).GetComponent<JoyStick>();
        joyStick.SetCharacter(this);

        GameObject petObject = Resources.Load<GameObject>("Prefabs/Pet");
        petController = Instantiate(petObject).GetComponent<PetController>();
        petController.target = transform;
        
        characterBody = GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(characterBody.forward.x, 0f, characterBody.forward.z).normalized;
            Vector3 lookRight = new Vector3(characterBody.right.x, 0f, characterBody.right.z).normalized;
            Vector3 moveDirection = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            Debug.Log(moveDirection.z + " / " + moveDirection.x);
            transform.position += moveDirection * Time.deltaTime * 5f;
        }
    }
}
