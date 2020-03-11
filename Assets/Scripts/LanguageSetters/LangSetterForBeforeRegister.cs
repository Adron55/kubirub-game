using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangSetterForBeforeRegister : MonoBehaviour
{
    public Text playWin, haveAccount, createAccount, withoutRegister;
    // Start is called before the first frame update
    void Start()
    {
        languageForRubicsCube.lang_BeforeRegister language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_BeforeRegister;

        playWin.text = language.playWin;
        haveAccount.text = language.haveAccount;
        createAccount.text = language.createAccount;
        withoutRegister.text = language.withoutRegister;
    }

    
}
