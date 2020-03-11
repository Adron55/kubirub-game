using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.EventSystems;
using TMPro;
using ToastPlugin;
public class OldCouponsActivator : MonoBehaviour {
	//public AudioSource clickForButtons;
	public GameObject coupon, couponParent,loadingPanel, feedbackPopUp, feedbackMesssage, feedbackPopUpSent;
    private string couponDes, coupid, sts, winCoupon, pageNumber;
    public TMP_Text feedbackCompany, feedbackId, feedbackproduct;
    public TMP_InputField feedbackComment;
    public Text commentLength;
    private int leng;
    int countOld;
    JsonData jsonOld;
    int chosen;


    public void oldCoupons(){
        StartCoroutine(getOldCoupons());
	}

	IEnumerator getOldCoupons(){

        loadingPanel.SetActive(true);

        deleteCoupons();


		string urlOld= "http://kubirub.com/api/getOldCoupons";
		WWWForm apiOld = new WWWForm ();
		string formApiKey = PlayerPrefs.GetString ("uuid");
		apiOld.AddField ("uuid", formApiKey);

		WWW www = new WWW (urlOld,apiOld);
		yield return www;
		//Debug.Log (www.text);
		jsonOld = JsonMapper.ToObject (www.text);
		try {
            pageNumber = jsonOld["current_page"].ToString();

            var  myArr = jsonOld["data"];
            leng = myArr.Count;
			//sts = jsonOld ["status"].ToString ();
			//sts = jsonOld ["status"].ToString ();
			//sts = jsonOld ["status"].ToString ();
			//winCoupon = jsonOld["content"].ToString ();
		    //Debug.Log ("json converted!: " +pageNumber);
            
		}
		// handle the error
		catch (System.Exception err) {
			Debug.Log ("Got: " + err);
		}
        var myOldList = jsonOld["data"];
		countOld = leng;
		//Debug.Log (jsonOld.ToJson());
		if (countOld != 0) {
            GameObject coupBut1;
            string name = "Nameee", product = "Producttt";
            for (int i = 0; i < countOld; i++)
            {
                if(jsonOld["data"][i]["name"] != null)
                {
                    name = jsonOld["data"][i]["name"].ToString();
                }
                if (jsonOld["data"][i]["product"] != null)
                {
                    product = jsonOld["data"][i]["product"].ToString();
                }
                couponDes = name + " " + product;
                coupBut1 = Instantiate(coupon,couponParent.transform);
                //coupBut1.transform.parent = couponParent.transform;
                coupBut1.transform.GetChild(0).GetComponent<Text>().text = couponDes;
                coupBut1.name = jsonOld["data"][i]["id"].ToString();
                coupBut1.gameObject.GetComponent<Button>().onClick.AddListener(delegate { feedbackPop();});
			}
		}
        yield return new WaitForEndOfFrame();
        loadingPanel.SetActive(false);
    }



    public void deleteCoupons()
    {
        int cnt = couponParent.transform.childCount;
        for(int i = 0; i < cnt; i++)
        {
            Destroy(couponParent.transform.GetChild(i).gameObject);
        }
    }




    public void feedbackPop()
    {
        Debug.Log("feedback");
        GameObject chosenGO = EventSystem.current.currentSelectedGameObject;
        string coupId = chosenGO.name;

        chosen = 0;
        for (int i = 0; i < countOld; i++)
        {
            if (jsonOld["data"][i]["id"] != null)
            {
                if (jsonOld["data"][i]["id"].ToString() == coupid)
                {
                    chosen = i;
                    break;
                }
            }
          
        }
        string id = "Idd", name = "Namee", product = "Productt";
        if(jsonOld["data"][chosen]["name"] != null)
        {
            name = jsonOld["data"][chosen]["name"].ToString();
        }
        if (jsonOld["data"][chosen]["id"] != null)
        {
            id = jsonOld["data"][chosen]["id"].ToString();
        }
        if(jsonOld["data"][chosen]["product"] != null)
        {
            product = jsonOld["data"][chosen]["product"].ToString();
        }
            feedbackCompany.text = name;
        feedbackId.text = "Code: " + id;
        feedbackproduct.text = product;

        feedbackPopUp.SetActive(true);
    }


    public void feedbackMessagePOP()
    {
        feedbackMesssage.SetActive(true);
        feedbackComment.Select();
        feedbackComment.ActivateInputField();

    }

    public void feedbackSendd()
    {
        string url = "kubirub.com/api/sendCompanyFeedback";
        string uuid = PlayerPrefs.GetString("uuid");


        string message = feedbackComment.text;

        if (message.Length != 0 && message.Length <= 250)
        {
            WWWForm form = new WWWForm();
            form.AddField("uuid", uuid);
            form.AddField("company_id", jsonOld["data"][chosen]["company_id"].ToString());
            form.AddField("message", message);
            WWW www = new WWW(url, form);
            StartCoroutine( postApi(www));
            //apiiiiiiiiiiii
           
        }
        else
        {
            showToast("invalid length");
        }
    }//uuid, company_id, message


    IEnumerator postApi(WWW www)
    {
        yield return www;
        string succ = "";

        JsonData myobj = JsonMapper.ToObject(www.text);

        if (myobj.Keys.Contains("status"))
        {
            succ = myobj["status"].ToString();
        }



        if(www.error == null && succ.Equals("success"))
        {
            feedbackMesssage.SetActive(false);
            feedbackPopUp.SetActive(false);

            feedbackPopUpSent.SetActive(true);
            
            showToast(LangSetterForDiscounts.feedbackSentSuccess);
        }
        else
        {
            showToast(LangSetterForDiscounts.feedbackError);
        }
    }


    public void countComment()
    {
        commentLength.text = feedbackComment.text.Length.ToString() + "/250";
    }


    void showToast(string str)
    {
        ToastHelper.ShowToast(str);    
    }
}
