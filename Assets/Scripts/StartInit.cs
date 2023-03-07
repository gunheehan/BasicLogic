using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        GameObject startObject = Resources.Load<GameObject>("Prefabs/Character");
        Instantiate(startObject);
        
        startObject = Resources.Load<GameObject>("Prefabs/ObstacleInitController");
        Instantiate(startObject);

        startObject = Resources.Load<GameObject>("Prefabs/Pet");
        Instantiate(startObject);

        Camera.main.gameObject.AddComponent<MoveAroundTest>();
    }
}
