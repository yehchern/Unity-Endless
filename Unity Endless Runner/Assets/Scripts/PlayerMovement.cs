using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System;

public class PlayerMovement : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM5", 9600);
    bool alive = true;
    public GameObject uiObject;
    public GameObject uiObject2;
    public float speed = 8;
    [SerializeField] Rigidbody rb;

    float lastFloatArduinoData = 0f;
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;


    //////////////////////////////////////////
    public float leftRightSpeed = 2;
    void Start()
    {
        uiObject.SetActive(false);
        uiObject2.SetActive(false);
        //sp.Open();
        //sp.ReadTimeout = 1;
        
        sp.BaudRate = 9600;
        sp.PortName = "COM5";
        sp.Open();

    }

   
    private void FixedUpdate(){
        if(!alive) return;
        //Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime; 
        //Vector3 horizontalMove = transform.right * horizontalInput*speed*Time.fixedDeltaTime*horizontalMultiplier;
        //rb.MovePosition(rb.position + forwardMove);//+ horizontalMove
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
    } /**/

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");


        string arduinoData = sp.ReadLine();
        Debug.Log(arduinoData);

        float floatArduinoData = float.Parse(arduinoData);
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * 3);


        //if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        if (floatArduinoData > lastFloatArduinoData && ((floatArduinoData - lastFloatArduinoData)/20 > 0.02f))//right
        {
            if(this.gameObject.transform.position.x > -4f)
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime * leftRightSpeed);
            }
        }
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        if (floatArduinoData <= lastFloatArduinoData && ((lastFloatArduinoData - floatArduinoData)/20 > 0.02f))//left   
        {
            if (this.gameObject.transform.position.x < 4f)
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime * leftRightSpeed * -1);
            }
        }

        lastFloatArduinoData = floatArduinoData;
        /**/
        //if (sp.IsOpen){
        // try{
        //if (sp.ReadByte()>= 10){
        //transform.Translate(Vector3.left * Time.deltaTime*5);
        //}
        // if (sp.ReadByte()<= -10){
        //transform.Translate(Vector3.right * Time.deltaTime*5);
        // }
        // } catch (System.Exception){

        // }
        // }
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if(transform.position.y < -5){
            Die();
        }
        if (Input.GetKeyDown(KeyCode.A)){
        
            StartCoroutine("WaitForSec");
            
        }
        if (Input.GetKeyDown(KeyCode.D)){
            
            StartCoroutine("WaitForSec2");
            
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

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        uiObject.SetActive(true);
        yield return new WaitForSeconds(1);
        uiObject.SetActive(false);
    }

    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(1);
        uiObject2.SetActive(true);
        yield return new WaitForSeconds(1);
        uiObject2.SetActive(false);
    }

    



}
