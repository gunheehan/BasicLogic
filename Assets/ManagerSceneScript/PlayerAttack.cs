using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Collider _collider_Plane;

    private bool _isshoot = false;
    public Bounds _bounds = new Bounds();
    private int magazine = 10;

    void Start()
    {
        PlayerInputManager.Instance.KeyboradNormalInputEvent += PlayerInputNormalEventClassification;
        AttackManager.Instance.init();
        _bounds.center = Vector3.zero;
        _bounds.size = new Vector3(200f, 5f, 200f);
    }

    private void PlayerInputNormalEventClassification()
    {
        if (Input.GetKeyDown((KeyCode.Mouse0)))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (_isshoot)
            return;
        
        StartCoroutine(Shooting());
    }
    IEnumerator Shooting()
    {
        _isshoot = true;

        for (int i = 0; i < magazine; i++) 
        {
            BulletPrefab _bullet = AttackManager.Instance.GetBullet();
            _bullet.SetBullet(transform.position,transform.rotation, () =>
            {
                AttackManager.Instance.ReturnPoolBullet(_bullet);
            },_bounds);
            yield return new WaitForSeconds(0.2f);
        }
        _isshoot = false;
    }
}
