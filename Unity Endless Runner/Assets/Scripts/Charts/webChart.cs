using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class webChart : MonoBehaviour
{
    public List<string> coinData = new List<string>();
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("https://www.example.com"));

        // A non-existing page.
        //StartCoroutine(GetData("http://localhost/unityPatronus/getData.php"));
        //GetDate("http://localhost/unityPatronus/getData.php");

        //StartCoroutine(GetData("http://localhost/unityPatronus/getUsers.php"));
        //StartCoroutine(UploadCoins(200));

        //StartCoroutine(UploadCoinsData(6000, "hello"));

        //get coins data

        //StartCoroutine(GetCoinsData("http://localhost/unityPatronus/getChartDataFromMySQL.php", coinData));
        //Debug.Log("ssssssssss" + coinData);
        //Debug.Log(coinData.Count);
        //IEnumerator enumerator = enumerable.GetEnumerator();
        /*   for (int i = 0; i < coinData.Count; i++)
           {
               if (coinData[i] != "")
               {
                   Debug.Log(coinData[i]);
               }
           }*/
    }

    IEnumerator GetData(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

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
                    break;
            }
        }
    }

    IEnumerator UploadCoins(int coins)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", coins);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unityPatronus/getChartData.php", form))
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

    //getData
    IEnumerator GetCoinsData(string uri, List<string> list)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
           
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
                    //List<string> list = new List<string>();
                    string [] coinData = webRequest.downloadHandler.text.Split("<br>");
                    for(int i = 0; i < coinData.Length; i++)
                    {
                        if(coinData[i] != "")
                        {
                            //Debug.Log(coinData[i]);
                            //XChartsTest.Instance.chartInfo.setCoinDBInfo(coinData[i]);
                            list.Add(coinData[i]);
                            //yield return coinData[i];
                        }
                    }

                    for(int i = 0; i < list.Count; i++)
                    {
                        Debug.Log(list[i]);
                    }
                    //yield return list.GetEnumerator();
                    break;
            }
        }
    }
}
