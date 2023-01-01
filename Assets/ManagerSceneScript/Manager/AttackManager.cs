using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    private BulletPrefab Prefab_bullet;
    private Stack<BulletPrefab> pool_bullet;
    
    private bool isinit = false;
    private int magazine = 10;

    public void init()
    {
        if (isinit)
            return;
        
        SetBulletPool();
        isinit = true;
    }
    private void SetBulletPool()
    {
        GameObject newPrefab = PrefabManager.Instance.GetBulletPrefab();
        Prefab_bullet = newPrefab.GetComponent<BulletPrefab>();
        pool_bullet = new Stack<BulletPrefab>();

        for (int i = 0; i < magazine; i++)
        {
            BulletPrefab newBullet = Instantiate(Prefab_bullet);
            pool_bullet.Push(newBullet);
        }
    }

    public void ReturnPoolBullet(BulletPrefab _bullet)
    {
        pool_bullet.Push(_bullet);
    }
    public BulletPrefab GetBullet()
    {
        if (pool_bullet.Count > 0)
            return pool_bullet.Pop();
        else
        {
            BulletPrefab newBullet = Instantiate(Prefab_bullet);
            return newBullet;
        }
    }
}
