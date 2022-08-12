using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 targetPos = player.position + offset; 
       targetPos.x = 0;//x�b����k�A�Yx=0�A�h�۾��û��b����
       transform.position = targetPos;
    }
}
