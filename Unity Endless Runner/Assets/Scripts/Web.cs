using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetDate("http://localhost/EndlessRunner/GetDate.php"));
        //StartCoroutine(GetUsers("http://localhost/EndlessRunner/GetUsers.php"));
        //StartCoroutine(Login1("kevin", "123456789"));
        //StartCoroutine(RegisterUser("testuserttri", "20220828", "Doctor"));
        //StartCoroutine(GetPatients("http://localhost/EndlessRunner/GetPatients.php, form"));
        // A non-existing page.
        //StartCoroutine(GetDate("http://localhost/EndlessRunner/GetDate.php"));
    }

    public void ShowUserItems(){
        StartCoroutine(GetPatients(Main.Instance.UserInfo.doctor_name));
    }

    IEnumerator GetDate(string uri)
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

    IEnumerator GetUsers(string uri)
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

    public IEnumerator Login1(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/EndlessRunner/Login1.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Main.Instance.UserInfo.SetInfo(username, password);
                Main.Instance.UserInfo.SetID(www.downloadHandler.text);
            }
        }
    }

    
    public IEnumerator RegisterUser1(string username, string password, string roles_name)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("roleUser", roles_name);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/EndlessRunner/RegisterUser1.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetPatients(string doctor_name)
    {
        WWWForm form = new WWWForm();
        form.AddField("doctor_name", doctor_name);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/EndlessRunner/GetPatients.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                //Call callback function to pass results
            }
        }
    }
}
