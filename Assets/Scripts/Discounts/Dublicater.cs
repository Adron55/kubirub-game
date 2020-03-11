using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitJson;
using TMPro;
using ToastPlugin;

public class Dublicater : MonoBehaviour
{
	//public AudioSource soundForDelete,soundForAccept1,soundForAccept2,soundClick;
    public GameObject coupon, couponParent;
    public GameObject discountPopUP, loadingPanel;
    public ScrollRect discounts;
    bool loaded = true, scrolled = false, recursive = false;
    Vector2 last_pos;
    //,loadSceneNotif;

	//public GameObject textList,notificationPanel,acceptCoupon,usePanel;
    Text titleText, bodyText;
    private string url,couponDes,winCoupon,sts,coupid,coupName,sssik;
	//private float x, y, z;
	private JsonData myObject,jsonArray;
    private int count,currentPg=1, lastPg = 1, goalPg = 0;



   

    //public AudioSource audi;

    // Use this for initialization
    void Start() {

        StartCoroutine(getCoupon(currentPg));
        currentPg++;

        if (PlayerPrefs.GetInt("info", 1) == 1)
        {
            GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
        }


       // discounts.onValueChanged.AddListener(delegate { loadMore(); });
	}


    private void Update()
    {
        if (!loaded)
        {
            if (discounts.verticalNormalizedPosition < 0.000000000000000000001)
            {
                loaded = true;
                if (lastPg >= currentPg)
                {
                    //Debug.Log("i am callled");
                    StartCoroutine(getCoupon(currentPg));
                    currentPg++;
                }
            }
        }
        
    }


   


