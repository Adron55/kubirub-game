using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net;
using ToastPlugin;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
//using System.Random;

public class KubikRub : MonoBehaviour
{//Moves..
	public AudioSource winSoundCoupon;
    public Animation animBTN;
    public AnimationClip BTNClip;
    public Text couponText;
    private string juju;
   
    
    
    private string side,side1,winCoupon,company_name,coupon_win_text;
    private string server_axis;
    private string server_version;
    private string offlineModeT;
    private int cc=0;
    //tags
	//public InputField tags;
    
	//for complete face
    private float cx1, cy1, cz1, cx2, cy2, cz2, cx3, cy3, cz3, cx4, cy4, cz4 = 0;
    private string dat1, dat2, dat3;
    public Button scrambleButton;
    public List<Button> buttons;
    public GameObject mainObject, goOnlineBtn;
    public bool fff = false,ffff;
    public bool checks = false;
    public bool chh=true;
    private bool give_cu = false;
    public bool noSmoothing;
    //06.07.20118
    private bool ffft = false,fffft=true;
    //07.07.2018
    private bool ftutorial = false;
    //public Image tutorialHandImage;
    //09.07.2018
    private bool fftutorial = false;
    //10.07.2018
   //1``````````````````````````````````
    //04.02.2018
    public GameObject Complete_panel,offlineModeChanger;
    private string return2,a,b,c,d,e,f,night;
    [SerializeField]
    public TMP_Text movesText;
    public TMP_Text timerText;
    private float secondsCount;
    private int minuteCount=0;
    private int hourCount=0;
    private bool startCounter = true;
    private string hours, minutes,seconds;
    int rota=0;
    int mm;
    public Material[] mats;
    public Texture2D[] textures;
   // public Texture2D maintex;
   // public Texture2D arrowtex;
    List<List<Material>> edgeMats;
    public static float cubesize = 0.96f;
    public float rotTime = 0.5f;
    public int n;
    // public GameObject cam;
    //!Bu deyiseni deyismek lazimdir
    private Material matHighlight;
    private Material matNormal;
    string version;
    Vector3 axis;
    private readonly IEnumerable<Transform> Children;
    private GameObject edge;
    private GameObject cornerEdgeU;
    private GameObject cornerEdgeV;
    private GameObject cornerEdgeW;


    private GameObject rotationContainer;
    private GameObject rotationDummy1;
    private GameObject rotationDummy2;
    private List<Vector3> corners;
    private Texture2D[] cornerTexs;
    private List<Vector3> edges;
    private List<List<GameObject>> superArr;
    private float s = cubesize / 2;
    private bool solved = false;
    private bool scrambled = false;
    //cube construction variables>

    public GameObject cam;

    private int n1;

    public bool isTweening=false;
    public bool tweening=true;

    private int mapMode = 1;
    private string[] mapModeOptions = {"",""};
    float tStart = -1;
    private static float tGameStart=0;
    private bool isPirated = false;

    private static GameObject instance;
    private static GameObject instance1;
    private Password password;

