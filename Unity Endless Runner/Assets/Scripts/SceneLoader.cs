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
    public string beginningData;
    public float floatBeginningData;
    public float maxData;
    public float minData;
    public int leftCount;
    public int rightCount;
    public GameObject uiObject3;
    public GameObject uiObject4;
    public GameObject uiObject5;

    void Start()
    { 
        myport.BaudRate = 9600;
        myport.PortName = "COM5";
        myport.Open();
        //uiObject3.SetActive(false);
        //uiObject4.SetActive(false);
        //uiObject5.SetActive(false);
        StartCoroutine("WaitForSec3");
        StartCoroutine("WaitForSec4");
    }

    void Update()
    {
        string beginningData = myport.ReadLine();
        float floatBeginningData = float.Parse(beginningData);
        Debug.Log(floatBeginningData);
        /*for (int i = 0; i < length(floatBeginningData); i++) 
       {
        if (floatBeginningData > 0) {
        maxData = floatBeginningData;
        chooseMax();
       } else if (floatBeginningData < 0){
        minData = floatBeginningData;
        chooseMin();
       }
       }*/
        if (floatBeginningData < -20)
        {
        StartCoroutine("WaitForSec5");
        }   
    }

    /*public void chooseMin(float floatBeginningData,float minData){
        if(floatBeginningData < minData){
            minData = floatBeginningData;
        }
    }

    public void chooseMax(float floatBeginningData,float maxData){
        if(floatBeginningData > maxData){
            maxData = floatBeginningData;
        }
    }*/

    IEnumerator WaitForSec3()
    {
        yield return new WaitForSeconds(2);
        uiObject3.SetActive(true);
        yield return new WaitForSeconds(4);
        uiObject3.SetActive(false);
    }

    IEnumerator WaitForSec4()
    {
        yield return new WaitForSeconds(6);
        uiObject4.SetActive(true);
        yield return new WaitForSeconds(4);
        uiObject4.SetActive(false);
    }

    IEnumerator WaitForSec5()
    {
        yield return new WaitForSeconds(2);
        uiObject5.SetActive(true);
        yield return new WaitForSeconds(3);
        uiObject5.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
