using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //public void getmaxMin(){
    //Debug.Log("maxdata is" + SceneLoader.maxData);
    //Debug.Log(SceneLoader.minData);
    
    SerialPort sp = new SerialPort("COM5", 9600);
    SerialPort backport = new SerialPort("COM7", 9600);
    bool alive = true;
    public GameObject uiObject;
    public GameObject uiObject2;
    public GameObject uiObject3;
    

    public GameObject elf;
    public float speed = 8;
    public int value;
    [SerializeField] Rigidbody rb;

    //turning point
    public string direction;
    public string tempdirection;
    float lastLeftMax;
    float lastRightMin;
    

    float lastFloatArduinoData = 0f;
    float horizontalInput;
    //[SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;


    //////////////////////////////////////////
    public float leftRightSpeed = 2;

    //fairy
    //public float max = 10;
    //public float min = -10;


    /*Exit or start window*/
    [SerializeField] private ExitOrRestart myExitOrRestartWindow;

    //count wave time
    public int leftCount;
    public int rightCount;
    public static int stateChange;
    private int stateChangetemp;

    void Start()
    {
        uiObject.SetActive(false);
        uiObject2.SetActive(false);
        //elf.SetActive(false);
        //sp.Open();
        //sp.ReadTimeout = 1;
        
        sp.BaudRate = 9600;
        sp.PortName = "COM5";
        sp.Open();
        backport.BaudRate = 9600;
        backport.PortName = "COM7";
        backport.Open();

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
        //Debug.Log(arduinoData);

        float floatArduinoData = float.Parse(arduinoData);
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * 3);

        //back
        string backData = backport.ReadLine();
        float floatbackData = float.Parse(backData);


        //if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        if (floatArduinoData > lastFloatArduinoData && ((floatArduinoData - lastFloatArduinoData) > 0.02f))//right
        {
            if(this.gameObject.transform.position.x > -4f)
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime * leftRightSpeed);
            }
        }
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        if (floatArduinoData <= lastFloatArduinoData && ((lastFloatArduinoData - floatArduinoData) > 0.02f))//left   
        {
            if (this.gameObject.transform.position.x < 4f)
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime * leftRightSpeed * -1);
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if(transform.position.y < -5){
            Die();
        }


        //count wave time
        if (floatArduinoData < 0)
        {
            stateChange = -1;
        }
        else
        {
            stateChange = 1;
        }
        //Debug.Log(stateChange);

        if (stateChangetemp != stateChange)
        {
            stateChangetemp = stateChange;
            changeFunction();
        }

        if (leftCount >= 50)
        {
            Die();
        }

        //back

        if(floatbackData > 25 ||floatbackData < -25){
             StartCoroutine("WaitForSec3");
        }
        //hint from elf
        /*if (max >= lastFloatArduinoData && lastFloatArduinoData > 0.0f && lastFloatArduinoData > floatArduinoData + 0.2f)
        {
        
            StartCoroutine("WaitForSec");
            
        }

        if (min <= lastFloatArduinoData && lastFloatArduinoData < 0.0f && lastFloatArduinoData + 0.2f < floatArduinoData)
        {
            
            StartCoroutine("WaitForSec2");
            
        }*/
        /*switch (value)
                {
                    case 1:
                        if (floatArduinoData > 0.0f && floatArduinoData < lastFloatArduinoData){
                            turningpoint.Add(lastFloatArduinoData);
                            break;
                        }
                            
                    case 2:
                        if (floatArduinoData < 0.0f && floatArduinoData > lastFloatArduinoData){
                            turningpoint.Add(lastFloatArduinoData);
                            break;
                        }
                            
                }*/
        //if (floatArduinoData > 0 && floatArduinoData > lastFloatArduinoData){
        //    direction = "left";

    if (floatArduinoData > 0 && floatArduinoData < lastFloatArduinoData)
    {
        direction = "leftTurnRight";
        } else if (floatArduinoData < 0 && floatArduinoData > lastFloatArduinoData){
            direction = "rightTurnLeft";
        } 

    void getMax()
    {
        lastLeftMax = -10000;
        if(lastLeftMax < floatArduinoData){
            lastLeftMax = floatArduinoData;
        }
    }
    void getMin()
    {
        lastRightMin = 10000;
        if(lastRightMin > floatArduinoData){
            lastRightMin = floatArduinoData;
        }
    }

        if ( tempdirection != direction){
            if (direction == "leftTurnRight"){
                tempdirection = direction;
                getMax(); 
            } else if( direction =="rightTurnLeft"){
                tempdirection = direction;
                getMin();
            }
        }
        Debug.Log(lastLeftMax);
        Debug.Log(lastRightMin); 

        //hint from elf
        if (lastLeftMax - (SceneLoader.maxData - SceneLoader.datatemp) > 20.0f)
        {
            StartCoroutine("WaitForSec");
        }
        if(lastRightMin - (SceneLoader.minData - SceneLoader.datatemp) > -20.0f)
        {
            StartCoroutine("WaitForSec2");
        }
        /*} else if (floatArduinoData > 0 && floatArduinoData < lastFloatArduinoData){
            direction = "lefttoright";
        } else if (floatArduinoData < 0 && floatArduinoData < lastFloatArduinoData){
            direction = "right";
        } else if (floatArduinoData < 0 && floatArduinoData > lastFloatArduinoData){
            direction = "righttoleft";
        }*/

       /* if (direction == "lefttoright" && lastLeftMax < floatArduinoData){
            lastLeftMax= floatArduinoData;
            if (direction == "lefttoright" && lastLeftMax > floatArduinoData){
                LeftMax = lastLeftMax;
            }
            Debug.Log(LeftMax);
        }
        

         if (direction == "righttoleft" && lastRightMin > floatArduinoData){
            lastRightMin = floatArduinoData;
            if (direction == "righttoleft" && lastRightMin < floatArduinoData){
                RightMin = lastRightMin;
            }
            Debug.Log(RightMin);
        }*/
        
        /*if (floatArduinoData < 0 && floatArduinoData > lastFloatArduinoData && direction == "right"){
            return lastFloatArduinoData;
            
        } */
        /*while (floatArduinoData > 0 && floatArduinoData < lastFloatArduinoData){
          
            turningpoint.Add(lastFloatArduinoData);
    
            break;
        }*/
        /*if (direction =="lefttoright"){
            Debug.Log(floatArduinoData);
        }*/
        //Debug.Log(turningpoint);

        
        lastFloatArduinoData = floatArduinoData;
    }
    
    
    public void Die(){
        alive = false;

        if (!alive)
        {
            int s = GameManager.inst.scoreReturn();
            //Debug.Log(s);
            StartCoroutine(UploadCoinsData(s, "user01"));
            OpenWindow("Restart or End");
        }
            

    }


    public void changeFunction()
    {
        leftCount += 1;
    }
    

    void Restart(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(1);
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
        //elf.SetActive(true);
        uiObject.SetActive(true);
        yield return new WaitForSeconds(1);
        uiObject.SetActive(false);
        //elf.SetActive(false);
    }

    IEnumerator WaitForSec2()
    {
        yield return new WaitForSeconds(1);
        //elf.SetActive(true);
        uiObject2.SetActive(true);
        yield return new WaitForSeconds(1);
        uiObject2.SetActive(false);
        //elf.SetActive(false);
    }

    IEnumerator WaitForSec3()
    {
        yield return new WaitForSeconds(1);
        uiObject3.SetActive(true);
        yield return new WaitForSeconds(1);
        uiObject3.SetActive(false);
        
    }

    /*test window*/
    private void OpenWindow(string message)
    {
        myExitOrRestartWindow.gameObject.SetActive(true);
        

        /*rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();*/
        //transform.Translate( Time.fixedDeltaTime * 3);
        myExitOrRestartWindow.restartButton.onClick.AddListener(restartClick);
        myExitOrRestartWindow.exitButton.onClick.AddListener(endClick);
        myExitOrRestartWindow.messageText.text = message;
        sp.Close();
    }

    private void restartClick()
    {
        myExitOrRestartWindow.gameObject.SetActive(false);
        Debug.Log("Yessssssssssssssss");
        //Restart the game
        Invoke("Restart", 1);
    }

    public void endClick()
    {
        myExitOrRestartWindow.gameObject.SetActive(false);
        Debug.Log("enddddddddddddddddddddd");
        //StartCoroutine(UploadCoinsData(100000, "user00"));
        SceneManager.LoadScene(5);
    }


    /*connect to database*/
    IEnumerator UploadCoinsData(int coins, string usernames)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", coins);
        form.AddField("user", usernames);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityPatronus/insertChartData.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

}
