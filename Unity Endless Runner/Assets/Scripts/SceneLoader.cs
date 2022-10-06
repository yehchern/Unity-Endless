using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;
using System;

public class SceneLoader : MonoBehaviour
{
    SerialPort myport = new SerialPort("COM5", 9600);
    SerialPort boneport = new SerialPort("COM7", 9600);
    public string beginningData;
    public float floatBeginningData;
    public static float datatemp;
    public int stateChange;
    public int stateChangetemp;
    public static int datacount;
    public static float maxData = -10000;
    //public float maxData2;
    //public float maxTemp;
    public static float minData = 10000;
    //public float minData2;
    //public float minTemp;
    public int leftCount;
    public int rightCount;
    public GameObject uiObject3;
    public GameObject uiObject4;
    public GameObject uiObject5;
    public GameObject uiObject6;
    public GameObject uiObject7;

    void Start()
    {
        StartCoroutine("WaitForSec6");
        /*myport.BaudRate = 9600;
        myport.PortName = "COM5";
        myport.Open();
        boneport.BaudRate = 9600;
        boneport.PortName = "COM7";
        boneport.Open();*/
        //uiObject3.SetActive(false);
        //uiObject4.SetActive(false);
        //uiObject5.SetActive(false);
       
    }

    void Update()
    {
        StartCoroutine("WaitForSec7");
        StartCoroutine("WaitForSec3");
        StartCoroutine("WaitForSec4");
    }
        /*string beginningData = myport.ReadLine();
        float floatBeginningData = float.Parse(beginningData);
        //Debug.Log(floatBeginningData);
        if (floatBeginningData < 0)
        {
            stateChange = -1;
            
            if(floatBeginningData < minData){
            minData = floatBeginningData;
            Debug.Log(minData);
        }
            //RightMovement.Add(floatBeginningData);
        }
        else
        {
            stateChange = 1;
           // LeftMovement.Add(floatBeginningData);
            if(floatBeginningData > maxData){
            maxData = floatBeginningData;
            Debug.Log(maxData);
        }
        }
        
        
        if (stateChangetemp != stateChange)
        {
            stateChangetemp = stateChange;
            changeFunction();
        }
        
        if (leftCount > 6)
        {
            StartCoroutine("WaitForSec5");
        }
    }

    
    
    public void changeFunction()
    {
        leftCount += 1;
    }
    

    /*public void minChoose()
    {
        minTemp = 0;
        if (floatBeginningData < minTemp){
            minTemp = floatBeginningData;
        }
        
    }

    public void maxChoose()
    {
        maxTemp = 0;
        if (floatBeginningData > maxTemp){
            maxTemp = floatBeginningData;
        }
    }*/

    IEnumerator WaitForSec3()
    {
        yield return new WaitForSeconds(43);
        uiObject3.SetActive(true);
        yield return new WaitForSeconds(45);
        uiObject3.SetActive(false);
    }

    IEnumerator WaitForSec4()
    {
        yield return new WaitForSeconds(48);
        uiObject4.SetActive(true);
        yield return new WaitForSeconds(52);
        uiObject4.SetActive(false);
    }

    IEnumerator WaitForSec5()
    {
        yield return new WaitForSeconds(2);
        uiObject5.SetActive(true);
        yield return new WaitForSeconds(7);
        uiObject5.SetActive(false);
        SceneManager.LoadScene(2);
    }
     IEnumerator WaitForSec6()
    {
        uiObject6.SetActive(true);
        yield return new WaitForSeconds(3);
        uiObject6.SetActive(false);
        uiObject7.SetActive(true);
        yield return new WaitForSeconds(15);
        uiObject7.SetActive(false);
        myport.BaudRate = 9600;
        myport.PortName = "COM5";
        myport.Open();
        boneport.BaudRate = 9600;
        boneport.PortName = "COM7";
        boneport.Open();
    }

    IEnumerator WaitForSec7()
    {
        yield return new WaitForSeconds(40);
        string beginningData = myport.ReadLine();
        float floatBeginningData = float.Parse(beginningData);
        //Debug.Log(floatBeginningData);
        if (floatBeginningData < 0)
        {
            stateChange = -1;
            
            if(floatBeginningData < minData){
            minData = floatBeginningData;
            Debug.Log(minData);
        }
            //RightMovement.Add(floatBeginningData);
        }
        else
        {
            stateChange = 1;
           // LeftMovement.Add(floatBeginningData);
            if(floatBeginningData > maxData){
            maxData = floatBeginningData;
            Debug.Log(maxData);
        }
        }
        
        
        if (stateChangetemp != stateChange)
        {
            stateChangetemp = stateChange;
            changeFunction();
        }
        
        if (leftCount > 6)
        {
            StartCoroutine("WaitForSec5");
        }
    

    
    
    void changeFunction()
    {
        leftCount += 1;
    }

    if (datatemp != floatBeginningData){
        datatemp = floatBeginningData;
        datacount += 1;
    }
    

    /*public void minChoose()
    {
        minTemp = 0;
        if (floatBeginningData < minTemp){
            minTemp = floatBeginningData;
        }
        
    }

    public void maxChoose()
    {
        maxTemp = 0;
        if (floatBeginningData > maxTemp){
            maxTemp = floatBeginningData;
        }
    }*/
    }
}
