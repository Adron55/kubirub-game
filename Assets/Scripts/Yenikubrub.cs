using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Yenikubrub : MonoBehaviour
{//timerin cagirilmasi...
 // public Timer _Timer;
    public Text timerText;
    public static float startTime = 0;
    private static bool f1 = false;
    private float t = 0;

    public Material[] mats;
    public Texture2D[] textures;
    Texture2D[] anar;
    List<List<Material>> edgeMats;
    public static float cubesize = 0.96f;
    public float rotTime = 0.5f;
    public GameObject basePlane;
    public int n;
    // public GameObject cam;
    //!Bu deyiseni deyismek lazimdir
    Material matHighlight;
    string version;
    Vector3 axis;
    private readonly IEnumerable<Transform> Children;
    private GameObject edge;
    private GameObject cornerEdgeU;
    private GameObject cornerEdgeV;
    private GameObject cornerEdgeW;
    int mm;

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
    public bool rere = false;

    public GameObject cam;

    public GUISkin skin1;
    private int n1;

    public bool isTweening = false;
    public bool tweening = false;

    private int mapMode = 1;
    private string[] mapModeOptions = { " ", " " };
    float tStart = -1;
    private static float tGameStart;
    private bool isPirated = false;

    private Dictionary<string, string> dict;
    private string[] GUIStrings = { "YENÝL?", "QARIÞDIR", "cube " };
    //private object vertices;

    // Use this for initialization
    internal void Start()
    {

        float s = cubesize / 2;
        dict = new Dictionary<string, string>();
        n1 = n;

        rotationContainer = new GameObject();
        rotationContainer.name = "rotationContainer";
        rotationDummy1 = new GameObject();
        rotationDummy1.name = "rotationDummy1";
        rotationDummy2 = new GameObject();
        rotationDummy2.name = "rotationDummy2";


        //<GUI text variables
        foreach (string key in GUIStrings)
        {
            dict[key] = key;
        }

        //GUI text variables>
        mapModeOptions = new String[] { " ", " " };

        //yield return new parseData();
        mapModeOptions = new String[] { " ", " " };
        init();

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
        //  Transform cam y = -2 * n;
        //  cam.transform.position = Vector3.zero;
        //   cam.transform.rotation.eulerAngles = Vector3.zero;
        //    cam.transform.position.z = -n * 2;
        //    cam.transform.RotateAround(Vector3.zero, Vector3.up, 45);
        /*cam.GetComponent<Cameraorbit>().distance = n*2;
        cam.GetComponent<Cameraorbit>().distanceMin = n;
        cam.GetComponent<Cameraorbit>().distanceMax = n*5;
        cam.GetComponent<Cameraorbit>().Init();*/
        //cam.GetComponent<Cameraorbit>().enabled = true;
        Supercube();
    }
    void Supercube()
    {
        superArr = new List<List<GameObject>>();//verification Array 
        for (int i1 = 0; i1 < 6; i1++)
        {
            var sideArray = new List<GameObject>();//side controll arrays
            superArr.Add(sideArray);
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
                }
            }
        }
        map(mapMode);
    }

    // Update is called once per frame
    void Update()
    {//silme laizm ola bile
        //   if (scrambled == true)
        //  {
        //      _Timer.hello();
        //  }
        //timerin zir zibillleri
        startTime = Time.deltaTime;
        if (f1 == true)
        {
            t = Time.time - startTime;
        }
        string minutes = ((int)t / 60).ToString();
        string hours = ((int)t / 60 / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = hours + ":" + minutes + ":" + seconds;
        //timerden behs edir....
        //  rotationContainer.transform.rotation = Quaternion.Slerp(rotationDummy1.transform.rotation, rotationDummy2.transform.rotation, Time.deltaTime / rotTime);
        rotationContainer.transform.rotation = Quaternion.Slerp(rotationDummy1.transform.rotation, rotationDummy2.transform.rotation, (Time.time - tStart) / rotTime);

    }

    void addWalls(GameObject box, int u, int v, int w)


    {
        //  GameObject edge;
        // GameObject edge;
        //GameObject cornerEdgeU;
        //GameObject cornerEdgeV;
        // GameObject cornerEdgeW;
        edge = new GameObject();
        if (corners.Contains(new Vector3(u, v, w)))
        {
            // edge = new GameObject();
            cornerEdgeU = new GameObject();
            cornerEdgeV = new GameObject();
            cornerEdgeW = new GameObject();
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
        Material matHighlight;
        Material matNormal;
        foreach (var po in parentObjects)
        {
            bool reverseX = false;
            bool reverseY = false;
            float a1 = 2 * s * vect.x / (n - 1);
            float b1 = 2 * s * vect.y / (n - 1);
            var tri = new GameObject();
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
                    break;
                case 1://right      
                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, -s + b1, -s + a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, s - b1, s - a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), k1 * new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    temp1.x = s; // modify the component you want in the variable...                    
                    tri.transform.position = temp1;
                    reverseX = true;
                    break;
                case 2://front               
                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, -s + b1, 0), k1 * new Vector3(s - a1, -s + b1, 0), k1 * new Vector3(s - a1, s - b1, 0) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    temp1.z = -s;
                    tri.transform.position = temp1;
                    reverseX = true;
                    break;
                case 3://left

                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, s - b1, s - a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, -s + b1, -s + a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    temp1.x = -s;
                    tri.transform.position = temp1;
                    break;
                case 4://bottom        
                    mesh.vertices = new Vector3[] { k1 * new Vector3(s - a1, 0, s - b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(-s + a1, 0, -s + b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    temp1.y = -s;
                    tri.transform.position = temp1;
                    break;
                case 5://top

                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, s - b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(1 - a1 / s, 0), k1 * new Vector2(0, 0), k1 * new Vector2(0, -1 + b1 / s) };
                    //  mesh.uv = new Vector2[] { k1 * new Vector2(a1 / s - 1, 0), k1 *new  Vector2(0, 0), k1 *new  Vector2(0, 1 - b1 / s) };
                    temp1.y = s;
                    tri.transform.position = temp1;
                    reverseY = true;
                    break;

            }
            //   mesh.vertices = vertices;
            // hemcinin bunada 
            if (vect == new Vector2(0, 2))
            {
                mm = 5; if (reverseX) { mm = 4; }
                if (reverseY) { mm = 6; }
            }
            if (vect == new Vector2(0, 0))
            {
                mm = 6; if (reverseX) { mm = 7; }
                if (reverseY) { mm = 5; }
            }
            if (vect == new Vector2(2, 0))
            {
                mm = 7; if (reverseX) { mm = 6; }
                if (reverseY) { mm = 4; }
            }
            if (vect == new Vector2(2, 2))
            {
                mm = 4; if (reverseX) { mm = 5; }
                if (reverseY) { mm = 7; }
            }

            if (vect.x == vect.y) { mesh.triangles = new int[] { 0, 2, 1 }; } else { mesh.triangles = new int[] { 0, 1, 2 }; }
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


        // Material matHighlight;
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
            switch (x1)
            {
                case 0: //left
                    matHighlight = edgeMats[pos][1];
                    break;
                case 2: //right
                    matHighlight = edgeMats[pos][0];
                    break;
            }
            switch (y1)
            {
                case 0: //bottom n-1=2 baxarsan
                    matHighlight = edgeMats[pos][2];
                    break;
                case 2: //top         bottom n-1 baxarsan
                    matHighlight = edgeMats[pos][3];
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


    void OnGUI()
    {
        var sw = Screen.width; var sh = Screen.height;
        var sw16 = 0.16f * sw; var sw58 = 0.58f * sw; var sw84 = 0.84f * sw;
        GUI.skin = skin1;

        GUI.Label(new Rect(0, 0, sw16, sh), " ", "window");
        if (GUI.Button(new Rect(12, 102, 100, 40), dict["QARIÞDIR"]))
        {
            solved = false;
            scrambled = false;
            Scramble();
        }

        var mappingOld = mapMode;
        mapMode = GUI.SelectionGrid(new Rect(22, 220, 80, 80), mapMode, mapModeOptions, 1);
        if ((GUI.changed) && (mapMode != mappingOld)) { map(mapMode); }

        //if(isPirated){GUI.Label ( new Rect(sw16,0,sw84,sh), "The WebPlayer can run from the online webpage only","box2");}
    }



    public void Scramble()
    {
        f1 = true;
        //if(isPirated==true){return;}
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
            selectRotate(version, axis, rand2, rand2, rand2, true);
            rere = true;
        }
        scrambled = true;
    }
    /*
    internal IEnumerator RotateMe(Vector3 byAngles1)
    {
        var fromAngle1 = transform.rotation;
        var toAngle1 = Quaternion.Euler(transform.eulerAngles + byAngles1);
        for (var t = 0f; t < 1; t += Time.deltaTime / rotTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle1, toAngle1, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.6f);
        byAngles1 *= -1;

    }*/
    private IEnumerator coroutine;

    internal void selectRotate(string version, Vector3 axis, float ixx, float iyy, float izz, bool noSmoothing)
    {
        solved = false;
        var rotationArr = new List<Transform>();
        foreach (Transform box in transform)
        {

            switch (version)
            {
                case "x":

                    if (Mathf.Round(box.transform.position.x + 0.5f * (n - 1)) == ixx)
                    {
                        rotationArr.Add(box);
                    }
                    break;
                case "y":
                    if (Mathf.Round(box.transform.position.y + 0.5f * (n - 1)) == iyy)
                    {

                        rotationArr.Add(box);
                    }
                    break;
                case "z":
                    if (Mathf.Round(box.transform.position.z + 0.5f * (n - 1)) == izz)
                    {
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
        if (!tweening || noSmoothing)
        {
            coroutine = Example(2.0f);
            StartCoroutine(coroutine);
            rotationContainer.transform.RotateAround(Vector3.zero, -axis, 90);
            rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
        }
        else
        {
            tStart = Time.time;
            coroutine = Example(2.0f);
            StartCoroutine(coroutine);
            rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
            isTweening = false;
        }

        for (int j = 0; j < m; j++)
        {

            rotationArr[j].transform.parent = transform;
        }

    }

    IEnumerator Example(float waitTime)
    {
        //print(Time.time);
        yield return new WaitForSeconds(waitTime);
        rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
        //print(Time.time);
    }
    //sekiller
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
        foreach (Transform child in Children)
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

}
