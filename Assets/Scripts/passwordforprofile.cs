using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
public class passwordforprofile : MonoBehaviour
{

    public TMP_InputField profilemail;
    //public TMP_InputField profilename;
    public TMP_InputField profilefullname;
    //public TMP_InputField profileage;
    public TMP_InputField profileageYear, profileageMonth, profileageDay;
    public GameObject profilepassword;
    public GameObject[] editImages;
   

    public GameObject generalEditModeButton;

    public GameObject normalMode_middle;
    public GameObject passwordEditMode_middle;
    public GameObject passwordEditButtons;
    

    public GameObject normalMode_down;
    public GameObject editMode_down;

    public GameObject gender;
    public GameObject genderEdit;
    public GameObject boy, girl, defGend;

    public GameObject logoutPopout;
    public GameObject loadScenePanel;

    private void Awake()
    {

        StartCoroutine(getInfo());


        if (PlayerPrefs.GetInt("info", 1) == 1)
        {
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
        }
    }




    IEnumerator getInfo()
    {
        yield return new WaitForEndOfFrame();
        //api keyi goturmek ucun bunu isledin
        loadScenePanel.SetActive(true);
        string formApiKey = PlayerPrefs.GetString("uuid");
        //Debug.Log(PlayerPrefs.GetString("uuid"));
        string url1 = "http://kubirub.com/api/getInfo";
        WWWForm apiform = new WWWForm();
        apiform.AddField("uuid", formApiKey);
        WWW www1 = new WWW(url1, apiform);
        //getinfo(www1);
        yield return www1;
        //Debug.Log(www1.text);
        //PlayerPrefs.SetString("www_text", www1.text);
        JsonData jsonval = JsonMapper.ToObject(www1.text);
        try
        {
            JsonData user = jsonval["user"];
            profilemail.text = user["email"].ToString();
            //PlayerPrefs.SetString("mailstring", mailstring);



            //string nnamme = user["fullname"].ToString().Remove(user["fullname"].ToString().IndexOf(" "));
            //string sirrrrnameee = user["fullname"].ToString().Remove(0, user["fullname"].ToString().IndexOf(" ")+1);

            //profilename.text = nnamme;
            //PlayerPrefs.SetString("namestring",namestring);

            profilefullname.text = user["fullname"].ToString();
            //PlayerPrefs.SetString("lastname", surnamestring);

            //Debug.Log(user["age"].ToString());
            string[] temp = user["age"].ToString().Split('-');
            profileageYear.text = temp[0];
            profileageMonth.text = temp[1];
            profileageDay.text = temp[2];
            loadScenePanel.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        int gend = PlayerPrefs.GetInt("gender", -1);
        if(gend == 1)
        {
            boy.SetActive(true);
            girl.SetActive(false);
            defGend.SetActive(false);
        }
        else if(gend == 0)
        {
            boy.SetActive(false);
            girl.SetActive(true);
            defGend.SetActive(false);
        }
        else
        {
            boy.SetActive(false);
            girl.SetActive(false);
            defGend.SetActive(true);
        }
        
    }
    


    public  void generalEditMode()
    {
        profilemail.interactable = true;
        //profilename.interactable = true;
        profilefullname.interactable = true;
        profileageYear.interactable = true;
        profileageMonth.interactable = true;
        profileageDay.interactable = true;

        for (int i = 0; i < editImages.Length; i++)
        {
            editImages[i].SetActive(true);
        }

        profilepassword.SetActive(true);
        generalEditModeButton.SetActive(true);

        normalMode_down.SetActive(false);
        editMode_down.SetActive(true);

        gender.GetComponent<Button>().interactable = true;
    }

    public void passEditMode()
    {
        passwordEditMode_middle.SetActive(true);
        normalMode_middle.SetActive(false);

        passwordEditButtons.SetActive(true);
        editMode_down.SetActive(false);

    }


    public void cancelPassEditMode()
    {
        passwordEditMode_middle.SetActive(false);
        normalMode_middle.SetActive(true);

        passwordEditButtons.SetActive(false);
        editMode_down.SetActive(true);
    }


    public void saveNewPass()
    {
        GetComponent<savepassword>().changepassword();
    }



    public void genderEditMode()
    {
        genderEdit.SetActive(true);
    }

    public void genderSelect(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetInt("gender", 1);
        }
        else
        {
            PlayerPrefs.SetInt("gender", 0);
        }
        boy.SetActive(b);
        girl.SetActive(!b);
        defGend.SetActive(false);
        genderEdit.SetActive(false);
    }

    public void cancelEditMode()
    {



        StartCoroutine(getInfo());
        profilemail.interactable = false;
        //profilename.interactable = false;
        profilefullname.interactable = false;
        profileageYear.interactable = false;
        profileageMonth.interactable = false;
        profileageDay.interactable = false;

        for (int i = 0; i < editImages.Length; i++)
            {
                editImages[i].SetActive(false);
            }

            profilepassword.SetActive(false);
            generalEditModeButton.SetActive(false);

            normalMode_down.SetActive(true);
            editMode_down.SetActive(false);

            gender.GetComponent<Button>().interactable = false;

        


    }



    public void logOut()
    {    
        PlayerPrefs.SetString("autologin", "");
        Destroy(GameObject.Find("MainGameObject"));
        Destroy(GameObject.Find("rotationContainer"));
        Destroy(GameObject.Find("rotationDummy1"));
        Destroy(GameObject.Find("rotationDummy2"));
        SceneManager.LoadScene("beforeLoginAndRegister");
    }




    string lastTextYear = "", lastTextMonth = "", lastTextDay = "";

    public void nextInput(int o)
    {
        //print("nese" + o);
        if (o == 0)
        {

            if (profileageYear.text.Length == 4)
            {
                if (lastTextYear != "")
                {
                    //print(0);
                    StartCoroutine(activateInputField(profileageMonth));
                    //profileageMonth.Select();
                    //profileageMonth.ActivateInputField();


                    resetLasts(1);
                    profileageYear.onValueChanged.RemoveAllListeners();
                }
            }
            lastTextYear = profileageYear.text;
        }
        else
        {
            if (profileageMonth.text.Length == 2)
            {
                if (lastTextMonth != "")
                {
                    //print(1);
                    StartCoroutine(activateInputField(profileageDay));
                    //profileageDay.Select();
                    //profileageDay.ActivateInputField();

                    profileageMonth.onValueChanged.RemoveAllListeners();
                }
            }
            lastTextMonth = profileageMonth.text;
        }
    }


    public void resetLasts(int b)
    {
        if (b == 0)
        {
            //print(00);
            lastTextYear = profileageYear.text;
            profileageYear.onValueChanged.AddListener(delegate { nextInput(0); });
        }
        else if (b == 1)
        {
            //print(11);
            //print("dfddddddddddddddddd");
            lastTextMonth = profileageMonth.text;
            profileageMonth.onValueChanged.AddListener(delegate { nextInput(1); });
        }
        else if (b == 2)
        {
            //print(22);
            profileageYear.onValueChanged.RemoveAllListeners();
        }
        else if (b == 3)
        {
            //print(33);
            profileageMonth.onValueChanged.RemoveAllListeners();
        }
    }




    IEnumerator activateInputField(TMP_InputField inputt)
    {
        Debug.Log("i am coorouting");
        yield return new WaitForEndOfFrame();
        inputt.Select();
        inputt.ActivateInputField();
        inputt.caretPosition = inputt.text.Length;
    }
}
