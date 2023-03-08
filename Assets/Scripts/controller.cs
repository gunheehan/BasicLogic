using UnityEngine;

public class controller : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject box;
    private Vector3 pos;

    private const float ray_distance = 20f;
    private int layerMask;

    private void Start()
    {
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer("Plane");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit,ray_distance,layerMask))
            {
                pos = hit.point;
                box.transform.position = pos;

                _player.PlayerMove(pos);
            }
        }
    }
}
