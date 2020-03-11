using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using ToastPlugin;
using TMPro;
public class savepassword : MonoBehaviour
{
    public TMP_InputField oldpassword;
    public TMP_InputField newpassword;
    public TMP_InputField newpassword1;
    
 

    private string oldpasswordString = string.Empty;
    private string newpasswordString = string.Empty;
    private string newpasswordString1 = string.Empty;

    private void Awake()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }


    public void changepassword()
    {
        //api keyi goturmek ucun bunu isledin
        string formApiKey = PlayerPrefs.GetString("uuid");
        //Debug.Log(PlayerPrefs.GetString("uuid"));
        string url1 = "http://kubirub.com/api/change/password";
        WWWForm apiform = new WWWForm();

        oldpasswordString = oldpassword.text;
        newpasswordString = newpassword.text;
        newpasswordString1 = newpassword1.text;

        if (!string.Equals(newpasswordString, newpasswordString1))
        {
            errorShower_1(2);
        }
        else if (newpasswordString.Length<6)
        {
            errorShower_1(1);
        }
        else
        {
            apiform.AddField("old_password", oldpasswordString);
            apiform.AddField("password", newpasswordString);
            apiform.AddField("uuid", formApiKey);
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
        Debug.Log(www.text);
        string status = "";
        try
        {
            status = JsonMapper.ToObject(www.text)["status"].ToString();
        }
        catch
        {

        }
        // check for errors
        if (www.error == null &&  status == "success")
        {
            JsonData jsonvale = JsonMapper.ToObject(www.text);

            //Debug.Log("WWW Ok!: " + www.text);
            try
            {
                //String uuid = jsonvale["uuid"].ToString();
                //Debug.Log("json converted!Amksf: ");
                //bringscript=bringChangePanel.GetComponent<bringpanel>();
                //bringscript.cancelbutton();
                //PlayerPrefs.SetString("uuid", uuid);
                GetComponent<passwordforprofile>().cancelPassEditMode();

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
            //Debug.Log("WWW Error: " + www.text);
            errorShower_1(4);
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
                    ToastShow("The old password is incorrect");
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
                    ToastShow("The old password is incorrect");
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
                    ToastShow("Eski şifre yanlış");
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
                    ToastShow("Köhnə şifrə düzgün deyil");
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
}