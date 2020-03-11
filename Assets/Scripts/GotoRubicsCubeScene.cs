using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using UnityEngine.UI;
using ToastPlugin;
using TMPro;
using System;

public class GotoRubicsCubeScene : MonoBehaviour {
	public AudioSource clickForButton;
    public TMP_InputField fullName;
    //public TMP_InputField lastname;
    //public TMP_InputField age;
    public TMP_InputField ageYear, ageMonth, ageDay;
    public TMP_InputField email;
    public KubikRub cubeScript1;
    //private string user1,lastname1,agestring1,mailstring1;

    private string agestring = string.Empty;    
    private string mailstring = string.Empty;
    string year, month, day, fullname;
    bool correctDate;

    public void goRubic()
    {
		GameObject cube = GameObject.Find ("superCube");
		clickForButton.Play ();
        SceneManager.LoadScene("Game");
    }

    public void changedata()
    {
        //Debug.Log(user1);
        //Debug.Log(lastname1);
        //Debug.Log(agestring1);
        //Debug.Log(mailstring1);
        //api keyi goturmek ucun bunu isledin
        string formApiKey = PlayerPrefs.GetString("uuid");
        //Debug.Log(PlayerPrefs.GetString("uuid"));
        string url1 = "http://kubirub.com/api/change/change_userdata";
        WWWForm apiform = new WWWForm();


        year = ageYear.text;
        month = ageMonth.text;
        day = ageDay.text;
        fullname = fullName.text;
        agestring = ageYear.text + "-" + ageMonth.text + "-" + ageDay.text;
        mailstring = email.text;
        apiform.AddField("fullname", fullname);
        //apiform.AddField("lastname", lastnamestring);
        apiform.AddField("age", agestring);
        apiform.AddField("email", mailstring);
        apiform.AddField("uuid", formApiKey);

        correctDate = checkDate(year, month, day);

        if (fullname.Length < 3)
        {
            errorShower_1(3);
        }
        else if (fullname.Length == 0)
        {
            errorShower_1(0);
        }
        else if (!mailstring.ToString().Contains("@"))
        {
            errorShower_1(4);
        }
        else if (!correctDate)
        {
            errorShower_1(5);
        }
        else
        {

            //Debug.Log("i am changingingingninginginig"); 
            WWW www1 = new WWW(url1, apiform);
            StartCoroutine(WaitForRequest(www1));
        }
        //getinfo(www1);
        //yield return www1;
        //Debug.Log(www1.text);
        //JsonData jsonval = JsonMapper.ToObject(www1.text);
        //JsonData user = jsonval["user"];
        //Debug.Log(user.ToString());
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);
            try
            {
                GetComponent<passwordforprofile>().cancelEditMode();

            }
            // handle the error
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
            }
        }
        else
        {
            //Passwordlenerror.SetActive(true);
            Debug.Log("WWW Error: " + www.text);
        }
    }
    public void errorShower_1(int i)
    {
        int l = PlayerPrefs.GetInt("Language");
        if (l == 0)
        {//in English
            switch (i)
            {
                case 0:
                    //errorWarnings.text = "*Make sure all fields are filled.";
                    ToastShow("Make sure all fields are filled.");
                    break;
                case 1:
                    //errorWarnings.text = "*Password must be at least 6 characters.";
                    ToastShow("Password must be at least 6 characters.");
                    break;
                case 2:
                    //errorWarnings.text = "*Make sure passwords are the same.";
                    ToastShow("Make sure passwords are the same.");
                    break;
                case 3:
                    //errorWarnings.text = "*The firstname must be at least 3 characters.";
                    ToastShow("The firstname must be at least 3 characters.");
                    break;
                case 4:
                    ToastShow("Incorrect email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    //Debug.Log("....");
                    break;
            }
        }
        else if (l == 1)
        {
            switch (i)
            {
                //in Russian
                case 0:
                    // errorWarnings.text = "*Будьте уверены,что все поля заполнены.";
                    ToastShow("Будьте уверены,что все поля заполнены.");
                    break;
                case 1:
                    //errorWarnings.text = "*Пароль должен содержать мин 6 символов.";
                    ToastShow("Пароль должен содержать мин 6 символов.");
                    break;
                case 2:
                    ToastShow("Будьте уверены,пароли одинаковы.");
                    //errorWarnings.text = "*Будьте уверены,пароли одинаковы.";
                    break;
                case 3:
                    ToastShow("Имя долно содержать мин 3 символа.");
                    //errorWarnings.text = "*Имя долно содержать мин 3 символа.";
                    break;
                case 4:
                    ToastShow("Неверный почтовый ящик!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    //Debug.Log("....");
                    break;
            }
        }
        else if (l == 2)
        {
            switch (i)
            {
                //in Turkish
                case 0:
                    //errorWarnings.text = "*Tüm metinlerin dolduğundan emin olun.";
                    ToastShow("Tüm metinlerin dolduğundan emin olun.");
                    break;
                case 1:
                    ToastShow("Şifre en az 6 karakterden oluşmak zorundadır.");
                    //errorWarnings.text = "*Şifre en az 6 karakterden oluşmak zorundadır.";
                    break;
                case 2:
                    ToastShow("Tüm metinlerin dolduğundan emin olun.");
                    //errorWarnings.text = "*Tüm metinlerin dolduğundan emin olun.";
                    break;
                case 3:
                    ToastShow("Sizin adınızda en az 3 karakter girilmelidir.");
                    //errorWarnings.text = "*Sizin adınızda en az 3 karakter girilmelidir.";
                    break;
                case 4:
                    ToastShow("Yanlış email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    //Debug.Log("....");
                    break;
            }
        }
        else if (l == 3)
        {
            switch (i)//in azeri
            {
                case 0:
                    ToastShow("Bütün sahələrin doldurulduğundan əmin olun.");
                    //errorWarnings.text = "*Bütün sahələrin doldurulduğundan əmin olun.";
                    break;
                case 1:
                    ToastShow("Parol ən azı 6 simvol olmalıdır.");
                    //errorWarnings.text = "*Parol ən azı 6 simvol olmalıdır.";
                    break;
                case 2:
                    ToastShow("Parolların eyniliyindən əmin olun.");
                    //errorWarnings.text = "*Parolların eyniliyindən əmin olun.";
                    break;
                case 3:
                    ToastShow("Sizin adınızda ən az 3 simvol olmalıdır.");
                    // errorWarnings.text = "*Sizin adınızda ən az 3 simvol olmalıdır.";
                    break;
                case 4:
                    ToastShow("Yanlış email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    // errorWarnings.text = "";
                    break;
            }
        }
    }
    void ToastShow(string toastText)
    {
        ToastHelper.ShowToast(toastText, false);
    }



    bool checkDate(string yearr, string monthh, string dayy)
    {
        DateTime dt;
        bool b = Int32.TryParse(yearr.Replace(" ", ""), out int j);
        return (DateTime.TryParse(yearr + "/" + monthh + "/" + dayy, out dt) && b && j > 1930 && j < DateTime.Now.Year-3);
    }
}
