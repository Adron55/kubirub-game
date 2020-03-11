using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToastPlugin;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Globalization;

public class SignUp : MonoBehaviour {
    string t;
    string x;
    string z;
    string c1;
    string c2;
    string year, month, day;
    bool correctDate = false;
    public TMP_InputField mainInput;
    //public TMP_InputField surInput;
    public TMP_InputField ageInputYear, ageInputMonth, ageInputDay;
    public TMP_InputField emailInput;
    public TMP_InputField pasInput1;
    public TMP_InputField pasInput2;
    public TMP_Text textcure;
    public TMP_Text textcure2;
    public Button signupButton;
    public GameObject send_information;
    public Password password1;
    public Text errorWarnings;
    public GameObject errors;
    public GameObject paslenerror;
    public GameObject lenOfName;
    public GameObject mailwarning;
    public Text tagText;
    public GameObject checkTag;

    public void Start_fun() {


        c1 = pasInput1.text;
        c2 = pasInput2.text;
        t = mainInput.text;
        year = ageInputYear.text;
        month = ageInputMonth.text;
        day = ageInputDay.text;

        z = emailInput.text;
        /*
       // errorWarnings.SetActive(false);1
        errors.SetActive(false);
        paslenerror.SetActive(false);
        lenOfName.SetActive(false);
        */
        // SceneManager.LoadScene("rubicsCube");
        //burada string.Equals funksiyasi bize iki stiringin beraber olub olmamasini yoxlayir...
        correctDate = checkDate(year, month, day);

        if (!correctDate)
        {
            errorShower_1(5);
        }
        else if (string.Equals(c1, c2) && t.Length >= 6 && z.Length != 0 && c1.Length >= 6 && c2.Length >= 6)
        {
            password1.Register();

        }
        else if (!string.Equals(c1, c2))
        {
            errorShower_1(2);

        }
        else if (t.Length < 6)
        {
            errorShower_1(3);
        }
        else if (t.Length == 0 || z.Length == 0 || c1.Length == 0 || c2.Length == 0)
        {
            errorShower_1(0);
        }
        else if (c1.Length < 6 || c2.Length < 6)
        {
            errorShower_1(1);
        }
        else if (!z.ToString().Contains("@"))
        {
            errorShower_1(4);
        }

    }
    public void visible(bool tog)
    {
        if (tog)
        {
            pasInput1.contentType = TMP_InputField.ContentType.Password;
        }
        else {
            pasInput1.contentType = TMP_InputField.ContentType.Standard;
        }
    }
    public void visible2(bool tog2)
    {
        if (tog2)
        {
            pasInput2.contentType = TMP_InputField.ContentType.Password;
        }
        else
        {
            pasInput2.contentType = TMP_InputField.ContentType.Standard;
        }
    }
	public bool Tags(){
			
		//Debug.Log(tagText.text.Split (','));
		//Debug.Log ("dd");
		return true;
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
                    ToastShow("The fullname must be at least 6 characters.");
                    break;
                case 4:
                    ToastShow("Incorrect email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    errorWarnings.text = "";
                    break;
            }
        }
        else if (l == 1) {
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
                    ToastShow("Имя долно содержать мин 6 символа.");
                    //errorWarnings.text = "*Имя долно содержать мин 3 символа.";
                    break;
                case 4:
                    ToastShow("Неверный почтовый ящик!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    errorWarnings.text = "";
                    break;
            }
        }
        else if (l==2)
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
                    ToastShow("Şifreler aynı değil.");
                    //errorWarnings.text = "*Tüm metinlerin dolduğundan emin olun.";
                    break;
                case 3:
                    ToastShow("Sizin adınızda en az 6 karakter girilmelidir.");
                    //errorWarnings.text = "*Sizin adınızda en az 3 karakter girilmelidir.";
                    break;
                case 4:
                    ToastShow("Yanlış email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    errorWarnings.text = "";
                    break;
            }
        }
        else if(l==3)
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
                    ToastShow("Sizin adınızda ən az 6 simvol olmalıdır.");
                   // errorWarnings.text = "*Sizin adınızda ən az 3 simvol olmalıdır.";
                    break;
                case 4:
                    ToastShow("Yanlış email!");
                    break;
                case 5:
                    ToastShow("Incorrect date!");
                    break;
                default:
                    errorWarnings.text = "";
                    break;
            }
        }
    }
    void ToastShow(string toastText)
    {
        ToastHelper.ShowToast(toastText,false);
    }


    bool checkDate(string yearr, string monthh, string dayy)
    {
        DateTime dt;
        bool b = Int32.TryParse(yearr.Replace(" ", ""), out int j);
        return (DateTime.TryParse(yearr + "/" + monthh + "/" + dayy, out dt) && b && j > 1930 && j < DateTime.Now.Year - 3);
    }



    string lastTextYear = "", lastTextMonth = "", lastTextDay = "";

    public void nextInput(int o)
    {
        if (o == 0)
        {

            if (ageInputYear.text.Length == 4)
            {
                if (lastTextYear != "")
                {
                    ageInputMonth.Select();
                    ageInputMonth.ActivateInputField();


                    resetLasts(1);
                    ageInputYear.onValueChanged.RemoveAllListeners();
                }
            }
            lastTextYear = ageInputYear.text;
        }
        else
        {
            if (ageInputMonth.text.Length == 2)
            {
                if (lastTextMonth != "")
                {
                    ageInputDay.Select();
                    ageInputDay.ActivateInputField();
                    
                    ageInputMonth.onValueChanged.RemoveAllListeners();
                }
            }
            lastTextMonth = ageInputMonth.text;
        }
    }


    public void resetLasts(int b)
    {
        if (b == 0)
        {
            lastTextYear = "";
            ageInputYear.onValueChanged.AddListener(delegate { nextInput(0); });
        }
        else if(b == 1)
        {
            lastTextMonth = "";
            ageInputMonth.onValueChanged.AddListener(delegate { nextInput(1); });
        }
        else if(b == 2)
        {
            ageInputYear.onValueChanged.RemoveAllListeners();
        }
        else if (b == 3)
        {
            ageInputMonth.onValueChanged.RemoveAllListeners();
        }
    }

}