    private Dictionary<string, string> dict;
    private string[] GUIStrings = { "YENILE", "QARISDIR", "cube "};
    // Use this for initialization
    private Graphic volumeSprite;
    private Hashtable postHeader;
    private float scheduler=0;
    private void Awake()
    {
        offlineModeChanger = this.transform.parent.Find("/GameobjectForSavingİMG").gameObject;   
    }
    void Start()
    {
        getOfflineModeMessage();
        if (offlineModeChanger.GetComponent<saveİmages>().offGet() == true)
        {
            deactiveButtons(0);
            deactiveButtons(1);
            deactiveButtons(2);
            Toast(offlineModeT);
        }


        //PlayerPrefs.SetInt("tutorial",1);
        //if (PlayerPrefs.GetInt("tutorial") > 0)
        //{
            cam.GetComponent<Cam1_1>().enabled = true;//bu hisseni deyishdin en son yadinda saxkkkk
        //    //tutorialHandImage.gameObject.SetActive(false);
        //    //tutorialHandImage.transform.parent.gameObject.SetActive(false);
        //}
        //else
        //{
        //    cam.GetComponent<Cam1_1>().enabled = false;
        //}
        string formApiKey = PlayerPrefs.GetString("uuid");
        postHeader = new Hashtable();
        postHeader.Add("User-Agent", formApiKey);
        //Debug.Log(tweening);
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        //float s = cubesize / 2;
        dict = new Dictionary<string,string>();
        n1 = n;
        rotationContainer = new GameObject();
        if (instance1 != null)
        {
            Destroy(rotationContainer);
            return;
        }
        instance1 = rotationContainer;
        DontDestroyOnLoad(rotationContainer);


        rotationContainer.name = "rotationContainer";
        rotationDummy1 = new GameObject();
        DontDestroyOnLoad(rotationDummy1);
        rotationDummy1.name = "rotationDummy1";
		rotationDummy2 = new GameObject();
        DontDestroyOnLoad(rotationDummy2);
        rotationDummy2.name = "rotationDummy2";
        //<GUI text variables
        foreach (string key in GUIStrings)
        {
            dict[key] = key;
        }
        //GUI text variables>
        mapModeOptions = new String[] {"",""};
        mapModeOptions = new String[] {"",""};
        init();
        Scramble();
    }
    void tag_take(){
		//Debug.Log (tags.GetComponentInChildren<Text>().text);
	}
    void init()
    {
        corners = new List<Vector3>();
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                for (var k = 0; k < 2; k++)
                {
                    corners.Add(new Vector3(i * (n - 1), j * (n - 1), k * (n - 1)));
                }
            }
        }


        edges = new List<Vector3>();
        for (var i1 = 1; i1 < n - 1; i1++)
        {
            for (var j1 = 0; j1 < 2; j1++)
            {
                for (var k1 = 0; k1 < 2; k1++)
                {
                    edges.Add(new Vector3(i1, j1 * (n - 1), k1 * (n - 1)));
                    edges.Add(new Vector3(j1 * (n - 1), i1, k1 * (n - 1)));
                    edges.Add(new Vector3(j1 * (n - 1), k1 * (n - 1), i1));
                }
            }
        }
        string[] mnames = { "L", "R", "D", "UP", "TL", "TR", "BR", "BL" };
        edgeMats = new List<List<Material>>();
        for (var i3 = 0; i3 < 6; i3++)
        {
            //Debug.Log(mats[i3]);
            mats[i3].SetTextureScale("_MainTex", new Vector2(1, 1));
        }

        for (var i2 = 0; i2 < 6; i2++)
        {
            var arr4 = new List<Material>();
            for (var j2 = 0; j2 < 8; j2++)
            {
                var mat1 = Instantiate(mats[i2]);
                mat1.shader = Shader.Find("Self-Illumin/Specular");
                mat1.SetTexture("_Illum", textures[j2]);
                mat1.name = mnames[j2];
                //Debug.Log(mat1);
                arr4.Add(mat1);
            }
            for (var j3 = 0; j3 < 4; j3++)
            {//cornerMaterials
                var mat2 = Instantiate(mats[i2]);
                mat2.name = mnames[j3 + 4];
                arr4.Add(mat2);
            }
            edgeMats.Add(arr4);
        }
        sceneSetup();
    }

    void sceneSetup()
    {
        //Kameranin baxdigi 
        //Transform cam y = -2 * n;
        //cam.transform.position = Vector3.zero;
        //cam.transform.rotation.eulerAngles = Vector3.zero;
        //cam.transform.position.z = -n * 2;
        //cam.transform.RotateAround(Vector3.zero, Vector3.up, 45);
        //cam.GetComponent<Cam1_1>().distance = n*2;
        //cam.GetComponent<Cam1_1>().distanceMin = n;
        //cam.GetComponent<Cam1_1>().distanceMax = n*5;
        //cam.GetComponent<Cameraorbit1>().Init();
        //cam.GetComponent<Cam1_1>().enabled = true;
        Supercube();
    }
    void Supercube()
    {
        superArr = new List<List<GameObject>>();//verification Array 
        for (int i1 = 0; i1 < 6; i1++)
        {
            var sideArray = new List<GameObject>();//side controll arrays
            superArr.Add(sideArray);
            //Debug.Log(superArr);
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    GameObject cube;
                    if (i != 0 && j != 0 && k != 0 && i != n - 1 && j != n - 1 && k != n - 1)
                    {
                        cube = new GameObject();
                        //  cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        //cube.transform.localScale =new  Vector3(cubesize,cubesize,cubesize);
                        //cube.GetComponent<Renderer>().material = mats[6];
                    }
                    else
                    {
                        cube = new GameObject();
                        addWalls(cube, i, j, k);
                    }
                    cube.name = "cube" + i + j + k;
                    float c1 = -n * 0.5f;
                    cube.transform.parent = transform;
                    cube.transform.localPosition = new Vector3(-n * 0.5f + i + 0.5f, -n * 0.5f + j + 0.5f, -n * 0.5f + k + 0.5f);
                    
                    if (i == 0) { superArr[0].Add(cube); }
                    if (i == n - 1) { superArr[1].Add(cube); }
                    if (j == 0) { superArr[2].Add(cube); }
                    if (j == n - 1) { superArr[3].Add(cube); }
                    if (k == 0) { superArr[4].Add(cube); }
                    if (k == n - 1) { superArr[5].Add(cube); }
                    
                    //Debug.Log(cube.transform.localPosition);
                }
            }
        }
        map(mapMode);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        scheduler += Time.deltaTime;
        if(startCounter)
            UpdateTimerUI();
        if (scheduler > 3f)
        {
            connectionChecker();
            scheduler = 0;
        }
        rotationContainer.transform.rotation = Quaternion.Slerp(rotationDummy1.transform.rotation, rotationDummy2.transform.rotation, ((Time.time - tStart) / rotTime)*3.5f);
    }

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
    void addWalls(GameObject box, int u, int v, int w)
    {
        //  GameObject edge;
        // GameObject edge;
        //GameObject cornerEdgeU;
        //GameObject cornerEdgeV;
        // GameObject cornerEdgeW;
        //edge = new GameObject();
        ////BUrdaN ASAGI Kom Alindi
        //cornerEdgeU = new GameObject();
        //cornerEdgeV = new GameObject();
        //cornerEdgeW = new GameObject();
        //till here
        if (corners.Contains(new Vector3(u, v, w)))
        {
            // edge = new GameObject();
            cornerEdgeU = new GameObject(u.ToString());
            cornerEdgeV = new GameObject(v.ToString());
            cornerEdgeW = new GameObject(w.ToString());
            cornerEdgeU.transform.parent = box.transform;
            cornerEdgeV.transform.parent = box.transform;
            cornerEdgeW.transform.parent = box.transform;
        }
        if (edges.Contains(new Vector3(u, v, w)))
        {
            edge = new GameObject();
            edge.name = "edge" + u + v + w;
            edge.transform.parent = box.transform;
        }
        if (w == n - 1)
        {//back
            if (new Vector2(u, v) == new Vector2(0, 0) || new Vector2(u, v) == new Vector2(n - 1, 0) || new Vector2(u, v) == new Vector2(0, n - 1) || new Vector2(u, v) == new Vector2(n - 1, n - 1))
            {
                cornerWall(0, new Vector2(u, v), cornerEdgeU, cornerEdgeV);
            }
            else
            {
                if (u == 0 || u == n - 1 || v == 0 || v == n - 1)
                {
                    wall(0, "outer", u, v, edge, true);
                }
                else { wall(0, "outer", u, v, box, false); }
            }
        }
        else { wall(0, "inner", u, v, box, false); }
        if (u == n - 1)
        {//right
            if (new Vector2(w, v) == new Vector2(0, 0) || new Vector2(w, v) == new Vector2(n - 1, 0) || new Vector2(w, v) == new Vector2(0, n - 1) || new Vector2(w, v) == new Vector2(n - 1, n - 1))
            {
                cornerWall(1, new Vector2(w, v), cornerEdgeW, cornerEdgeV);
            }
            else
            {
                if (w == 0 || w == n - 1 || v == 0 || v == n - 1) { wall(1, "outer", n - 1 - w, v, edge, true); } else { wall(1, "outer", n - 1 - w, v, box, false); }
            }
        }
        else { wall(1, "inner", w, v, box, false); }
        if (w == 0)
        {//front
            if (new Vector2(u, v) == new Vector2(0, 0) || new Vector2(u, v) == new Vector2(n - 1, 0) || new Vector2(u, v) == new Vector2(0, n - 1) || new Vector2(u, v) == new Vector2(n - 1, n - 1))
            {
                cornerWall(2, new Vector2(u, v), cornerEdgeU, cornerEdgeV);
            }
            else
            {
                if (u == 0 || u == n - 1 || v == 0 || v == n - 1) { wall(2, "outer", n - 1 - u, v, edge, true); } else { wall(2, "outer", n - 1 - u, v, box, false); }
            }
        }
        else { wall(2, "inner", -u, v, box, false); }
        if (u == 0)
        {//left
            if (new Vector2(w, v) == new Vector2(0, 0) || new Vector2(w, v) == new Vector2(n - 1, 0) || new Vector2(w, v) == new Vector2(0, n - 1) || new Vector2(w, v) == new Vector2(n - 1, n - 1))
            {
                cornerWall(3, new Vector2(w, v), cornerEdgeW, cornerEdgeV);
            }
            else
            {
                if (w == 0 || w == n - 1 || v == 0 || v == n - 1) { wall(3, "outer", w, v, edge, true); } else { wall(3, "outer", w, v, box, false); }
            }
        }
        else { wall(3, "inner", w, v, box, false); }
        if (v == 0)
        {//bottom
            if (new Vector2(w, u) == new Vector2(0, 0) || new Vector2(w, u) == new Vector2(n - 1, 0) || new Vector2(w, u) == new Vector2(0, n - 1) || new Vector2(w, u) == new Vector2(n - 1, n - 1))
            {
                cornerWall(4, new Vector2(u, w), cornerEdgeU, cornerEdgeW);
            }
            else
            {
                if (w == 0 || w == n - 1 || u == 0 || u == n - 1) { wall(4, "outer", u, w, edge, true); } else { wall(4, "outer", u, w, box, false); }
            }
        }
        else { wall(4, "inner", w, u, box, false); }
        if (v == n - 1)
        {//top
            if (new Vector2(w, u) == new Vector2(0, 0) || new Vector2(w, u) == new Vector2(n - 1, 0) || new Vector2(w, u) == new Vector2(0, n - 1) || new Vector2(w, u) == new Vector2(n - 1, n - 1))
            {
                cornerWall(5, new Vector2(u, w), cornerEdgeU, cornerEdgeW);
            }
            else
            {
                if (w == 0 || w == n - 1 || u == 0 || u == n - 1) { wall(5, "outer", u, n - 1 - w, edge, true); } else { wall(5, "outer", u, n - 1 - w, box, false); }
            }
        }
        else { wall(5, "inner", w, u, box, false); }
    }

    void cornerWall(int pos, Vector2 vect, GameObject parentObject1, GameObject parentObject2)
    {
        GameObject[] parentObjects = { parentObject1, parentObject2 };
        var k1 = 1;
        foreach (var po in parentObjects)
        {
            bool reverseX = false;
            bool reverseY = false;
            float a1 = 2 * s * vect.x / (n - 1);
            float b1 = 2 * s * vect.y / (n - 1);
            var tri = new GameObject();
            cornerEdgeU.name= "U";
            cornerEdgeV.name = "V";
            cornerEdgeW.name = "W";


            tri.transform.parent = po.transform;
            Mesh mesh = new Mesh();
            tri.AddComponent<MeshFilter>();
            tri.AddComponent<MeshRenderer>();
            tri.GetComponent<MeshFilter>().mesh = mesh;
            Vector3 temp1 = tri.transform.localPosition; // copy to an auxiliary variable...     
            switch (pos)// sonra bax bura 
            {
                case 0: //back             
                    mesh.vertices = new Vector3[] { k1 * new Vector3(s - a1, s - b1, 0), k1 * new Vector3(s - a1, -s + b1, 0), k1 * new Vector3(-s + a1, -s + b1, 0) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    temp1.z = s;
                    tri.transform.position = temp1;
                    tri.name = mats[0].ToString().Substring(0,5);
                    break;
                case 1://right      
                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, -s + b1, -s + a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, s - b1, s - a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), k1 * new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    temp1.x = s; // modify the component you want in the variable...                    
                    tri.transform.position = temp1;
                    reverseX = true;
                    tri.name = mats[1].ToString().Substring(0, 5);
                    break;
                case 2://front               
                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, -s + b1, 0), k1 * new Vector3(s - a1, -s + b1, 0), k1 * new Vector3(s - a1, s - b1, 0) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    temp1.z = -s;
                    tri.transform.position = temp1;
                    reverseX = true;
                    tri.name = mats[2].ToString().Substring(0, 5);
                    break;
                case 3://left
                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, s - b1, s - a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, -s + b1, -s + a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    temp1.x = -s;
                    tri.transform.position = temp1;
                    tri.name = mats[3].ToString().Substring(0, 5);
                    break;
                case 4://bottom        
                    mesh.vertices = new Vector3[] { k1 * new Vector3(s - a1, 0, s - b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(-s + a1, 0, -s + b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    temp1.y = -s;
                    tri.transform.position = temp1;
                    tri.name = mats[4].ToString().Substring(0, 5);
                    break;
                case 5://top

                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, s - b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(1 - a1 / s, 0), k1 * new Vector2(0, 0), k1 * new Vector2(0, -1 + b1 / s) };
                    //  mesh.uv = new Vector2[] { k1 * new Vector2(a1 / s - 1, 0), k1 *new  Vector2(0, 0), k1 *new  Vector2(0, 1 - b1 / s) };
                    temp1.y = s;
                    tri.transform.position = temp1;
                    reverseY = true;
                    tri.name = mats[5].ToString().Substring(0, 5);
                    break;

            }
            //   mesh.vertices = vertices;
            // hemcinin bunada 
            if (vect == new Vector2(0, 2))
            {
                mm = 5;
                if (reverseX) { mm = 4; }
                if (reverseY) { mm = 6; }
            }
            if (vect == new Vector2(0, 0))
            {
                mm = 6;
                if (reverseX) { mm = 7; }
                if (reverseY) { mm = 5; }
            }
            if (vect == new Vector2(2, 0))
            {
                mm = 7;
                if (reverseX) { mm = 6; }
                if (reverseY) { mm = 4; }
            }
            if (vect == new Vector2(2, 2))
            {
                mm = 4;
                if (reverseX) { mm = 5; }
                if (reverseY) { mm = 7; }
            }

            if (vect.x == vect.y)
            { mesh.triangles = new int[] { 0, 2, 1 }; }
            else { mesh.triangles = new int[] { 0, 1, 2 }; }
            mesh.RecalculateNormals();
            tri.AddComponent<MeshCollider>();
            tri.AddComponent<mouseInteract>();
            tri.GetComponent<mouseInteract>().n = n;
            tri.GetComponent<mouseInteract>().mat = edgeMats[pos][mm + 4];
            tri.GetComponent<Renderer>().material = edgeMats[pos][mm + 4];
            tri.GetComponent<mouseInteract>().matOver = edgeMats[pos][mm];

            k1 *= -1;
        }
    }
    void wall(int pos, string inner, int x1, int y1, GameObject parentObject, bool interactive)
    {
        //Material matHighlight;
        var square = new GameObject();
        Mesh mesh = new Mesh();
        square.transform.parent = parentObject.transform;
        square.AddComponent<MeshFilter>();
        square.AddComponent<MeshRenderer>();
        square.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = new Vector3[] { new Vector3(s, s, 0), new Vector3(s, -s, 0), new Vector3(-s, -s, 0), new Vector3(-s, s, 0) };
        mesh.uv = new Vector2[] { new Vector2(-x1 - 0.5f - s, y1 + 0.5f + s) / n, new Vector2(-x1 - 0.5f - s, y1 + 0.5f - s) / n, new Vector2(-x1 - 0.5f + s, y1 + 0.5f - s) / n, new Vector2(-x1 - 0.5f + s, y1 + 0.5f + s) / n };
        mesh.triangles = new int[] { 0, 2, 1, 0, 3, 2 };
        mesh.RecalculateNormals();
        Vector3 temp = square.transform.position; // copy to an auxiliary variable... 
        temp.z = s;// modify the component you want in the variable...
        square.transform.position = temp;
        switch (pos)
        {
            case 0: //back
                break;
            case 1://right
                square.transform.RotateAround(Vector3.zero, Vector3.up, 90);
                break;
            case 2://front
                square.transform.RotateAround(Vector3.zero, Vector3.up, 180);
                break;
            case 3://left
                square.transform.RotateAround(Vector3.zero, Vector3.up, 270);
                break;
            case 4://bottom
                square.transform.RotateAround(Vector3.zero, Vector3.right, 90);
                break;
            case 5://top
                square.transform.RotateAround(Vector3.zero, Vector3.right, 270);
                break;
        }
        if (inner == "inner") { square.GetComponent<Renderer>().material = mats[6]; }
        else
        {
            square.GetComponent<Renderer>().material = mats[pos];
            square.name = mats[pos].ToString().Substring(0,5);
            switch (x1)
            {
                case 0: //left
                    matHighlight = edgeMats[pos][1];
                    square.name = mats[pos].ToString().Substring(0, 5);
                    break;
                case 2: //right
                    matHighlight = edgeMats[pos][0];
                    square.name = mats[pos].ToString().Substring(0, 5);
                    break;
            }
            switch (y1)
            {
                case 0: //bottom n-1=2 baxarsan
                    matHighlight = edgeMats[pos][2];
                    square.name = mats[pos].ToString().Substring(0, 5);
                    break;
                case 2: //top         bottom n-1 baxarsan
                    matHighlight = edgeMats[pos][3];
                    square.name = mats[pos].ToString().Substring(0, 5);
                    break;
            }
        }

        if (interactive)
        {

            square.AddComponent<MeshCollider>();
            square.AddComponent<mouseInteract>();
            square.GetComponent<mouseInteract>().n = n;
            square.GetComponent<mouseInteract>().mat = mats[pos];
            square.GetComponent<mouseInteract>().matOver = matHighlight;

        }
    }
    
    public void Scramble()
    {
        for (var i = 0; i < 8 * n; i++)
        {
            var rand1 = (int)Mathf.Round(n * UnityEngine.Random.value - 0.5f);
            var rand2 = (int)Mathf.Round(n * UnityEngine.Random.value - 0.5f);
            switch (rand1)
            {
                case 0:
                    version = "x"; axis = Vector3.right;
                    break;
                case 1:
                    version = "x"; axis = -Vector3.right;
                    break;
                case 2:
                    version = "y"; axis = Vector3.up;
                    break;
                case 3:
                    version = "y"; axis = -Vector3.up;
                    break;
                case 4:
                    version = "z"; axis = Vector3.forward;
                    break;
                case 5:
                    version = "z"; axis = -Vector3.forward;
                    break;
            }
            // AnimeRotate(version, axis, rand2, rand2, rand2, true);
            StartCoroutine(selectRotate(version, axis, rand2, rand2, rand2, true));
        }
        scrambled = true;
        
        position_complete(false);
        hourCount = minuteCount = 0;
        secondsCount = 0f;
        startCounter = false;
        UpdateTimerUI();
        StartCoroutine(toGetListFromGameobjects());
    }

    public void AnimeRotate(string version, Vector3 axis, float ixx, float iyy, float izz, bool noSmoothing)
    {
        startCounter = true;
        StartCoroutine(selectRotate(version, axis, ixx, iyy, izz, noSmoothing));
        
        //if (PlayerPrefs.GetInt("tutorial")==0)
        //{
        //    PlayerPrefs.SetInt("tutorial", 1);
        //    //tutorialFunc();
        //    PlayerPrefs.SetInt("tutorial",1);
        //}
        
        //PlayerPrefs.SetInt("tutorial",n);
    }
    public void tutorialFunc()
    {
        //tutorialHandImage.gameObject.SetActive(false);
        cam.GetComponent<Cam1_1>().enabled = true;
    }
    public IEnumerator selectRotate(string version, Vector3 axis, float ixx, float iyy, float izz, bool noSmoothing)
    {

        checks = true;
        var rotationArr = new List<Transform>();
        foreach (Transform box in transform)
        {

            switch (version)
            {
                case "x":
                    if (Mathf.Round(box.transform.position.x + 0.5f * (n - 1)) == ixx)
                    {
                        server_version = "x";
                        server_axis = ((int)axis[0]).ToString();
                        side = ixx.ToString();
                        rotationArr.Add(box);
                    }
                    break;
                case "y":
                    if (Mathf.Round(box.transform.position.y + 0.5f * (n - 1)) == iyy)
                    {
                        server_version = "y";
                        server_axis = ((int)axis[1]).ToString();
                        side = iyy.ToString();
                        rotationArr.Add(box);
                    }
                    break;
                case "z":
                    if (Mathf.Round(box.transform.position.z + 0.5f * (n - 1)) == izz)
                    {
                        server_version = "z";
                        server_axis = ((int)axis[2]).ToString();
                        side = izz.ToString();
                        rotationArr.Add(box);
                    }
                    break;
            }
        }

        var m = rotationArr.Count;
        for (var i = 0; i < m; i++)
        {
            rotationArr[i].transform.parent = rotationContainer.transform;
        }
        rotationDummy2.transform.RotateAround(Vector3.zero, -axis, 90);
        if ((!tweening || noSmoothing))
        {
                buttonOperations(false);
                rotationContainer.transform.RotateAround(Vector3.zero, -axis, 90);
                rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
                rota = 0;
        }
        else
        {
               buttonOperations(false);
               tStart = Time.time;
               rota++;
               yield return new WaitForSeconds(0.4f);
               rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
               isTweening = false;
        }
        
        for (int j = 0; j < m; j++)
        {
            rotationArr[j].transform.parent = transform;
        }
        buttonOperations(true);
        //if (noSmoothing == true) { rota = 0; } else if (noSmoothing == false) { rota++; }
        movesText.text = rota.ToString();
        cc += 1;
        //Moves...
        if (noSmoothing == false)
        {
            // bu_gece(out night);
            verifyface("cube110", "elem2", "U", "cube120", "cube010", "cube210", "cube100", "cube020", "cube220", "cube000", "cube200", out c);//c
            verifyface("cube112", "elem0", "U", "cube102", "cube012", "cube212", "cube122", "cube002", "cube202", "cube022", "cube222", out a);
            ////Startup ve Teching W
            verifyface("cube121", "elem5", "W", "cube021", "cube120", "cube122", "cube221", "cube020", "cube022", "cube220", "cube222", out f);//f
            verifyface("cube101", "elem4", "W", "cube100", "cube001", "cube201", "cube102", "cube000", "cube200", "cube002", "cube202", out e);//d
                                                                                                                                               ////Otv ve GRP V
            verifyface("cube011", "elem3", "V", "cube021", "cube012", "cube010", "cube001", "cube022", "cube020", "cube002", "cube000", out d);//c
            verifyface("cube211", "elem1", "V", "cube221", "cube210", "cube212", "cube201", "cube222", "cube220", "cube202", "cube200", out b);//b
                                                                                                                                               //position_complete(false);



            if (offlineModeChanger.GetComponent<saveİmages>().offGet() != true)
            {
               
                string url1 = "http://kubirub.com/api/check_status"; //kubirub.online
                WWWForm apiform = new WWWForm();
                string formApiKey = PlayerPrefs.GetString("uuid");
                apiform.AddField("uuid", formApiKey);

                apiform.AddField("coordinates", server_version);
                //0 1 2 
                apiform.AddField("side", side);
                //right left bottom top 
                apiform.AddField("axis", server_axis);


                WWW www1 = new WWW(url1, apiform);
                yield return www1;
                try
                {
                    JsonData jsonval = JsonMapper.ToObject(www1.text);
                    JsonData user = jsonval["Cubedata"];
                }catch(Exception ex)
                {
                    Debug.Log(ex);
                }
            }
            // position_complete();




            if ((a == "iki" || b == "iki" || c == "iki" || d == "iki" || e == "iki" || f == "iki") && fff == false)
            {
                if (a == "iki") { company_name = mats[0].GetTexture("_EmissionMap").name; }//elem0
                else if (b == "iki") { company_name = mats[1].GetTexture("_EmissionMap").name; }//elem1
                else if (c == "iki") { company_name = mats[2].GetTexture("_EmissionMap").name; }//elem2
                else if (d == "iki") { company_name = mats[3].GetTexture("_EmissionMap").name; }//elem3
                else if (e == "iki") { company_name = mats[4].GetTexture("_EmissionMap").name; }//elem4
                else if (f == "iki") { company_name = mats[5].GetTexture("_EmissionMap").name; }//elem5
                give_cu = true;
                side1 = "one_side";
                // position_complete(false);
                if (offlineModeChanger.GetComponent<saveİmages>().offGet() == true)
                {
                    StartCoroutine(toGetListFromGameobjects());
                }
                StartCoroutine(showPanel());
                fff = true;
            }
            else if (a == "iki" && b == "iki" && c == "iki" && d == "iki" && e == "iki" && f == "iki")
            {
                StartCoroutine(showPanel());
                side1 = "all_side";
            }
            else
            {
                Complete_panel.SetActive(false);
            }
            checks = false;

        }
    }
    //sekiller
	IEnumerator showPanel(){
		winSoundCoupon.Play ();
		Complete_panel.SetActive (true);

		yield return new WaitForSeconds (5);
		Complete_panel.SetActive (false);

	}
    static double Distance(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        double D;
        x1 = Math.Round(x1, 3);
        y1 = Math.Round(y1, 3);
        z1 = Math.Round(z1, 3);
        x2 = Math.Round(x2, 3);
        y2 = Math.Round(y2, 3);
        z2 = Math.Round(z2, 3);
        D = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) + Math.Pow(z2 - z1, 2));
        //Debug.Log("The distance is ");
        D = Math.Round(D, 3);
        return D;

    }

    void map(int ver)
    {
		
        Texture mtex = null;
        var ctex = new Texture[] { null, null, null, null };
        switch (ver)
        {
            case 0:
                break;
            case 1:
                //mtex = arrowtex;
                //ctex = new Array(arrowtex,arrowtex,arrowtex,arrowtex);
                break;
                /*case 2:
                    mtex = maintex;
                    ctex = new Array(cornerTexs[0],cornerTexs[1],cornerTexs[2],cornerTexs[3]);
                    break;
                    silersen burani
                */
        }
        var children = GetComponentsInChildren<Transform>();
        //arrow olan hisse her xanaya sekil elave etme 
        foreach (Transform child in children)
        {
            if (child.gameObject.GetComponent<Renderer>())
            {
                Material cmat = child.gameObject.GetComponent<Renderer>().material;
                if (cmat.color != mats[6].color) { cmat.SetTexture("_MainTex", mtex); }
                mouseInteract scr1 = child.gameObject.GetComponent<mouseInteract>();
                if (scr1)
                {
                    switch (scr1.matOver.name)
                    {
                        case "BL":
                            scr1.mat.SetTexture("_MainTex", ctex[3]); scr1.matOver.SetTexture("_MainTex", ctex[3]);
                            child.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", ctex[3]);
                            break;
                        case "TL":
                            scr1.mat.SetTexture("_MainTex", ctex[0]); scr1.matOver.SetTexture("_MainTex", ctex[0]);
                            child.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", ctex[0]);
                            break;
                        case "TR":
                            scr1.mat.SetTexture("_MainTex", ctex[1]); scr1.matOver.SetTexture("_MainTex", ctex[1]);
                            child.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", ctex[1]);
                            break;
                        case "BR":
                            scr1.mat.SetTexture("_MainTex", ctex[2]); scr1.matOver.SetTexture("_MainTex", ctex[2]);
                            child.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", ctex[2]);
                            break;
                        default:
                            scr1.mat.SetTexture("_MainTex", mtex); scr1.matOver.SetTexture("_MainTex", mtex);
                            scr1.matOver.SetTextureScale("_Illum", new Vector2(n, n));
                            if (ver == 1)
                            {
                                cmat.SetTextureScale("_MainTex", new Vector2(n, n)); scr1.mat.SetTextureScale("_MainTex", new Vector2(n, n)); scr1.matOver.SetTextureScale("_MainTex", new Vector2(n, n));
                            }
                            else { cmat.SetTextureScale("_MainTex", new Vector2(1, 1)); scr1.mat.SetTextureScale("_MainTex", new Vector2(1, 1)); scr1.matOver.SetTextureScale("_MainTex", new Vector2(1, 1)); }
                            break;

                    }
                }
                else { if (ver == 1) { cmat.SetTextureScale("_MainTex", new Vector2(n, n)); } else { cmat.SetTextureScale("_MainTex", new Vector2(1, 1)); } }
            }
        }

    }


    bool verify()
    {
        for (var i1 = 0; i1 < 6; i1++)
        {
            bool xequal = true;
            bool yequal = true;
            bool zequal = true;
            int x0 = (int)Mathf.Round(2 * superArr[i1][0].transform.position.x);
            int y0 = (int)Mathf.Round(2 * superArr[i1][0].transform.position.y);
            int z0 = (int)Mathf.Round(2 * superArr[i1][0].transform.position.z);
            var rot0 = superArr[i1][0].transform.rotation.eulerAngles.ToString();
            for (var i2 = 1; i2 < n * n; i2++)
            {
                int x1 = (int)Mathf.Round(2 * superArr[i1][i2].transform.position.x);
                int y1 = (int)Mathf.Round(2 * superArr[i1][i2].transform.position.y);
                int z1 = (int)Mathf.Round(2 * superArr[i1][i2].transform.position.z);
                if (x0 != x1) { xequal = false; }
                if (y0 != y1) { yequal = false; }
                if (z0 != z1) { zequal = false; }
                if (!xequal && !yequal && !zequal) { return false; }

                bool onEdge = false;//whether cube is on edge
                if (i2 <= n || i2 >= n * (n - 1) || i2 % n == 0 || (i2 + 1) % n == 0) { onEdge = true; }
                if (mapMode != 0 || onEdge)
                {
                    var rot1 = superArr[i1][i2].transform.rotation.eulerAngles.ToString();
                    if (rot0 != rot1) { return false; }
                    rot0 = rot1;
                }
                if (mapMode == 2 && !onEdge)
                {
                    int dif = 0; //the neighbouring cubes coordinates should not differ more than one
                    if (x0 != x1) { dif = x1 - x0; if (dif != 2 && dif != -2) { return false; } }
                    if (y0 != y1) { dif = y1 - y0; if (dif != 2 && dif != -2) { return false; } }
                    if (z0 != z1) { dif = z1 - z0; if (dif != 2 && dif != -2) { return false; } }
                }
                x0 = x1;
                y0 = y1;
                z0 = z1;
            }
        }
        return true;
    }

    void verifyface(string pivot, string materialname,string cornerenter, string edgeabove, string edgeleft, string edgeright, string edgebottom, string corneraboveleft, string corneraboveright, string cornerbottomleft, string cornerbottomright,out string return2)
    {
        //materialname = transform.Find(pivot).Find(materialname).GetComponent<Renderer>().material.name; case verib duzelt
        double px = transform.Find(pivot).Find(materialname).transform.position.x;
        double py = transform.Find(pivot).Find(materialname).transform.position.y;
        double pz = transform.Find(pivot).Find(materialname).transform.position.z;
        return2 = "bir";

        //Edges 
        var ex = transform.Find(edgeabove).Find("edge" + edgeabove.Substring(4, 3)).Find(materialname).transform.position.x;
        var ey = transform.Find(edgeabove).Find("edge" + edgeabove.Substring(4, 3)).Find(materialname).transform.position.y;
        var ez = transform.Find(edgeabove).Find("edge" + edgeabove.Substring(4, 3)).Find(materialname).transform.position.z;
        //
        var ex1 = transform.Find(edgeleft).Find("edge" + edgeleft.Substring(4, 3)).Find(materialname).transform.position.x;
        var ey1 = transform.Find(edgeleft).Find("edge" + edgeleft.Substring(4, 3)).Find(materialname).transform.position.y;
        var ez1 = transform.Find(edgeleft).Find("edge" + edgeleft.Substring(4, 3)).Find(materialname).transform.position.z;
        //
        var ex2 = transform.Find(edgeright).Find("edge" + edgeright.Substring(4, 3)).Find(materialname).transform.position.x;
        var ey2 = transform.Find(edgeright).Find("edge" + edgeright.Substring(4, 3)).Find(materialname).transform.position.y;
        var ez2 = transform.Find(edgeright).Find("edge" + edgeright.Substring(4, 3)).Find(materialname).transform.position.z;
        //
        var ex3 = transform.Find(edgebottom).Find("edge" + edgebottom.Substring(4, 3)).Find(materialname).transform.position.x;
        var ey3 = transform.Find(edgebottom).Find("edge" + edgebottom.Substring(4, 3)).Find(materialname).transform.position.y;
        var ez3 = transform.Find(edgebottom).Find("edge" + edgebottom.Substring(4, 3)).Find(materialname).transform.position.z;
        //edges end 

        
        //corner 
        switch (cornerenter)
        {
            case "U":
                //Console.WriteLine("Case 1");
                cx1 = transform.Find(corneraboveleft).Find("U").Find(materialname).transform.position.x;
                cy1 = transform.Find(corneraboveleft).Find("U").Find(materialname).transform.position.y;
                cz1 = transform.Find(corneraboveleft).Find("U").Find(materialname).transform.position.z;
                //
                cx2 = transform.Find(corneraboveright).Find("U").Find(materialname).transform.position.x;
                cy2 = transform.Find(corneraboveright).Find("U").Find(materialname).transform.position.y;
                cz2 = transform.Find(corneraboveright).Find("U").Find(materialname).transform.position.z;

                //
                cx3 = transform.Find(cornerbottomleft).Find("U").Find(materialname).transform.position.x;
                cy3 = transform.Find(cornerbottomleft).Find("U").Find(materialname).transform.position.y;
                cz3 = transform.Find(cornerbottomleft).Find("U").Find(materialname).transform.position.z;

                //
                cx4 = transform.Find(cornerbottomright).Find("U").Find(materialname).transform.position.x;
                cy4 = transform.Find(cornerbottomright).Find("U").Find(materialname).transform.position.y;
                cz4 = transform.Find(cornerbottomright).Find("U").Find(materialname).transform.position.z;
                break;
            case "V":
                //Console.WriteLine("Case 2");
                cx1 = transform.Find(corneraboveleft).Find("V").Find(materialname).transform.position.x;
                cy1 = transform.Find(corneraboveleft).Find("V").Find(materialname).transform.position.y;
                cz1 = transform.Find(corneraboveleft).Find("V").Find(materialname).transform.position.z;
                //
                cx2 = transform.Find(corneraboveright).Find("V").Find(materialname).transform.position.x;
                cy2 = transform.Find(corneraboveright).Find("V").Find(materialname).transform.position.y;
                cz2 = transform.Find(corneraboveright).Find("V").Find(materialname).transform.position.z;

                //
                cx3 = transform.Find(cornerbottomleft).Find("V").Find(materialname).transform.position.x;
                cy3 = transform.Find(cornerbottomleft).Find("V").Find(materialname).transform.position.y;
                cz3 = transform.Find(cornerbottomleft).Find("V").Find(materialname).transform.position.z;

                //
                cx4 = transform.Find(cornerbottomright).Find("V").Find(materialname).transform.position.x;
                cy4 = transform.Find(cornerbottomright).Find("V").Find(materialname).transform.position.y;
                cz4 = transform.Find(cornerbottomright).Find("V").Find(materialname).transform.position.z;
                break;
            case "W":
                //Console.WriteLine("Case 3");
                cx1 = transform.Find(corneraboveleft).Find("W").Find(materialname).transform.position.x;
                cy1 = transform.Find(corneraboveleft).Find("W").Find(materialname).transform.position.y;
                cz1 = transform.Find(corneraboveleft).Find("W").Find(materialname).transform.position.z;
                //
                 cx2 = transform.Find(corneraboveright).Find("W").Find(materialname).transform.position.x;
                 cy2 = transform.Find(corneraboveright).Find("W").Find(materialname).transform.position.y;
                 cz2 = transform.Find(corneraboveright).Find("W").Find(materialname).transform.position.z;

                //
                 cx3 = transform.Find(cornerbottomleft).Find("W").Find(materialname).transform.position.x;
                 cy3 = transform.Find(cornerbottomleft).Find("W").Find(materialname).transform.position.y;
                 cz3 = transform.Find(cornerbottomleft).Find("W").Find(materialname).transform.position.z;

                //
                 cx4 = transform.Find(cornerbottomright).Find("W").Find(materialname).transform.position.x;
                 cy4 = transform.Find(cornerbottomright).Find("W").Find(materialname).transform.position.y;
                 cz4 = transform.Find(cornerbottomright).Find("W").Find(materialname).transform.position.z;
                break;
        }

        var de1 = Distance(px, py, pz, ex, ey, ez);
        var de2 = Distance(px, py, pz, ex1, ey1, ez1);
        var de3 = Distance(px, py, pz, ex2, ey2, ez2);
        var de4 = Distance(px, py, pz, ex3, ey3, ez3);
        
        var dc1 = Distance(px, py, pz, cx1, cy1, cz1);
        var dc2 = Distance(px, py, pz, cx2, cy2, cz2);
        var dc3 = Distance(px, py, pz, cx3, cy3, cz3);
        var dc4 = Distance(px, py, pz, cx4, cy4, cz4);

        if ((pivot=="cube110") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            //print("Adnsu tamdir!");
            return2 = "iki";
            
        }
         if ((pivot == "cube011") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            return2 = "iki";
        }
         if ((pivot == "cube101") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            return2 = "iki";
        }
        if ((pivot == "cube112") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            return2 = "iki";
        }
         if ((pivot == "cube211") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            return2 = "iki";
        }
       if ((pivot == "cube121") && (de1 == 1 && de2 == 1 && de3 == 1 && de4 == 1 && dc1 == 1.414 && dc2 == 1.414 && dc3 == 1.414 && dc4 == 1.414))
        {
            return2 = "iki";
        }
        

    }

    void position_complete(bool ffff)
    {
        foreach (Transform bo in transform)
        {
            //print(bo.name);
            if (bo.name != "Canvas" && bo.name != "timerObject")
            {
                if (Mathf.Round(bo.transform.position.x) != -1 && Mathf.Round(bo.transform.position.x) != 1)
                {
                    bo.position = new Vector3(0, Mathf.Round(bo.transform.position.y), Mathf.Round(bo.transform.position.z));
                    
                }
                if (Mathf.Round(bo.transform.position.y) != -1 && Mathf.Round(bo.transform.position.y) != 1)
                {
                    bo.position = new Vector3(Mathf.Round(bo.transform.position.x), 0, Mathf.Round(bo.transform.position.z));
                    
                }
                if (Mathf.Round(bo.transform.position.z) != -1 && Mathf.Round(bo.transform.position.z) != 1)
                {
                    bo.position = new Vector3(Mathf.Round(bo.transform.position.x), Mathf.Round(bo.transform.position.y), 0);
                    

                }
                //print(Mathf.Round(bo.transform.position.x));
                //print(Mathf.Round(bo.transform.position.y));
                //print(Mathf.Round(bo.transform.position.z));
                bo.position = new Vector3(Mathf.Round(bo.transform.position.x), Mathf.Round(bo.transform.position.y), Mathf.Round(bo.transform.position.z));
                
            }
          //  bo.gameObject.SetActive(false);
          //  bo.gameObject.SetActive(true);//asdfghjk
        }
       
    }
    void bu_gece(out string return2) {
        return2 = "";
        dat1 = "";
        List<string> array = new List<string>();
        List<string> array1 = new List<string>();
        List<List<string>> array2 = new List<List<string>>();
        List<string> array3 = new List<string>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    foreach (Transform fe in transform)
                    {
                        if (fe.name != "Canvas" && fe.name != "timerObject" && fe.name != "rotationContainer" && fe.name != "rotationDummy1" && fe.name != "rotationDummy2")
                        {
                            if (fe.position == new Vector3(i, j, k))
                            {
                                dat1 = dat1 + fe.name.Substring(4, 3);
                                break;
                                    
                            }

                        }
                    }
                }

            }
        }
        return2 = dat1;

    }
 
    IEnumerator toGetListFromGameobjects()
    {
        dat1 = "";

        List<string> array = new List<string>();
        List<string> array1 = new List<string>();
        List<List<string>> array2 = new List<List<string>>();
        List<string> array3 = new List<string>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {

                for (int k = -1; k < 2; k++)
                {
                    foreach (Transform fe in transform)
                    {
                        if (fe.name != "Canvas"  && fe.name != "rotationContainer" && fe.name != "rotationDummy1" && fe.name != "rotationDummy2")
                        {
                            
                            if (fe.position == new Vector3(i, j, k))
                            {
                                dat1 = dat1  + fe.name.Substring(4, 3);
                                break;
                            }

                        }
                    }
                }

            }

        }
        juju = dat1;
        string url2;

        // Servere melumat gondermek ucun olan script
        if (give_cu == false)
        {
             url2 = "http://kubirub.com/api/cubirub"; //kubirub.online
        }
        else {
             url2 = "http://kubirub.com/api/give_check";
        }
        WWWForm apiform1 = new WWWForm();
        string formApiKey = PlayerPrefs.GetString("uuid");
        apiform1.AddField("uuid", formApiKey);
        apiform1.AddField("datas", juju);
        if (give_cu == true)
        {
            apiform1.AddField("side", side1);
			apiform1.AddField("id",company_name);

        }

        //  WWW www2 = new WWW(url2, postData: apiform1.data, headers: postHeader);
        WWW www2 = new WWW(url2,apiform1);
        
        yield return www2; //cavab gelir serverden
        JsonData jsonCoupon = JsonMapper.ToObject(www2.text);
        try
        {

            String sts = jsonCoupon["status"].ToString();
            winCoupon = jsonCoupon["content"].ToString();
            winCoupon = jsonCoupon["content"].ToString();
            coupon_win_text = winCoupon;
            couponText.text = "Your Discount: " + coupon_win_text;
        }
        // handle the error
        catch (System.Exception err)
        {
            //Debug.Log("Got: " + err);
        }
    }
    void Toast(string content)
    {
       ToastHelper.ShowToast(content, true);
    }
    void buttonOperations(bool f)
    {
        scrambleButton.enabled = f;
        foreach(Button btn in buttons)
        {
            btn.interactable = f;
        }
        
    }
    void deactiveButtons(int i)
    {
        Button btn = buttons[i];
        btn.GetComponent<Button>().enabled = false;
        btn.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color32(81,122,180,255);
    }
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        if (hourCount.ToString().Length<2)
            hours = "0"+hourCount;
        else
            hours = hourCount.ToString();
        if (minuteCount.ToString().Length<2)
            minutes = "0"+minuteCount;
        else
            minutes = minuteCount.ToString();
        if (((int)secondsCount).ToString().Length < 2)
            seconds = "0" + (int)secondsCount;
        else
            seconds = ((int)secondsCount).ToString();
        
        timerText.text = hours + ":" + minutes+":" + seconds;
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }
    private void connectionChecker()
    {
        if (!HasConnection())
        {
            if (ffft == false)
            {
                getOfflineModeMessage();
                Toast(offlineModeT);
                if (!fffft)
                {
                    showGoOnlineButton(fffft);
                }
            }
            ffft = true;
            fffft = true;
            offlineModeChanger.GetComponent<saveİmages>().offSet(true);
        }
        else if(ffft && HasConnection() && fffft)
        {
            showGoOnlineButton(fffft);
            fffft = false;
            ffft = false;
            offlineModeChanger.GetComponent<saveİmages>().offSet(false);
        }
    }
    void showGoOnlineButton(bool blean)
    {
        
        if (blean)
        {
            animBTN.clip = BTNClip;
            animBTN["ToOnlineGame"].speed = 1;
            animBTN.Play("ToOnlineGame");
        }
        else
        {

            animBTN["ToOnlineGame"].speed = -1;
            animBTN["ToOnlineGame"].time = animBTN["ToOnlineGame"].length;
            animBTN.Play("ToOnlineGame");
        }
    }
    private void getOfflineModeMessage()
    {
        string[] names = offlineModeChanger.GetComponent<languageForRubicsCube>().getOffModeTxt();
        offlineModeT = names[0];
        animBTN.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = names[1];

    }
    public Material[] GetMaterials()
    {
        return mats;
    }
}

//classin sonu

