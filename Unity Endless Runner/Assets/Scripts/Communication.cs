using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;
 
 public class Communication : MonoBehaviour {
 
    
 
     SerialPort myport = new SerialPort();
     // Use this for initialization
     void Start () {
 
         myport.BaudRate = 115200;
         myport.PortName = "COM5";
         myport.Open();
     }
     
     // Update is called once per frame
     void Update () {
         string arduinoData = myport.ReadLine();
         Debug.Log(arduinoData);
     }
 
     //void SetAccelerationText()
     //{
        // string arduinoData = myport.ReadLine();
         //string[] vec6 = arduinoData.Split(',');
         //Debug.Log(arduinoData);
         //xspeed = int.Parse(vec6[0]);
         //speedText.text = "Speed: " + vec6[0] + ',' + vec6[1] + ',' + vec6[2] + ',' + vec6[3] + ',' + vec6[4] + ',' + vec6[5];
     //}
 }