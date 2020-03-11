using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TextureLoader : MonoBehaviour {
    public Material[] materials;
    private Texture2D text;
    string mainPath;
    // Use this for initializatio
    private void Awake()
    {
        mainPath = Application.persistentDataPath + "/save/";
        string path;
        for (int i = 0; i <7; i++)
        {
            path = PlayerPrefs.GetString((i + 1).ToString());
            load1(mainPath+path,i, path); 
        }      
    }
    public void load1(string path,int i,string name)
    {
        
        byte[] bytes = File.ReadAllBytes(path);
        text = new Texture2D(4, 4, TextureFormat.ETC_RGB4, false);
        text.LoadImage(bytes);
        text.name = name;
        materials[i].SetTexture("_EmissionMap", text);
        //Debug.Log("+++++++++++++++++++++"+path);
        //Debug.Log(materials[i].GetTexture("_EmissionMap").name);
        
    }
}
