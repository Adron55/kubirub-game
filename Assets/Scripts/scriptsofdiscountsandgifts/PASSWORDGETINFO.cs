using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
public class PASSWORDGETINFO : MonoBehaviour {
    public Text mineText;
    //Hashtable data = new Hashtable();
    private string usernameString = string.Empty;
    private string passwordString = string.Empty;
    private string firstnameString = string.Empty;
    private string secondnameString = string.Empty;
    private bool Equality = false;

    public GameObject Passwordlenerror;

    //public  void Start_check()
    // {
    //     if (string.Equals(pass.text, pass2.text))
    //     {
    //         Equality = true;
    //     }
    //     else
    //     {
    //         while (string.Equals(pass.text, pass2.text))
    //         {
    //             pass.text = "";
    //             pass2.text = "";
    //         }
    //     }
    // }

    //void OnGUI()
    //{
    //    // GUI.Window(0, windowRect, WindowFunction, "Log-in");
    //    WindowFunction();
    //}
    /*
    public void WindowFunction()
    {
        //User input log-in
        usernameString = name.text;
        //User input password 
        passwordString = pass.text;
        firstnameString = fullName.text;
        secondnameString = secondName.text;
        //data.Add(usernameString, passwordString);
        
        Register();
        //SceneManager.LoadScene("rubicsCube");
    }*/

    public void goRegister()
    {
        SceneManager.LoadScene("signup");
    }
     void Start()
    {
        StartCoroutine(getinfo());
    }

    IEnumerator getinfo()
    {
        string formApiKey = PlayerPrefs.GetString("api_key");
        //Debug.Log(PlayerPrefs.GetString("api_key"));
        string url1 = "http://kubirub.com/api/getInfo";
        WWWForm apiform = new WWWForm();
        apiform.AddField("api_key", formApiKey);
        WWW www1 = new WWW(url1, apiform);
        yield return www1;
        //Debug.Log(www1.text);
        JsonData jsonval = JsonMapper.ToObject(www1.text);
        JsonData user = jsonval["user"];
        //Debug.Log(user["firstname"].ToString());
        mineText.text = user["firstname"].ToString()+" "+ user["lastname"].ToString();
        //mineText.text = user["lastname"].ToString();

    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);

            //Debug.Log("WWW Ok!: " + www.text);
            try
            {

                String api_key = jsonvale["api_key"].ToString();
                //Debug.Log("json converted!: " + api_key);
                SceneManager.LoadScene("Game");
                PlayerPrefs.SetString("api_key", api_key);
            }
            // handle the error
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
            }
        }
        else
        {
            Passwordlenerror.SetActive(true);
            //Debug.Log("WWW Error: " + www.text);
        }

        //api keyi goturmek ucun bunu isledin
        //string formApiKey = PlayerPrefs.GetString("api_key");
        //Debug.Log(PlayerPrefs.GetString("api_key"));
        //string url1 = "http://kubirub.online/api/getInfo";
        //WWWForm apiform = new WWWForm();
        //apiform.AddField("api_key", formApiKey);
        //WWW www1 = new WWW(url1, apiform);
        //yield return www1;
        //Debug.Log(www1.text);
        //JsonData jsonval = JsonMapper.ToObject(www1.text);

        ////String user = jsonval["status"].ToString();
        //Debug.Log(jsonval["user"]["firstname"].ToString());
        //JsonData jsonva = JsonMapper.ToObject(user);
        //try
        //{

        //    String firstname = jsonva["firstname"].ToString();
        //    Debug.Log(firstname);
        //}
        //// handle the error
        //catch (System.Exception err)
        //{
        //    Debug.Log("Got: " + err);
        //}

    }
    IEnumerator WaitForRequest1(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);

            //Debug.Log("WWW Ok!: " + www.text);
            try
            {
                String api_key = jsonvale["api_key"].ToString();
                //Debug.Log("json converted!: " + api_key);
                SceneManager.LoadScene("basket_plane");
                PlayerPrefs.SetString("api_key", api_key);
            }
            // handle the error
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
            }
        }
        else
        {
            //Debug.Log("WWW Error: " + www.text);
        }

        //api keyi goturmek ucun bunu isledin
        //Debug.Log(PlayerPrefs.GetString("api_key"));


    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.LoadScene("rubicsCube");
        //}
    }
    public void goLogin()
    {
        SceneManager.LoadScene("basket_plane");
    }
}
