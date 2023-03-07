using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleState : MonoBehaviour
{
    [SerializeField] private BoxCollider Object_Collider;

    public bool CheckPlacedClear()
    {
        Collider[] hitColliders = Physics.OverlapBox(Object_Collider.bounds.center, Object_Collider.bounds.extents, Object_Collider.transform.rotation, LayerMask.NameToLayer("Obstacle"));

        if (hitColliders.Length > 0)
        {
            gameObject.SetActive(false);
            return false;
        }
        
        return true;
    }
}
