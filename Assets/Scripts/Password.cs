using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.EventSystems;
using ToastPlugin;
using System.Net.NetworkInformation;
using TMPro;


public class Password : MonoBehaviour {
    public GameObject popUp;
    public GameObject loadingPanel;
    public Text texteditorForMac;
    public TMP_InputField email;
    public TMP_InputField pass;
    public TMP_InputField pass2;
    public TMP_InputField fullName;
    //public InputField secondName;
    public TMP_InputField year, month, day;

    public InputField profilemail;
    public InputField profilename;
    public InputField profilesurname;
    public InputField profileage;
    public InputField profilepass;
    public InputField profilepass1;
    
    //Hashtable data = new Hashtable();
    private string usernameString = string.Empty;
    private string passwordString = string.Empty;
    private string ageString = string.Empty;
    private string firstnameString = string.Empty;
    private string secondnameString = string.Empty;
    private string active_scene_name;


    private bool Equality = false;

    public Text DiscountsnameText;
    public Toggle autologin;
    public Text connect;

    public Text Passwordlenerror;
    public InputField firstinputfield;


    [SerializeField]
    Image emailInput, passwordInput;
    [SerializeField]
    Text nida_login, nida_password;
    [SerializeField]
    Toggle eye;
    float eyeOffSet = 10f;



    //For Sign-up
    public Text mailwarning;
    public Toggle checkbox;
    public Button Create_Account;
    public void Awake()
    {


        if (PlayerPrefs.GetInt("info", 1) == 1)
        {
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
        }
        active_scene_name = SceneManager.GetActiveScene().name;
        if (false  && HasConnection() == true)
        {
            StartCoroutine(getInfoforbasket());
            string formApiKey = PlayerPrefs.GetString("uuid");
        }
    }

    public static bool HasConnection()
    {
        try
        {
            
            if(Application.internetReachability != NetworkReachability.NotReachable)
            {
                return true;
            }
            else
                return false;
        }
        catch
        {
            //errorShower(1);
            errorShower(1);
            return false;
        }
    }
    public void show() {
        profilemail.gameObject.SetActive(false);
        profilename.gameObject.SetActive(false);
        profilesurname.gameObject.SetActive(false);
        profileage.gameObject.SetActive(false);
        popUp.SetActive(true);
    }
    public void disappear() {
        profilemail.gameObject.SetActive(true);
        profilename.gameObject.SetActive(true);
        profilesurname.gameObject.SetActive(true);
        profileage.gameObject.SetActive(true);
        popUp.SetActive(false);
    }
    public void exitgame()
    {
        PlayerPrefs.SetString("autologin", "");
        Destroy(GameObject.Find("MainGameObject"));
        Destroy(GameObject.Find("rotationContainer"));
        Destroy(GameObject.Find("rotationDummy1"));
        Destroy(GameObject.Find("rotationDummy2"));
        SceneManager.LoadScene("beforeLoginAndRegister");
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void goRegister()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Register");
    }

