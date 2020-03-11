using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangSetterForLogin : MonoBehaviour
{
    public TMP_Text username, password;
    public Text rememberMe, forgetPass, singIn, withoutregister;

    void Start()
    {

        languageForRubicsCube.lang_Login language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_Login;
      

        username.text = language.username;
        password.text = language.password;
        rememberMe.text = language.rememberMe;
        forgetPass.text = language.forgetPass;
        singIn.text = language.signIn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