    IEnumerator getCoupon(int currentPgg)
	{
        loadingPanel.SetActive(true);
		url = "http://kubirub.com/api/getCoupons?page="+currentPgg.ToString();

		WWWForm apiformCoupon = new WWWForm ();
		string formApiKey = PlayerPrefs.GetString ("uuid");
		//Debug.Log (formApiKey);
		apiformCoupon.AddField ("uuid", formApiKey);

		//  WWW www2 = new WWW(url2, postData: apiform1.data, headers: postHeader);
		WWW www2 = new WWW (url, apiformCoupon);
       
		yield return www2; //cavab gelir serverden
         //Debug.Log(www2.text);
		myObject = JsonMapper.ToObject (www2.text);
        try
        {
            if(!myObject.Keys.Contains("coupons") && myObject.Keys.Contains("status") && KubikRub.HasConnection())
            {
                loadingPanel.SetActive(false);
            }
            JsonData jsonData = JsonMapper.ToObject(myObject["coupons"].ToJson());
            //currentPg = (int)jsonData["current_page"];
            lastPg = (int)jsonData["last_page"];
            //currentPg++;

            //Debug.Log(currentPg);
            jsonArray = JsonMapper.ToObject(jsonData["data"].ToJson());
            GameObject coupBut;
            count = jsonArray.Count;

            for (int i = 0; i < count; i++)
            {
                couponDes = "";

                coupBut = Instantiate(coupon, couponParent.transform);

                //coupBut.transform.parent = textList.transform;

                try
                {
                    coupBut.gameObject.name = jsonArray[i]["id"].ToString();
                    //couponDes = jsonArray[i]["id"].ToString();
                    couponDes = jsonArray[i]["name"].ToString() + " " + jsonArray[i]["product"].ToString() + " " + jsonArray[i]["discount"].ToString() + " %";
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
                coupBut.gameObject.transform.Find("Text").GetComponent<Text>().text = couponDes;
                coupBut.GetComponent<Button>().onClick.AddListener(discountPopUp);

            }
            loaded = false;


            if (recursive)
            {
                currentPg++;
                if (currentPg < goalPg)
                {
                    StartCoroutine(getCoupon(currentPg));
                }
                else
                {
                    recursive = false;
                    discounts.normalizedPosition = last_pos;
                }
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
        yield return new WaitForEndOfFrame();
        if (KubikRub.HasConnection())
        {
            loadingPanel.SetActive(false);
        }
    }


	public void Del(){
        loaded = true;
        goalPg = currentPg;
        last_pos =  discounts.normalizedPosition;

		string urlDel="http://kubirub.com/api/deleteCoupon";
		WWWForm apiformCouponDel = new WWWForm ();
		string formApiKeyDel = PlayerPrefs.GetString ("uuid");
		string formApiKeyDelid = coupid;
		apiformCouponDel.AddField ("uuid",formApiKeyDel);
		apiformCouponDel.AddField ("coupon_id",formApiKeyDelid);
		WWW www= new WWW(urlDel,apiformCouponDel);
		GameObject go = GameObject.Find (coupid);
		//soundForDelete.Play ();
		Destroy (go.gameObject);

        
        int childCountt = couponParent.transform.childCount;
        for (int i = 0; i < childCountt; i++)
        {
            Destroy(couponParent.transform.GetChild(i).gameObject);
        }

        showToast(LangSetterForDiscounts.deletedToastText);

        currentPg = 1;
        lastPg = 1;
        recursive = true;
        StartCoroutine(getCoupon(currentPg));


        
        		
		//Debug.Log ("Delete");
	}

	

	//public void getupAccept(){
	//	string compName = EventSystem.current.currentSelectedGameObject.gameObject.transform.parent.name;
	//	sssik = compName;
	//	for (int i = 0; i < count; i++) {
	//		if(Equals((myObject[i]["id"]).ToString(),compName.ToString())){
	//			titleText.text=companyTitle ((myObject[i]["name"]).ToString());
	//			bodyText.text="Code : "+(myObject[i]["id"]).ToString() +"\n"+(myObject[i]["product"]).ToString()+"\n" + (myObject[i]["discount"]).ToString()+" %" + "\n" + (myObject [i] ["created_at"]).ToString ();
	//			//bodyText.GetComponent<Text>().text=(myObject[i]["description"]).ToString()+"/n";
	//			//bodyText.GetComponent<Text> ().text = (myObject [i] ["created_at"]).ToString ();
	//			break;
	//		}
	//	}
	//	//acceptCoupon.SetActive (true);
	//}

	IEnumerator acceptCoupons(string couponId){
		string urlAcc = "http://kubirub.com/api/requestCoupon";
		WWWForm apiformCouponAcc = new WWWForm ();
		string formApiKeyAcc = PlayerPrefs.GetString ("uuid");
		string formApiKeyAccid = couponId;
		apiformCouponAcc.AddField ("uuid",formApiKeyAcc);
		apiformCouponAcc.AddField ("coupon_id",formApiKeyAccid);

		WWW www= new WWW(urlAcc,apiformCouponAcc);
		//GameObject go = GameObject.Find (coupid);
		//Destroy (go.gameObject);
		yield return www;
	//	Debug.Log(www.text);
		GameObject destr = new GameObject ();
		string destr2;
		destr = GameObject.Find (sssik);
		//Destroy (destr);		
			Destroy (destr);

	}
	public  void use_it(){
		StartCoroutine(acceptCoupons(sssik));
    }
	
	private string companyTitle(string title1){

		foreach (char f in title1) {
			coupName += (f.ToString().ToUpper());
		}

		return coupName;
	}
    

    public void discountPopUp()
    {
        GameObject chosenGO = EventSystem.current.currentSelectedGameObject;
        coupid = chosenGO.name;

        int chosen = 0;
        for (int i = 0; i < count; i++)
        {
            try
            {
                if (jsonArray[i]["id"].ToString() == coupid)
                {
                    chosen = i;
                    break;
                }
            }
            catch(Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        try
        {
            //Debug.Log(myObject.ToJson());
            discountPopUP.transform.GetChild(0).transform.Find("Text (TMP)_Company").GetComponent<TMP_Text>().text = jsonArray[chosen]["name"].ToString();
            discountPopUP.transform.GetChild(0).transform.Find("Text (TMP)_Code").GetComponent<TMP_Text>().text = "Code: " + jsonArray[chosen]["id"].ToString();
            discountPopUP.transform.GetChild(0).transform.Find("Text (TMP)_Discount").GetComponent<TMP_Text>().text = companyTitle(jsonArray[chosen]["product"].ToString()) + " - " + jsonArray[chosen]["discount"].ToString();
        }
        catch(Exception e)
        {
            Debug.LogException(e, this);
        }

        discountPopUP.SetActive(true);
    }


    void showToast(string str)
    {
        ToastHelper.ShowToast(str);
    }

}

