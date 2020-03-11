using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
using UnityEngine.SceneManagement;
public class Get_Info : MonoBehaviour {

    public InputField name;
    public InputField ageinput;
    public InputField pass;
    public InputField pass2;
    public InputField firstName;
    public InputField secondName;
    //Hashtable data = new Hashtable();
    private string usernameString = string.Empty;
    private string passwordString = string.Empty;
    private string firstnameString = string.Empty;
    private string secondnameString = string.Empty;
    private string ageString = string.Empty;
    private bool Equality = false;
    // Use this for initialization

    void Start () {
        Screen.orientation = ScreenOrientation.Portrait;

    }
    void chechk2()
    {
      //getInformatin.API("http://kubirub.online/api/getInfo", fname.HttpMethod.GET, ret);
    }

	public void getInformation()
    {
       // Passwordlenerror.SetActive(false);
        usernameString = name.text;

        passwordString = pass.text;
        string url = "http://kubirub.com/api/getInfo";


        //Header
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        WWWForm form = new WWWForm();
        form.AddField("email", usernameString);
        //form.AddField("firstname", "Anar");
        //form.AddField("lastname", "Memmedli");
        form.AddField("password", passwordString);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));

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
                String uuid = jsonvale["uuid"].ToString();
                //Debug.Log("json converted!: " + uuid);
                //SceneManager.LoadScene("rubicsCube");
                PlayerPrefs.SetString("uuid", uuid);
            }
            // handle the error
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
            }
        }
        else
        {
           // Passwordlenerror.SetActive(true);
            //Debug.Log("WWW Error: " + www.text);
        }

        //api keyi goturmek ucun bunu isledin
        //Debug.Log(PlayerPrefs.GetString("uuid"));


    }
}
