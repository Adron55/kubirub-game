using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ToastPlugin;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Logo_Chooser : MonoBehaviour {
    //to me
    public GameObject testForClient;
	public Material[] material;
    private Texture2D t,t1,t2,t3,t4,t5;
	private string[] urllib;
    private bool f = false, f1 = false, f2 = false, f3 = false, f4 = false, f5 = false, foff = true;
	public GameObject asd,gameObjectForSaveImg;
	private float tim=0;
    public Text text;
	public Image lastestImg;
    public int mm;
    private string path1,path2,path3,path4,path5,path6,mainPath;
    byte[] imageBytes2;
    private JsonData jsonCoupon;
    int countOfCycle = 0;
    public List<(string company_nme, string webUrl, int id)> comps;
    void Start () {
            if (PlayerPrefs.GetInt("info") == 0) //that means player first time start to player
                PlayerPrefs.SetInt("info", 1);
            if (PlayerPrefs.GetFloat("sVolume") == 0f)//for Volume init Value
                PlayerPrefs.SetFloat("sVolume", 10);
            if (PlayerPrefs.GetFloat("sCamera") == 0f)//for Camera init Value
                PlayerPrefs.SetFloat("sCamera", 11);
            urllib = new string[6];
            if (HasConnection() == true)
            {
                StartCoroutine(loadAndPut());
            }
            else {
                //testForClient.SetActive(HasConnection());//internet connection pop up u duzeldilmelidi burda unutma
            }
            material[0].SetTexture("_EmissionMap", null);
            material[1].SetTexture("_EmissionMap", null);
            material[2].SetTexture("_EmissionMap", null);
            material[3].SetTexture("_EmissionMap", null);
            material[4].SetTexture("_EmissionMap", null);
            material[5].SetTexture("_EmissionMap", null);
    }
	IEnumerator loadAndPut(){
		//to get datas from server
		string url = "http://kubirub.com/api/initGame";
        WWWForm form = new WWWForm();
		UnityWebRequest www = UnityWebRequest.Post(url,form.ToString());
	    yield return www.SendWebRequest();
        if (www.error == null)
        {
            jsonCoupon = JsonMapper.ToObject(www.downloadHandler.text);
            for (int i = 0; i < 6; i++)
            {
                urllib[i] = "http://kubirub.com/" + jsonCoupon["clients"][i]["path"].ToString();
            }
            if(!f)
                StartCoroutine(pic1(jsonCoupon["clients"][0]["id"].ToString()));
            if(!f1)
                StartCoroutine(pic2(jsonCoupon["clients"][1]["id"].ToString()));
            if(!f2)
                StartCoroutine(pic3(jsonCoupon["clients"][2]["id"].ToString()));
            if(!f3)
                StartCoroutine(pic4(jsonCoupon["clients"][3]["id"].ToString()));
            if(!f4)
                StartCoroutine(pic5(jsonCoupon["clients"][4]["id"].ToString()));
            if(!f5)
                StartCoroutine(pic6(jsonCoupon["clients"][5]["id"].ToString()));
            companyInfoSetter();
        }
        else
        {
            Debug.Log(www.error.ToString());
        }
    }
	IEnumerator pic1(string l1){
        t = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[0]);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            //www.downloadHandler//LoadImageIntoTexture(t);
            t = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t.name = l1;
            name0(l1);
        }
		
	}
	IEnumerator pic2(string l2){
        t1 = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[1]);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            //www.downloadHandler//LoadImageIntoTexture(t);
            t1 = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t1.name = l2;
            name1(l2);
        }
	}
	IEnumerator pic3(string l3){
        //path3 = Path.Combine(mainPath, l3);
        t2 = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[2]);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            t2 = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t2.name = l3;
            name2(l3);
        }
	}
	IEnumerator pic4(string l4){
        t3 = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[3]);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            t3 = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t3.name = l4;
            name3(l4);
        }
	}
	IEnumerator pic5(string l5){

        t4 = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[4]);
        yield return www.SendWebRequest();
        if (www.error==null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            t4 = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t4.name = l5;
            name4(l5);
        }
    }
	IEnumerator pic6(string l6){

        //path6 = Path.Combine(mainPath, l6);
        t5 = new Texture2D(4, 4, TextureFormat.RGB24, false); ;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urllib[5]);
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            while (!www.isDone)
            {
                yield return new WaitForSeconds(0.5f);
            }
            //www.downloadHandler//LoadImageIntoTexture(t);
            t5 = (Texture2D)((DownloadHandlerTexture)www.downloadHandler).texture;
            t5.name = l6;
            name5(l6);
        }
	}
	private void name0(string nameImg)
	{
        //Texture2D tt = new Texture2D (4,4,TextureFormat.RGB24,false);
       // saveImage(path1, t.EncodeToPNG());
        material[0].SetTexture("_EmissionMap",t);
        PlayerPrefs.SetString("1", nameImg);
        f = true;
        //Debug.Log ("Is Done!");
	}
	private void name1(string nameImg)
	{
		//Texture2D tt = new Texture2D (4,4,TextureFormat.ETC_RGB4,false);
        //saveImage(path2, t1.EncodeToPNG());
        material[1].SetTexture("_EmissionMap", t1);
        PlayerPrefs.SetString("2", nameImg);
        f1 = true;
		//Debug.Log ("Is Done!1");
	}
	private void name2(string nameImg)
	{
		//Texture2D tt = new Texture2D (4,4,TextureFormat.ETC_RGB4,false);
       // saveImage(path3, t2.EncodeToPNG());
        material[2].SetTexture("_EmissionMap", t2);
        PlayerPrefs.SetString("3", nameImg);
        f2 = true;
		//Debug.Log ("Is Done!2");
	}
	private void name3(string nameImg)
	{
		//Texture2D tt = new Texture2D (4,4,TextureFormat.ETC_RGB4,false);
      //  saveImage(path4, t3.EncodeToPNG());
        material[3].SetTexture("_EmissionMap", t3);
        PlayerPrefs.SetString("4", nameImg);
        f3 = true;
		//Debug.Log ("Is Done!3");
	}
	private void name4(string nameImg)
	{
		//Texture2D tt = new Texture2D (4,4,TextureFormat.ETC_RGB4,false);
       // saveImage(path5, t4.EncodeToPNG());
        material[4].SetTexture("_EmissionMap", t4);
        PlayerPrefs.SetString("5", nameImg);
        f4 = true;
		//Debug.Log ("Is Done!4");
	}
    private void name5(string nameImg)
	{
		//Texture2D tt5 = new Texture2D (4,4,TextureFormat.ETC_RGB4,false);
       // saveImage(path6, t5.EncodeToPNG());
        material[5].SetTexture("_EmissionMap", t5);
        PlayerPrefs.SetString("6", nameImg);
        f5 = true;
	}
    //from Avtonom Script
    public static bool HasConnection()
    {
        try
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }
            else
                return true;
        }

        catch
        {
            return false;
        }
    }
    void FixedUpdate(){
        
	    tim += Time.deltaTime;
        if (tim >= 6.0)
        {
            if (f == true && f1 == true && f2 == true && f3 == true && f4 == true && f5 == true && HasConnection() == true)
            {

                func_load_scene();
            }
            else if (HasConnection() == false ||  countOfCycle>3)
            {
                if (foff == true)
                {
                    offlineMode();
                }
            }
            else
            {
                countOfCycle++;
                StartCoroutine(loadAndPut());
            }
            tim = 0;
        }

	}
	void func(){
		float tt =Mathf.RoundToInt(((tim - 2) / 6) * 100);
		asd.gameObject.transform.localScale = new Vector3 ((tim-2)/6, 1, 1);
		//lastestImg.flexibleWidth += ((tim - 2) / 6) * 10;
		lastestImg.rectTransform.sizeDelta=new Vector2(((tim - 2) / 6) * 582.6f,9.110001f);
		text.text =tt.ToString()+" %";
	}
	void func_load_scene()
	{
        //Debug.Log(PlayerPrefs.GetInt("Language"));
        saveİmages saveOFFMode = gameObjectForSaveImg.GetComponent<saveİmages>();
        saveOFFMode.offSet(false);
        if (PlayerPrefs.GetString("autologin") == "on")
        {
            string url = "http://www.kubirub.com//api/check_uuid";
            StartCoroutine(checkUUID(url));
        }
        else
        {
            loadSceneASYNC("beforeLoginAndRegister");
        }
    }

    IEnumerator checkUUID(string url)
    {
        string uuid = PlayerPrefs.GetString("uuid");
        WWWForm form = new WWWForm();
        form.AddField("uuid", uuid);
        UnityWebRequest postData = UnityWebRequest.Post(url, form);
        yield return postData.SendWebRequest();
        if (postData.error == null)
        {

            if (postData.isNetworkError || postData.isHttpError)
            {
                loadSceneASYNC("beforeLoginAndRegister");
            }
            else
            {
                JsonObject json = new JsonObject(postData.downloadHandler.text);
                if (json["status"].Equals("success"))
                {
                    loadSceneASYNC("Game");
                }
                else
                {
                    string text = gameObjectForSaveImg.GetComponent<languageForRubicsCube>().language_Discounts.feedbackError;
                    showToast(text);
                }
            }
        }
        else
        {
            loadSceneASYNC("beforeLoginAndRegister");
        }
    }


    public Sprite ConvertToSprite(Texture2D texture){
	
		return Sprite.Create (texture,new Rect(0,0,texture.width,texture.height),Vector2.zero);
	}
    public void DeleteAllFiles()
    {
        int c = 0;
        var info = new DirectoryInfo(mainPath);
        var infofile = info.GetFiles();
        foreach (var cx in infofile)
        {
            File.Delete(cx.FullName);
            c++;
        }
    }
    //'''''''''''
    void saveImage(string path, byte[] imageBytes)
    {
        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        
        try
        {
            File.WriteAllBytes(path, imageBytes);
            //Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
            imageBytes2 = loadImage(path1);
        }
        catch (Exception e)
        {
            //Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    byte[] loadImage(string path)
    {
        byte[] dataByte = null;

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            //Debug.LogWarning("Directory does not exist");
            return null;
        }

        if (!File.Exists(path))
        {
            //Debug.Log("File does not exist");
            return null;
        }

        try
        {
            dataByte = File.ReadAllBytes(path);
            //Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            //Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
            //Debug.LogWarning("Error: " + e.Message);
        }

        return dataByte;
    }
    private void offlineMode()
    {
        //Debug.Log("I am in here");
        Texture2D textureOffline = new Texture2D(4, 4, TextureFormat.RGB24,false);
        for(int i = 0; i < 6; i++)
        {
            textureOffline = (Texture2D)Resources.Load((i+1).ToString(), typeof(Texture2D));
            material[i].SetTexture("_EmissionMap", textureOffline);
        }
        sceneLoader();
        foff = false;
    }
    private void sceneLoader()
    {

        // SceneManager.LoadSceneAsync("rubicsCube");
        loadSceneASYNC("Game");
        //PlayerPrefs.SetInt("off",1);
        modeDefiner(true);
    }
    private void modeDefiner(bool fl)
    {
        saveİmages saveOFFMode = gameObjectForSaveImg.GetComponent<saveİmages>();
        saveOFFMode.offSet(fl);
    }
    private void companyInfoSetter()
    {
        try
        {
            saveİmages saveOFFMode = gameObjectForSaveImg.GetComponent<saveİmages>();
            comps = new List<(string company_nme, string webUrl, int id)>();
            for (int i = 0; i < jsonCoupon["clients"].Count; i++)
            {
                comps.Add((jsonCoupon["clients"][i]["company_name"].ToString(), jsonCoupon["clients"][i]["website"].ToString(), (int)jsonCoupon["clients"][i]["id"]));
            }
            saveOFFMode.createInfoList(comps);

        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
        
    }
    void loadSceneASYNC(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
    void showToast(string text)
    {
        ToastHelper.ShowToast(text);
    }
    /*
     * #if UNITY_EDITOR
        //File.Delete("Assets/Resources/"+t5.name.ToString()+".asset");//work
        AssetDatabase.CreateAsset(t5, "Assets/Resources/" + t5.name.ToString() + ".asset");
        AssetDatabase.SaveAssets();
        //tt5 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/" + t5.name.ToString() + ".asset", typeof(Texture2D));
        //Toast(t5.name);
        #endif
        byte[] imageBytes = loadImage(path6);
        tt5.LoadImage(imageBytes);
        tt5.Apply();
        tt5.name = nameImg;
        material [5].SetTexture ("_EmissionMap",tt5);
		f5 = true;
     */
}
