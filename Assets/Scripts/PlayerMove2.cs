using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    private Transform characterBody;

    private void Start()
    {
        GameObject stickObject = Resources.Load<GameObject>("Prefabs/JoyStick");
        JoyStick joyStick = Instantiate(stickObject).GetComponent<JoyStick>();
        joyStick.SetPlayer(this);

        characterBody = GetComponent<Transform>();
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
            transform.position += moveDirection * Time.deltaTime * 5f;
        }
    }
}
