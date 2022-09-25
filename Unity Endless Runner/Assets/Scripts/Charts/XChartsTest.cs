using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
using UnityEngine.UI;
using TMPro;

public class XChartsTest : MonoBehaviour
{
    //https://github.com/XCharts-Team/XCharts/blob/3.0/Documentation/XChartsConfiguration-ZH.md
    //LineChart chart;
    public Button buttonForCoin;
    int countClickCoinButtonTimes;

    public Button buttonForMove;
    int countClickMoveButtonTimes;


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
    }

    void Update()
    {
        if (countClickCoinButtonTimes == 1)
        {//click button one time
            //Debug.Log(countClickCoinButtonTimes);
            //chart.RemoveData()
            //createCoinChart();

        }
        else if(countClickCoinButtonTimes == 2)
        {
            countClickCoinButtonTimes = 0;
            createChart(10, 20);
        }

        if (countClickMoveButtonTimes == 1)
        {//click button one time
            //Debug.Log(countClickCoinButtonTimes);
            //chart.RemoveData()
            //createCoinChart();

        }
        else if (countClickMoveButtonTimes == 2)
        {
            countClickMoveButtonTimes = 0;
            createChart(40, 50);
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

    public void createChart(int num1, int num2)
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
        title.text = "the number of coins every time when you play the game";

        //show tip window and legend or not
        var tooltip = chart.GetOrAddChartComponent<Tooltip>();
        tooltip.show = true;

        var legend = chart.GetOrAddChartComponent<Legend>();
        legend.show = false;

        //setup axis
        var xAxis = chart.GetOrAddChartComponent<XAxis>();
        xAxis.splitNumber = 5;//expected splitNumbers
        xAxis.boundaryGap = true;// edge of axis is blank or not
        xAxis.type = Axis.AxisType.Category;// type of axis: Value¡BCategory¡BLog¡BTime

        var yAxis = chart.GetOrAddChartComponent<YAxis>();
        yAxis.type = Axis.AxisType.Value;

        chart.RemoveData();
        chart.AddSerie<Line>("line");

        for (int i = 1; i <= 5; i++)
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

            chart.AddData(0, Random.Range(num1, num2));
        }
    }
    

   
}
