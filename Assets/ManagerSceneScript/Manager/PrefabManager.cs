using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    [SerializeField] private GameObject PrefabBullet;

    public GameObject GetBulletPrefab()
    {
        if (PrefabBullet == null)
            return null;
        return PrefabBullet;
    }
}
