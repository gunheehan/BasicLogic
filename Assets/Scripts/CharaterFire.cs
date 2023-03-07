using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterFire : MonoBehaviour
{
    [SerializeField] private Transform transformFirePoint;
    private BulletPrefab bulletPrefab;
    private Stack<BulletPrefab> pool_bullet;

    private UIFireController uiFireController;
    private int limitMagazine = 10;
    private int magazine;
    
    public Bounds _bounds = new Bounds();

    private string magazineTextForm = "{0} / {1}";

    private void Start()
    {
        init();
    }

    private void OnDestroy()
    {
        uiFireController.FireInputEvent -= OnClickShoot;
    }

    private void init()
    {
        _bounds.center = Vector3.zero;
        _bounds.size = new Vector3(200, 5f, 200);
        bulletPrefab = Resources.Load<BulletPrefab>("Prefabs/BulletPrefab");
        uiFireController = Resources.Load<UIFireController>("Prefabs/UiFireController");
        uiFireController = Instantiate(uiFireController);
        uiFireController.FireInputEvent += OnClickShoot;
        SetBulletPool();
    }

    private void SetBulletPool()
    {
        pool_bullet = new Stack<BulletPrefab>();

        for (int i = 0; i < limitMagazine; i++)
        {
            BulletPrefab newBullet = Instantiate(bulletPrefab);
            pool_bullet.Push(newBullet);
        }

        magazine = limitMagazine;
        uiFireController.SetMagazineText(string.Format(magazineTextForm,magazine,limitMagazine));
    }

    public void OnClickShoot()
    {
        BulletPrefab bullet = GetBullet();

        if (bullet == null)
            return;
        magazine--;

        bullet.SetBullet(transformFirePoint.position, transformFirePoint.rotation, BulletPushEvent, _bounds,
            ObstacleRecycle);
        uiFireController.SetMagazineText(string.Format(magazineTextForm,magazine,limitMagazine));
    }

    private void BulletPushEvent(BulletPrefab bullet)
    {
        magazine++;
        uiFireController.SetMagazineText(string.Format(magazineTextForm,magazine,limitMagazine));
        pool_bullet.Push(bullet);
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
