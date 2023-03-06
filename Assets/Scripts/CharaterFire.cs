using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterFire : MonoBehaviour
{
    [SerializeField] private Button Btn_Fire;
    [SerializeField] private Transform transformFirePoint;
    private BulletPrefab bulletPrefab;
    private Stack<BulletPrefab> pool_bullet;
    private int magazine = 10;
    
    public Bounds _bounds = new Bounds();
    
    private Ray _pivotRay;
    private RaycastHit _raycastHit;
    private int layerMask;
    private Vector3 _rayOffset;
    
    void Start()
    {
        init();
        
        Btn_Fire.onClick.AddListener(OnClickShoot);
    }

    private void init()
    {
        _rayOffset = new Vector3(0, 0, 5);
        _pivotRay = new Ray();
        _pivotRay.direction = Vector3.down;
        layerMask = 1 << LayerMask.NameToLayer("Plane");
        RayJudgment();

        bulletPrefab = Resources.Load<BulletPrefab>("Prefabs/BulletPrefab");
        SetBulletPool();
    }

    private void RayJudgment()
    {
        _pivotRay.origin = transform.position + transform.rotation * _rayOffset;
        if (Physics.Raycast(_pivotRay, out _raycastHit, 30f, layerMask))
        {
            _bounds.center = Vector3.zero;
            _bounds.size = new Vector3(_raycastHit.transform.localScale.x * 10, 5f, _raycastHit.transform.localScale.z * 10);
        }
    }

    private void SetBulletPool()
    {
        pool_bullet = new Stack<BulletPrefab>();

        for (int i = 0; i < magazine; i++)
        {
            BulletPrefab newBullet = Instantiate(bulletPrefab);
            pool_bullet.Push(newBullet);
        }
    }

    public void OnClickShoot()
    {
        BulletPrefab _bullet = GetBullet();

        if (_bullet == null)
            return;
        
        _bullet.SetBullet(transformFirePoint.position,transformFirePoint.rotation, () =>
        {
            pool_bullet.Push(_bullet);
        },_bounds,ObstacleRecycle);
    }
    
    private BulletPrefab GetBullet()
    {
        if (pool_bullet.Count > 0)
            return pool_bullet.Pop();
        return null;
    }

    private void ObstacleRecycle(GameObject gameObject)
    {
        StartCoroutine(RecycleObject(gameObject));
    }

    IEnumerator RecycleObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(true);
    }
}
