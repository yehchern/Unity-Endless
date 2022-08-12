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
       targetPos.x = 0;//x軸控制左右，若x=0，則相機永遠在中間
       transform.position = targetPos;
    }
}
