using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    public float speed = 5;
    [SerializeField] Rigidbody rb;//鋼體，幫助受力

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;//幫助加速水平移動

    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    private void FixedUpdate(){//隔一段時間執行一次
        if(!alive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime; //移動距離、方向       transform.forward: 面向方向、
        Vector3 horizontalMove = transform.right * horizontalInput*speed*Time.fixedDeltaTime*horizontalMultiplier;// 水平移動
        rb.MovePosition(rb.position + forwardMove+ horizontalMove);
    }
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //鍵盤控制
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if(transform.position.y < -5){
            Die();
        }
    }
    public void Die(){
        alive = false;
        //Restart the game
        Invoke("Restart", 2);
    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Jump(){
        //Check whether we are currently grounded
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/2)+ 0.1f, groundMask);

        //if we are, jump
        rb.AddForce(Vector3.up * jumpForce);
    }
}
