using System;
using UnityEngine;

public class BulletPrefab : MonoBehaviour
{
    private Action<GameObject> objectRecycle;
    private Action<BulletPrefab> returnObjectPoolAction;
    
    private Vector3 forward;
    private Bounds bounds;
    RaycastHit hit;
    
    private int layerMask;
    private float speed = 20f;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Obstacle");
    }

    private void OnDisable()
    {
        returnObjectPoolAction = null;
        objectRecycle = null;
    }

    void Update()
    {
        transform.position += forward * Time.deltaTime * speed;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.deltaTime, layerMask))
        {
            returnObjectPoolAction.Invoke(this);
            objectRecycle.Invoke(hit.collider.gameObject);
            gameObject.SetActive(false);
        }
        else if (!bounds.Contains(gameObject.transform.position))
        {
            returnObjectPoolAction.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void SetBullet(Vector3 StartPosition, Quaternion Rotation, Action<BulletPrefab> ReturnpoolAction, Bounds Bounds, Action<GameObject> ObstacleRecycle)
    {
        transform.position = StartPosition;
        transform.rotation = Rotation;
        forward = Rotation * Vector3.forward;
        returnObjectPoolAction = ReturnpoolAction;
        bounds = Bounds;
        objectRecycle = ObstacleRecycle;
        gameObject.SetActive(true);
    }
}