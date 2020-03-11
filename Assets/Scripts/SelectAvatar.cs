using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using UnityEditor;
using UnityEngine.EventSystems;

public class SelectAvatar : MonoBehaviour {

    public Image avatarpic;
    public GameObject avatarpanel;
    private Texture2D twoDtex;
    private Material tex;
    public GameObject panel;
    //private Texture2D profiletexture;

    public void OnClicked(Button button)
    {
            int width = Screen.width;
            int height = Screen.height;

            avatarpic.sprite = button.gameObject.GetComponent<Image>().sprite;

            Texture2D profiletexture = ConvertSpriteToTexture(avatarpic.sprite);

            Texture2D newTexture = new Texture2D(profiletexture.width, profiletexture.height, TextureFormat.ARGB32, false);

            newTexture.SetPixels(0, 0, profiletexture.width, profiletexture.height, profiletexture.GetPixels());
            newTexture.Apply();


            string path = string.Format("{0}/profile_img1.png", Application.persistentDataPath);
            //for writing to file 
            File.WriteAllBytes(path, newTexture.EncodeToPNG());
            PlayerPrefs.SetString("pathofavatar", path);

            //For reading from file 
            var bytes = File.ReadAllBytes(path);
            twoDtex = new Texture2D(4, 4);
            twoDtex.LoadImage(bytes);
            avatarpic.sprite = Sprite.Create(twoDtex, new Rect(0, 0, twoDtex.width, twoDtex.height), new Vector2(0.5f, 0.5f), 100);
            panel.gameObject.SetActive(true);
            avatarpanel.gameObject.SetActive(false);
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }


    Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        try
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] colors = newText.GetPixels();
                Color[] newColors = sprite.texture.GetPixels((int)System.Math.Ceiling(sprite.textureRect.x),
                                                             (int)System.Math.Ceiling(sprite.textureRect.y),
                                                             (int)System.Math.Ceiling(sprite.textureRect.width),
                                                             (int)System.Math.Ceiling(sprite.textureRect.height));
                //Debug.Log(colors.Length + "_" + newColors.Length);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
        catch
        {
            return sprite.texture;
        }
    }

    public void changepp()
    {
        panel.gameObject.SetActive(false);
        avatarpanel.gameObject.SetActive(true);
    }
    public void Start()
    {
        
        var path= PlayerPrefs.GetString("pathofavatar","");
        if (File.Exists(path))
        {
            //Debug.Log("BAsid");
            //Debug.Log(path);
            var bytes = File.ReadAllBytes(path);
            twoDtex = new Texture2D(4, 4);

            twoDtex.LoadImage(bytes);
            avatarpic.sprite = Sprite.Create(twoDtex, new Rect(0, 0, twoDtex.width, twoDtex.height), new Vector2(0.5f, 0.5f), 100);
        }






        //WWW loader = new WWW("http://unity3d.com/sites/default/files/frontpage/learn.jpg");
        //yield return loader;
        //string path = string.Format("{0}/profile_img.png", Application.persistentDataPath);
        //Debug.Log(path);
        //File.WriteAllBytes(path, loader.texture.EncodeToPNG());



        //var fileName = Path.Combine(Application.persistentDataPath, "profile_img.png");
        //var bytes = File.ReadAllBytes(fileName);
        //twoDtex = new Texture2D(4, 4);

        //twoDtex.LoadImage(bytes);
        //tex.mainTexture = twoDtex;

        //Sprite newpicture = Sprite.Create(twoDtex, new Rect(0, 0, twoDtex.width, twoDtex.height), new Vector2(0.5f, 0.5f), 100);

        ////if (File.Exists(path))
        ////{
        ////    avatarpic.sprite = Sprite.Create(twoDtex, new Rect(0, 0, twoDtex.width, twoDtex.height), new Vector2(0.5f, 0.5f), 100);
        ////}

        //avatarpic.sprite = newpicture;

    }
}
