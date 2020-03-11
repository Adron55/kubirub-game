using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangSetterForMyProfile : MonoBehaviour
{

    public TMP_Text password, oldPass, newPass, retryNewPass;
    public Text myProfile, editProfile, logOut, yes, no, finish, select, select1, RYSureToLogOut, cancel, cancel1, finishPassword;


    // Start is called before the first frame update
    void Start()
    {
        languageForRubicsCube.lang_MyProfile language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_MyProfile;

        myProfile.text = language.myProfile;
        editProfile.text = language.editProfile;
        logOut.text = language.logOut;
        yes.text = language.yes;
        no.text = language.no;
        finish.text = language.finish;
        finishPassword.text = language.finish;
        select.text = language.select;
        select1.text = language.select;
        oldPass.text = language.oldPass;
        newPass.text = language.newPass;
        retryNewPass.text = language.retryNewPass;
        RYSureToLogOut.text = language.RYSureToLogOut;
        cancel.text = language.cancel;
        cancel1.text = language.cancel;
        password.text = language.password;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
