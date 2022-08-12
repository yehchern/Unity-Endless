using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    public float speed = 5;
    [SerializeField] Rigidbody rb;//����A���U���O

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;//���U�[�t��������

    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    private void FixedUpdate(){//�j�@�q�ɶ�����@��
        if(!alive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime; //���ʶZ���B��V       transform.forward: ���V��V�B
        Vector3 horizontalMove = transform.right * horizontalInput*speed*Time.fixedDeltaTime*horizontalMultiplier;// ��������
        rb.MovePosition(rb.position + forwardMove+ horizontalMove);
    }
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //��L����
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
