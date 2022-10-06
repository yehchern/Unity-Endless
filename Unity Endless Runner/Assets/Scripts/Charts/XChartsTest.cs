using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class XChartsTest : MonoBehaviour
{
    //https://github.com/XCharts-Team/XCharts/blob/3.0/Documentation/XChartsConfiguration-ZH.md
    //LineChart chart;
    public Button buttonForCoin;
    int countClickCoinButtonTimes;

    public Button buttonForMove;
    int countClickMoveButtonTimes;

    /*connect to mySQL*/
    public static XChartsTest Instance;
    public webChart webChart;
    

    
    
    void Start()
    {
        //the number of coins patient get
        //createCoinChart();
        //buttonForCoin.gameObject.SetActive(true);

        //the number of move
        //timesOfMoveChart();
        countClickCoinButtonTimes = 0;
        countClickMoveButtonTimes = 0;
        /*    if (countClickCoinButtonTimes == 1)
            {//click button one time
                createCoinChart();
            }//else if(countClickCoinButtonTimes == 2)
            //{

            //}*/


        /*connect to mySQL*/
        Instance = this;
        webChart = GetComponent<webChart>();
        
        //string[] s;
       // StartCoroutine(GetCoinsData("http://localhost/unityPatronus/getChartDataFromMySQL.php"));

        //Debug.Log(chartInfo_temp.coinDB.Count);
    }

    void Update()
    {
        if (countClickCoinButtonTimes == 1)
        {//click button one time
            //Debug.Log(countClickCoinButtonTimes);
            //createChart(10, 20, "the number of coins every time when you play the game");

        }
        else if(countClickCoinButtonTimes == 2)
        {
            countClickCoinButtonTimes = 0;
            //createChart(10, 20, "the number of coins every time when you play the game");
            StartCoroutine(GetCoinsData("http://localhost/unityPatronus/getChartDataFromMySQL.php"));
        }

        if (countClickMoveButtonTimes == 1)
        {//click button one time
            //Debug.Log(countClickMoveButtonTimes);
            List<int> coinMoveInt = new List<int>();
            for(int i = 0; i < 10; i++)
            {
                coinMoveInt.Add(Random.Range(40, 50));
            }
            
            createChart(40, 50, "the number of moving every time when you play the game", coinMoveInt);

        }
        else if (countClickMoveButtonTimes == 2)
        {
            countClickMoveButtonTimes = 0;
            //createChart(40, 50, "the number of moving every time when you play the game");
        }


    }/**/


    public void CoinButtonClick()
    {
        countClickCoinButtonTimes++;
        Debug.Log(countClickCoinButtonTimes);
    }

    public void MoveButtonClick()
    {
        countClickMoveButtonTimes++;
        Debug.Log(countClickMoveButtonTimes);
    }

    public void createChart(int num1, int num2, string s, List<int> coinDataInt)
    {
        //buttonForCoin.gameObject.SetActive(false);//hide button

        //set LineChart to game object 
        var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }

        //setSize
        chart.SetSize(620, 300);

        //set title
        var title = chart.GetOrAddChartComponent<Title>();
        title.text = s;

        //show tip window and legend or not
        var tooltip = chart.GetOrAddChartComponent<Tooltip>();
        tooltip.show = true;

        var legend = chart.GetOrAddChartComponent<Legend>();
        legend.show = false;

        //setup axis
        var xAxis = chart.GetOrAddChartComponent<XAxis>();
        xAxis.splitNumber = coinDataInt.Count;//expected splitNumbers
        xAxis.boundaryGap = true;// edge of axis is blank or not
        xAxis.type = Axis.AxisType.Category;// type of axis: Value、Category、Log、Time

        var yAxis = chart.GetOrAddChartComponent<YAxis>();
        yAxis.type = Axis.AxisType.Value;

        chart.RemoveData();
        //chart.ClearData();
        chart.AddSerie<Line>("line");
       
        for (int i = 1; i <= coinDataInt.Count; i++)
        {
            if (i == 1)
            {
                chart.AddXAxisData(i + "st game");
            }
            else if (i == 2)
            {
                chart.AddXAxisData(i + "nd game");
            }
            else if (i == 3)
            {
                chart.AddXAxisData(i + "rd game");
            }
            else
            {
                chart.AddXAxisData(i + "th game");
            }
            //chart.AddData(0, Random.Range(num1, num2));

            
            /*test array*/
            for (int j = coinDataInt.Count - 1; j >= 0; j--)
            {
                if(i + j == coinDataInt.Count)
                {
                    chart.AddData(0, coinDataInt[j]);
                }
                
            }

        }
    }

    //getData
    IEnumerator GetCoinsData(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            string[] coinData;
            List<int> coinDataInt = new List<int>();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    /*        for (int i = 0; i < webRequest.downloadHandler.text.Length; i++)
                            {
                                //Debug.Log(webRequest.downloadHandler.text[i]);
                            }*/

                   
                    int x = 0;
                    coinData = webRequest.downloadHandler.text.Split("<br>");
                    //Debug.Log("ttttttttttttttttttttt" + coinData.Length.ToString());
                    for (int i = 0, j = 0; i < coinData.Length; i++, j++)
                    {
                        Debug.Log(i);
                        Debug.Log(coinData[i]);
                        if (coinData[i] != "" && coinData[i] != "Connected successfully")
                        {
                            //Debug.Log(i);
                            //Debug.Log(coinData[i]);
                            //list.Add(coinData[i]);
                            Int32.TryParse(coinData[i], out x);
                            coinDataInt.Add(x);
                            
                        }

                    }

                    createChart(10, 20, "the number of coins every time when you play the game", coinDataInt);
                    break;
            }
        }
    }
}