using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RegisterUser : MonoBehaviour
{
    public TextMeshProUGUI NameRegisterInput;
    public TMP_InputField registername_inputField;
    public TextMeshProUGUI PassRegisterInput;
    public TMP_InputField registerpass_inputField;
    public TextMeshProUGUI RoleRegisterInput;
    public TMP_InputField registerrole_inputField;

    public Button RegisterButton2;

    // Start is called before the first frame update
    void Start()
    {
    NameRegisterInput.text = registername_inputField.text;
    PassRegisterInput.text = registerpass_inputField.text;
    RoleRegisterInput.text = registerrole_inputField.text;

    RegisterButton2.onClick.AddListener(() =>{
        StartCoroutine(Main.Instance.Web.RegisterUser1(registername_inputField.text, registerpass_inputField.text, registerrole_inputField.text));
        });  
    
    }

}
