using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{

    public TextMeshProUGUI UsernameInput;
    public TMP_InputField username_inputField;
    public TextMeshProUGUI PasswordInput;
    public TMP_InputField pass_inputField;

    public Button LoginButton;
    // Start is called before the first frame update
    void Start()
    {
        UsernameInput.text = username_inputField.text;
        PasswordInput.text = pass_inputField.text;

        LoginButton.onClick.AddListener(() =>{
            StartCoroutine(Main.Instance.Web.Login(username_inputField.text, pass_inputField.text));
        });
    }

}
