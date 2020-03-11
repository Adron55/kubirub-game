using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class saveİmages : MonoBehaviour {
    public Material[] matSave;
    private Texture2D texture1, texture2, texture3, texture4, texture5, texture6;
    string sceneName;
    bool f = false;
    [HideInInspector]
    public bool offline = false;
    // Use this for initialization
    public List<(string company_nme, string webUrl, int id)> company_;
    private static GameObject instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = gameObject;
        
        DontDestroyOnLoad(this.gameObject);
    }
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Start")
        {
            GYU_GET();
            f = false;
        }
        else if (sceneName != "Start" && f == false)
        {
            GYU_SET();
            f = true;
        }

    }
    void GYU_GET() {
        //Debug.Log("|--------------------->>"+matSave[0].GetTexture("_EmissionMap").name);
        texture1 = matSave[0].GetTexture("_EmissionMap") as Texture2D;
        texture2 = matSave[1].GetTexture("_EmissionMap") as Texture2D;
        texture3 = matSave[2].GetTexture("_EmissionMap") as Texture2D;
        texture4 = matSave[3].GetTexture("_EmissionMap") as Texture2D;
        texture5 = matSave[4].GetTexture("_EmissionMap") as Texture2D;
        texture6 = matSave[5].GetTexture("_EmissionMap") as Texture2D;
    }
    void GYU_SET()
    {
        matSave[0].SetTexture("_EmissionMap", texture1);
        matSave[1].SetTexture("_EmissionMap", texture2);
        matSave[2].SetTexture("_EmissionMap", texture3);
        matSave[3].SetTexture("_EmissionMap", texture4);
        matSave[4].SetTexture("_EmissionMap", texture5);
        matSave[5].SetTexture("_EmissionMap", texture6);
        //Debug.Log(sceneName+"................."+texture1);
    }
    public void offSet(bool val)
    {
        offline = val;
    }
    public bool offGet()
    {
        return offline;
    }
    public void createInfoList(List<(string company_nme,string webUrl,int id)> lists)
    {
        company_ = lists;

    }
    public List<(string company_nme, string webUrl, int id)> getInfoList()
    {
        return company_;
    }
}