    public void Register()
    {
        usernameString = email.text;
        passwordString = pass.text;

        firstnameString = fullName.text;
        ageString = year.text + "-" + month.text + "-" + day.text;

        string url = "http://kubirub.com/api/register";
        WWWForm form = new WWWForm();
        form.AddField("email", usernameString);
        form.AddField("password", passwordString);
        form.AddField("age", ageString);
        form.AddField("fullname", firstnameString);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest1(www));
    }
    public void Login()
    {
        if (HasConnection() == true)
        {
            startLoadingPanel(true);
            usernameString = email.text;
            passwordString = pass.text;

            string url = "http://kubirub.com/api/login";
            //Header
            Hashtable postHeader = new Hashtable();
            postHeader.Add("Content-Type", "application/json");
            WWWForm form = new WWWForm();
            form.AddField("email", usernameString);
            form.AddField("password", passwordString);
            WWW www = new WWW(url, form);
            StartCoroutine(WaitForRequest(www));
        }

    }

    public void Passto()
    {
        //Debug.Log("down");
        //Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();

        firstinputfield.GetComponent<Selectable>().Select();

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            //Debug.Log("down");
            Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (selectable != null)
                selectable.Select();
        }


    }

    IEnumerator getInfoforbasket()
    {
        //api keyi goturmek ucun bunu isledin
        string formApiKey = PlayerPrefs.GetString("uuid");
        //Debug.Log(PlayerPrefs.GetString("uuid"));
        string url1 = "http://kubirub.com/api/getInfo";
        WWWForm apiform = new WWWForm();
        apiform.AddField("uuid", formApiKey);
        WWW www1 = new WWW(url1, apiform);
        //getinfo(www1);
        yield return www1;
        JsonData jsonval = JsonMapper.ToObject(www1.text);
        JsonData user = jsonval["user"];
        if (DiscountsnameText != null)
        {
            DiscountsnameText.text = user["firstname"].ToString() + " " + user["lastname"].ToString();
        }

    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        bool loggedS = false;
        // check for errors
        if (www.error == null)
        {
            
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            try
            {
                String uuid = jsonvale["uuid"].ToString();
                if (autologin.isOn)
                {
                    PlayerPrefs.SetString("autologin", "on");
                }
                else
                {
                    PlayerPrefs.SetString("autologin", "off");
                }
                PlayerPrefs.SetString("uuid", uuid);
                loggedS = true;
            }
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
                if (!nida_login.IsActive() || !nida_password.IsActive())
                {
                    Color red;
                    ColorUtility.TryParseHtmlString("#FF0000", out red);
                    emailInput.color = red;
                    passwordInput.color = red;
                    nida_login.gameObject.SetActive(true);
                    nida_password.gameObject.SetActive(true);
                    eye.GetComponent<RectTransform>().localPosition = new Vector3(eye.GetComponent<RectTransform>().localPosition.x - eyeOffSet, eye.GetComponent<RectTransform>().localPosition.y, eye.GetComponent<RectTransform>().localPosition.z);
                }
                startLoadingPanel(false);
            }
            
        }
        else
        {
            errorShower(2);
            startLoadingPanel(false);
        }
        yield return new WaitForEndOfFrame();
        if (loggedS)
        {
            var async = SceneManager.LoadSceneAsync("Game");
            if (async.isDone)
            {
                startLoadingPanel(false);
            }
        }
        

    }
    public void startLoadingPanel(bool f)
    {
        loadingPanel.SetActive(f);
    }
    IEnumerator WaitForRequest1(WWW www)
    {
        yield return www;
        JsonData jsonvale = JsonMapper.ToObject(www.text);

        // check for errors
        if (www.error == null)
        {
            Debug.Log(jsonvale.ToJson());
            //Debug.Log("WWW Ok!: " + www.text);
            try
            {
                String uuid = jsonvale["uuid"].ToString();
                SceneManager.LoadSceneAsync("Login");
            }
            // handle the error
            catch (System.Exception err)
            {
                errorShower(2);
                Debug.Log("Got: " + err);
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.text);
        }

        //api keyi goturmek ucun bunu isledin
        //Debug.Log(PlayerPrefs.GetString("uuid"));


    }
    public void goLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void goback()
    {
        SceneManager.LoadScene("Game");
    }
    //It might be useable--below
    //public void ShowMacAddress(){
    //	IPGlobalProperties compProp = IPGlobalProperties.GetIPGlobalProperties ();
    //	NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces ();
    //	string info = "";
    //	foreach(NetworkInterface adapter in nics){
    //		PhysicalAddress address = adapter.GetPhysicalAddress ();
    //		byte[] bytes = address.GetAddressBytes ();
    //		string mac = "";
    //		for(int i=0;i<bytes.Length;i++){
    //			mac = string.Concat (mac+(string.Format("{0}",bytes[i].ToString("X2"))));
    //			if(i!=bytes.Length-1){
    //				mac = string.Concat (mac+"-");

    //			}
    //		}
    //		info+=mac + "  ";

    //		//info+="\n";
    //	}
    //	texteditorForMac.text = info;
    //	PlayerPrefs.SetString ("MAC",info);
    //	Debug.Log (info);
    //}

    public void CheckIfToogleFalse()
    {
        if (checkbox.isOn && fullName.text != "" && email.text != "" && pass.text != "" && pass2.text != "" && year.text != "" && month.text != "" && day.text != "")
        {
           
                //Create_Account.GetComponent<Image>().color = Color.white;
                Create_Account.GetComponent<Button>().interactable = true;
            
        }
        else
        {

            //Create_Account.GetComponent<Image>().color = new Color32(202,190,190,255);
            Create_Account.GetComponent<Button>().interactable = false;
        }
    }

    public void openurlfor(int i)
    {
        if (i == 0)
        {
            Application.OpenURL("http://kubirub.com/privacy-policy.html");
        }
        else if (i == 1)
        {
            Application.OpenURL("http://kubirub.com/support");
        }
        else
        {
            Application.OpenURL("http://kubirub.com");
        }
    }
    public void openurlForForgetPassword() {
        Application.OpenURL("http://kubirub.com/password/reset");
    }
    private static void errorShower(int i)
    {
        int l = PlayerPrefs.GetInt("Language");
        switch (l)
        {
            case 0:
                if (i == 1)
                {
                    //mailwarning.text = "*You are not connected to the internet.";
                    ToastShower("You are not connected to the internet.");
                }
                else if (i == 2)
                {
                    // mailwarning.text = "*The email address must be a valid.";
                    ToastShower("Password or email isn't correct.");
                    
                }
                break;
            case 1:
                //in russian
                if (i == 1)
                {
                    //mailwarning.text = "*Вы не подключены к интернету.";
                    ToastShower("Вы не подключены к интернету.");
                }
                else if (i == 2)
                {
                    //mailwarning.text = "*Эл.адрес должен быть действительным.";
                    ToastShower("Пароль или Эл.адрес неправильный.");
                }
                break;
            case 2:
                //in Turkish
                if (i == 1)
                {
                    //mailwarning.text = "*Cihazınız internete bağlı değil.";
                    ToastShower("Cihazınız internete bağlı değil.");
                }
                else if (i == 2)
                {
                    //mailwarning.text = "*E-posta doğruluğundan emin olun.";
                    ToastShower("Şifre ve ya e-posta yanlışdır.");
                }
                break;
            case 3:
                if (i == 1)
                {
                    //mailwarning.text = "*Smartfonunuz internetə qoşulmayıb.";
                    ToastShower("Smartfonunuz internetə qoşulmayıb.");
                }
                else if (i == 2)
                {
                    //mailwarning.text = "*Belə bir emailin olduğundan əmin olun.";
                    ToastShower("Parol və ya email yanlışdır.");
                }
                break;
        }

    }
    private static void ToastShower(string text)
    {
        ToastHelper.ShowToast(text, false);
    }
    public void changeType()
    {
        //Debug.Log("@@@@@@@@@@@@@@@@@@"+pass.GetComponent<InputField>().name);
        pass.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
    }


    public void getDefaultColor()
    {
        if (nida_login.IsActive() || nida_password.IsActive())
        {
            Color goy;
            ColorUtility.TryParseHtmlString("#203A7F", out goy);
            emailInput.color = goy;
            passwordInput.color = goy;
            nida_login.gameObject.SetActive(false);
            nida_password.gameObject.SetActive(false);
            eye.GetComponent<RectTransform>().localPosition = new Vector3(eye.GetComponent<RectTransform>().localPosition.x + eyeOffSet, eye.GetComponent<RectTransform>().localPosition.y, eye.GetComponent<RectTransform>().localPosition.z);
        }
    }


    public void makePasswordVisible()
    {
        bool on = eye.isOn;

        if (on)
        {
            //Debug.Log("standart");
            //passwordInput.GetComponent
            passwordInput.GetComponentInParent<TMP_InputField>().contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            //Debug.Log("pass");
            passwordInput.GetComponentInParent<TMP_InputField>().contentType = TMP_InputField.ContentType.Password;
        }

        passwordInput.GetComponentInParent<TMP_InputField>().ForceLabelUpdate();
        passwordInput.GetComponentInParent<TMP_InputField>().GraphicUpdateComplete();
    }


    public void makeNidaVisible(TMP_InputField field)
    {
        Color red;
        ColorUtility.TryParseHtmlString("#FF0000", out red);
        field.GetComponent<Image>().color = red;
        field.transform.GetChild(2).gameObject.SetActive(true);
        //activates the child which is a red nida, "!".
    }

    public void makeNidaInvisible()
    {
        TMP_InputField field = gameObject.GetComponent<TMP_InputField>();
        Color goy;
        ColorUtility.TryParseHtmlString("#203A7F", out goy);
        field.GetComponent<Image>().color = goy;
        field.transform.GetChild(2).gameObject.SetActive(false);
        //activates the child which is a red nida, "!".

    }



}

