using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class unRegisteredGame : MonoBehaviour {
    public GameObject loadingPanel;
    public Material[] material;
    
    public void offlineMode()
    {
        
        Texture2D textureOffline = new Texture2D(4, 4, TextureFormat.ETC_RGB4, false);
        for (int i = 0; i < 6; i++)
        {
            textureOffline = (Texture2D)Resources.Load((i + 1).ToString(), typeof(Texture2D));
            material[i].SetTexture("_EmissionMap", textureOffline);
        }
        StartCoroutine(sceneLoader());
    }
    IEnumerator sceneLoader()
    {
        loadingPanel.SetActive(true);
        var asycnV=SceneManager.LoadSceneAsync("Game");
        DontDestroyOnLoad(this.gameObject);
        GameObject gm = this.transform.Find("/GameobjectForSavingİMG").gameObject;
        gm.GetComponent<saveİmages>().offSet(true);
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();
        if (asycnV.isDone)
        {
            loadingPanel.SetActive(false);
        }
    }
}
