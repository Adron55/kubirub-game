using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangSetterForRegister : MonoBehaviour
{

    public TMP_Text fullName, birthDate, mail, password, retryPassword, privacyPolicy;
    public Text help, contact, createAccount;

    string linkStart = "<link=\"http://kubirub.com/privacy-policy.html\"><color=blue>";
    string linkEnd = "</color></link>";


    // Start is called before the first frame update
    void Start()
    {
        languageForRubicsCube.lang_Register language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_Register;

        fullName.text = language.fullName;
        birthDate.text = language.birthDate;
        mail.text = language.mail;
        password.text = language.password;
        retryPassword.text = language.retryPassword;
        privacyPolicy.text = language.iAccept + linkStart + language.privacyPolicy + linkEnd + language.and + linkStart + language.termsConditions + linkEnd;
        help.text = language.help;
        contact.text = language.contact;
        createAccount.text = language.createAccount;
        //iAccept.text = language.iAccept;
        //and.text = language.and;
        //termsConditions.text = language.termsConditions;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
