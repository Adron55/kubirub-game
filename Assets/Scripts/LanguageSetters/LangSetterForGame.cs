using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangSetterForGame : MonoBehaviour
{
    public TMP_Text volumeText, cameraText, soundsText, helpText, langBtnText, title,goOnline;
    public Text RYSureToQuit, yes, no, infoTitle, back;


    void Start()
    {
        setLang();   
    }



    public void setLang()
    {
        languageForRubicsCube.lang_Game language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_Game;

        volumeText.text = language.volumeText;
        cameraText.text = language.cameraText;
        soundsText.text = language.soundsText;
        title.text = language.titleText;
        langBtnText.text = language.langBtnText;
        helpText.text = language.helpText;
        RYSureToQuit.text = language.RYSureToQuit;
        yes.text = language.yes;
        no.text = language.no;
        infoTitle.text = language.infoTitle;
        back.text = language.back;
        goOnline.text =language.goToOnlineTxt;
    }
    
}
