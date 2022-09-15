using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string doctor_name { get; private set; }
    string UserName;
    string UserPassword;

    public void SetInfo(string username, string userpassword){
        UserName = username;
        UserPassword = userpassword;
    }

    public void SetID(string username){
        doctor_name = username;
    }



}
