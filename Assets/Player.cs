using UnityEngine;

public class Player : MonoBehaviour
{
    private float angle;

    private bool _ismove = false;
    private bool _isrotate = false;

    private Vector3 targetPoint;
    private Quaternion targetRotation;

    [SerializeField] private float speed = 0.1f;
    private float base_speed;
    private float middle_distance;

    private void Start()
    {
        base_speed = speed;
    }

    void FixedUpdate()
    {
        if (_ismove && !_isrotate)
        {
            StartMove();
        }
        
        if (_isrotate)
        {
            StartRotate();
        }
    }

    public void PlayerMove(Vector3 _targetPoint)
    {
        targetPoint = new Vector3(_targetPoint.x, _targetPoint.y + transform.position.y, _targetPoint.z);
        PlayerRotateToPoint();
    }

    public void PlayerMoveToPoint()
    {
        Debug.Log("이동 시작");
        speed = base_speed;
        middle_distance = Vector3.Distance(transform.position, targetPoint);
        
        _ismove = true;
    }

    public void PlayerRotateToPoint()
    {
        angle = Vector3.Angle(targetPoint - transform.position, transform.forward);

        if (Vector3.Dot(transform.right, targetPoint - transform.position) >= 0f)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        }
        else
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, -angle, 0);
        }

        _isrotate = true;
    }

    private void StartMove()
    {
        Vector3 velo = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref velo, 0.1f);
        // float distance = Vector3.Distance(transform.position, targetPoint);
        //
        // if (middle_distance <= distance)
        // {
        //     transform.position = Vector3.Lerp(transform.position, targetPoint, speed += Time.deltaTime / 10);
        //     Debug.Log("Speed UP");
        // }
        // else
        // {
        //     if (base_speed <= speed)
        //     {
        //         transform.position = Vector3.Lerp(transform.position, targetPoint, speed -= Time.deltaTime / 10);
        //         Debug.Log("Speed DOWN");
        //     }
        //     else
        //     {
        //         if (Vector3.Distance(transform.position, targetPoint) <= base_speed)
        //         {
        //             transform.position = targetPoint;
        //             Debug.Log("ARRIVE");
        //             _ismove = false;
        //         }
        //
        //         else
        //         {
        //             transform.position = Vector3.Lerp(transform.position, targetPoint, base_speed);
        //             Debug.Log("STILLDOWN");
        //         }
        //     }
        // }
    }

    private void StartRotate()
    {
        angle = Quaternion.Angle(transform.rotation,targetRotation);
        
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,speed);
        if (angle <= 1.0f)
        {
            _isrotate = false;
            Debug.Log("회전 종료");
            PlayerMoveToPoint();
        }
    }
}
