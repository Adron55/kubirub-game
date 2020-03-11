using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    private List<string> urls;
    public Text completePanel;
    public GameObject infoContent,compInfoBtns,camera;
    public TextMeshProUGUI volumeText, cameraText, soundsText,helpText,langBtnText,title;
    public string strUrl;
    public AudioSource clickForAllButtons,backgroundSound;
    public Transform mainMenu, optionsMenu;
    public Transform Cube, saver2;
    public KubikRub cubeScript1;
	public Image LoadCircBar;
    private static GameObject instance;
	private AsyncOperation async;
    
	private int level = 4, level2 = 3, level3 = 1, level4, level5;
	public int music, twen;
    public Toggle check;
    public Slider volumeSlider,camerSlider;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
       instance = gameObject;
    }

   void Start()
   {
        urls = new List<string>();
        cubeScript1 = Cube.GetComponent<KubikRub>();
        infoPanelMake();
        Load();
   }

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
    public void support()
    {
        Application.OpenURL(strUrl);
    }

   // public void BasketMenu(bool touch)
   // {
   //     if (touch == true)
   //     {
			
			//LoadCircBar.gameObject.SetActive(true);
			//StartCoroutine(loadLevelAsync(level));

   //     }
   //     else
   //     {
			//StartCoroutine(loadLevelAsync(level3));
   //     }
        
   // }
   // public void UserMEnu(bool shit)
   // {
   //     if (shit == true)
   //     {
			
			//LoadCircBar.gameObject.SetActive(true);
			//StartCoroutine(loadLevelAsync(level2));
   //    }     

   // }
    
    public void openScenes(string name)
    {
        LoadCircBar.gameObject.SetActive(true);
        StartCoroutine(loadLevelAsync(name));
    }
	IEnumerator loadLevelAsync(string name)
    {
        async = SceneManager.LoadSceneAsync(name);
        while (!async.isDone)
        {
            yield return null;
        }
        if (async.isDone)
        {
               LoadCircBar.gameObject.SetActive(false);
        }
    }

	public void Load(int ind=-1)
    {
        getVolumeValueToPrefs();
        getCameraValueToPrefs();
        setCameraDistance();
        setBackGrndSound();  
    }
    public void setBackGrndSound()
    {
        backgroundSound.volume = (volumeSlider.value - 5) / 10;
    }
    public void setCameraDistance()
    {
        camera.transform.position = new Vector3(0, 0 ,Cube.transform.position.z - camerSlider.value);
        camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        camera.GetComponent<Cam1_1>().distance = camerSlider.value;
       // camera.GetComponent<Cam1_1>().direction = 1;
    }
    public void Save_language(int d=0)
    {
        switch (d)
        {
            case 0:
               PlayerPrefs.SetInt("Language", 0);
               break;
            case 1:
               PlayerPrefs.SetInt("Language", 1);
                break;
            case 2:
               PlayerPrefs.SetInt("Language", 2);
                break;
            case 3:
               PlayerPrefs.SetInt("Language",3);
                break;
            default:
                PlayerPrefs.SetInt("Language", 0);
                break;
        }

        GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().defineTexts();
        GetComponent<LangSetterForGame>().setLang();
    }

    public void infoPanelMake()
    {

        try
        {
            GameObject notDestroyedGm = this.transform.parent.Find("/GameobjectForSavingİMG").gameObject;
            saveİmages company_Lists = notDestroyedGm.GetComponent<saveİmages>();
            
            if (!company_Lists.offGet())//works only when connected to net.
            {
                List<(string company_nme, string webUrl, int id)> compLists = company_Lists.getInfoList();
                Material[] materials = cubeScript1.GetMaterials();
                for (int i = 0; i < compLists.Count; i++)
                {
                    GameObject gm = Instantiate(compInfoBtns, infoContent.transform);
                    gm.transform.Find("Text").GetComponent<Text>().text = compLists[i].company_nme;
                    Texture2D texture = materials[i].GetTexture("_EmissionMap") as Texture2D;
                    if (texture.name.Equals(compLists[i].id.ToString()))
                    {
                        
                        gm.transform.Find("Logo").GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    }
                    urls.Add(compLists[i].webUrl);
                    if (!urls[i].Contains("http://") || !urls[i].Contains("https://"))
                        urls[i] = "http://" + urls[i];
                    gm.GetComponent<Button>().onClick.AddListener(() => { openUrls(urls[gm.transform.GetSiblingIndex()]); });
                }
            }

        }
            catch(Exception ex)
            {
                Debug.Log(ex.ToString());
            }

    }
    void openUrls(string url)
    {
        Application.OpenURL(url);
    }
    

    public void setVolumeValueToPrefs()
    {
        PlayerPrefs.SetFloat("sVolume", volumeSlider.value);
    }
    public void setCameraValueToPrefs()
    {
        PlayerPrefs.SetFloat("sCamera", camerSlider.value);
    }
    private void getVolumeValueToPrefs()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sVolume");
    }
    private void getCameraValueToPrefs()
    {
        camerSlider.value=PlayerPrefs.GetFloat("sCamera");
    }
    [Serializable]
	class playerData
    {
		public int music, twen;

	}
}
