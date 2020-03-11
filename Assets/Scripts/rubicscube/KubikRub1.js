#pragma strict 

import System.Collections.Generic;
import System.Text;
import System.Xml;
import System.IO;

//hec

//asagu
public  var movesText:UI.Text;
var rota:int;
var mats : Material[];
var textures : Texture2D[];
//var maintex : Texture2D;
//var arrowtex : Texture2D;
var anar : Texture2D[];
var edgeMats : List.< List.<Material> >;
var cubesize: float = 0.96;
var rotTime: float = 0.5;
var basePlane: GameObject;
var n: int;

//var testmode:boolean = false;


private var rotationContainer: GameObject;
private var rotationDummy1: GameObject;
private var rotationDummy2: GameObject;
private var corners : List.<Vector3>;
private var cornerTexs : Texture2D[];
private var edges : List.<Vector3>;;
private var superArr : List.< List.<GameObject> >;
private var s:float = cubesize/2;
private var solved:boolean = false;
private var scrambled:boolean = false;
//cube construction variables>

//<GUI variables
//GUI variables>

var cam: GameObject;

var skin1: GUISkin;
private var n1: int; 

var isTweening: boolean = false;
var tweening: boolean = false;

private var mapMode: int=1;
private var mapModeOptions: String[]=["",""];
private var tStart: float = -1;
private var tGameStart: float = 0;
private var isPirated: boolean = false;

//---<GUI strings
private var dict: Dictionary.<String,String>;
private var GUIStrings: String[] = ["YENÝL?","QARIÞDIR","cube "];
//GUI strings>---

function Start () {
   
    dict = new Dictionary.<String,String>();
    n1 = n;
    rotationContainer = new GameObject();
    rotationContainer.name = "rotationContainer";
    rotationDummy1 = new GameObject();
    rotationDummy1.name = "rotationDummy1";
    rotationDummy2 = new GameObject();
    rotationDummy2.name = "rotationDummy2";
    //<GUI text variables
    for (var key:String in GUIStrings){
    	dict[key] = key;
    }
	
//GUI text variables>
mapModeOptions = [];

//yield parseData();
mapModeOptions = [];
init();
}
public function testFunc(a:String)
    {
        Debug.Log(a);
    }
function init () {
    corners = new List.<Vector3> ();
    for (var i=0; i<2; i++) { 
        for (var j=0; j<2; j++) { 
            for (var k=0; k<2; k++) { 
                corners.Add(Vector3(i*(n-1),j*(n-1),k*(n-1)));
            }
        }
    }
    edges = new List.<Vector3> ();
    for (var i1=1; i1<n-1; i1++) {
        for (var j1=0; j1<2; j1++) { 
            for (var k1=0; k1<2; k1++) { 
                edges.Add(Vector3(i1,j1*(n-1),k1*(n-1)));
                edges.Add(Vector3(j1*(n-1),i1,k1*(n-1)));
                edges.Add(Vector3(j1*(n-1),k1*(n-1),i1));
            }
        }
    }
    var mnames: String[] =["L","R","D","UP","TL","TR","BR","BL"];
    edgeMats = new List.< List.<Material> >();
    for (var i3=0; i3<6; i3++) {
        mats[i3].SetTextureScale("_MainTex", Vector2(1,1));
    }
	
    for (var i2=0; i2<6; i2++) {
        var arr4 = new List.<Material>();
        for (var j2=0; j2<8; j2++) {
            var mat1 = Instantiate(mats[i2]);
            mat1.shader = Shader.Find( "Self-Illumin/Specular" );
            mat1.SetTexture("_Illum", textures[j2]);
            mat1.name = mnames[j2]; 
            arr4.Add(mat1);
        }
        for (var j3=0; j3<4; j3++) {//cornerMaterials
            var mat2 = Instantiate(mats[i2]);
            mat2.name = mnames[j3+4];
            arr4.Add(mat2);
        }
        edgeMats.Add(arr4);
    }
    /*
    materialin ustune sekil yapisdirmaq 
	cornerTexs = new Texture2D[4];
	var xb = cubesize*maintex.width/n;
	var yb = cubesize*maintex.height/n;
	var xo = 0.5*(1-cubesize)*maintex.width/n;
	var yo = 0.5*(1-cubesize)*maintex.height/n;
	cornerTexs[3] = new Texture2D(xb,yb,TextureFormat.ARGB32, false);
	var block:Color[]  = maintex.GetPixels(xo,yo,xb,yb,0);
	cornerTexs[3].SetPixels(0,0,xb,yb,block,0);
	cornerTexs[3].Apply();
	block  = maintex.GetPixels(xo,maintex.height-yo-yb,xb,yb,0);//xo,yo,xb,yb,0);
	cornerTexs[0] = new Texture2D(xb,yb,TextureFormat.ARGB32, false);
	cornerTexs[0].SetPixels(0,0,xb,yb,block,0);
	cornerTexs[0].Apply();
	block  = maintex.GetPixels(maintex.width-xo-xb,maintex.height-yo-yb,xb,yb,0);//xo,yo,xb,yb,0);
	cornerTexs[1] = new Texture2D(xb,yb,TextureFormat.ARGB32, false);
	cornerTexs[1].SetPixels(0,0,xb,yb,block,0);
	cornerTexs[1].Apply();
	block  = maintex.GetPixels(maintex.width-xo-xb,yo,xb,yb,0);//xo,yo,xb,yb,0);
	cornerTexs[2] = new Texture2D(xb,yb,TextureFormat.ARGB32, false);
	cornerTexs[2].SetPixels(0,0,xb,yb,block,0);
	cornerTexs[2].Apply();
	*/
    sceneSetup();
}

function sceneSetup () {
    //Kameranin baxdigi 
    //	basePlane.transform.position.y = -2*n;
    //	cam.transform.position = Vector3.zero ;
    //	cam.transform.rotation.eulerAngles = Vector3.zero ;
    //	cam.transform.position.z = - n*2;
    //cam.transform.RotateAround(Vector3.zero,Vector3.up,45); 
    ///cam.GetComponent(Cameraorbit).distance = n*2;
    //	cam.GetComponent(Cameraorbit).distanceMin = n;
    //	cam.GetComponent(Cameraorbit).distanceMax = n*5;
    //	cam.GetComponent(Cameraorbit).Init();
    //	cam.GetComponent(Cameraorbit).enabled = true;
    Supercube ();
}

function Supercube () {
    superArr = new List.< List.<GameObject> >();//verification Array 
    for (var i1=0; i1<6; i1++) { 
        var sideArray = new List.<GameObject>();//side controll arrays
        superArr.Add(sideArray);
    }
    for (var i=0; i<n; i++) { 
        for (var j=0; j<n; j++) { 
            for (var k=0; k<n; k++) { 
                var cube:GameObject;
                if(i!=0&&j!=0&&k!=0&&i!=n-1&&j!=n-1&&k!=n-1){
                    cube = new GameObject ();
                    //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.localScale = Vector3(cubesize,cubesize,cubesize);
                    //cube.GetComponent.<Renderer>().material = mats[6];
                }else{
                    cube = new GameObject ();
                    addWalls(cube,i,j,k);
                }
                cube.name = "cube"+i+j+k;
                var c1: float = -n*0.5;
                cube.transform.parent = transform;
                cube.transform.localPosition = Vector3(-n*0.5 + i+0.5,-n*0.5 +j+0.5,-n*0.5 +k+0.5);
                if(i==0){superArr[0].Add(cube);}
                if(i==n-1){superArr[1].Add(cube);}
                if(j==0){superArr[2].Add(cube);}
                if(j==n-1){superArr[3].Add(cube);}
                if(k==0){superArr[4].Add(cube);}
                if(k==n-1){superArr[5].Add(cube);}
            }
        }
    }
    map(mapMode);
}

function Update () {
    rotationContainer.transform.rotation = Quaternion.Slerp (rotationDummy1.transform.rotation, rotationDummy2.transform.rotation, (Time.time - tStart)/rotTime);
}
	
function addWalls(box:GameObject,u:int,v:int,w:int){
    var edge: GameObject;
    var cornerEdgeU: GameObject;
    var cornerEdgeV: GameObject;
    var cornerEdgeW: GameObject;
    if(corners.Contains(Vector3(u,v,w))){
        edge=new GameObject();
        cornerEdgeU = new GameObject();
        cornerEdgeV = new GameObject();
        cornerEdgeW = new GameObject();
        cornerEdgeU.transform.parent = box.transform;
        cornerEdgeV.transform.parent = box.transform;
        cornerEdgeW.transform.parent = box.transform;
    }
	
    if(edges.Contains(Vector3(u,v,w))){
        edge = new GameObject ();
        edge.name = "edge" +u+v+w;
        edge.transform.parent = box.transform;
    }
		
    if(w==n-1){//back
        if(Vector2(u,v)==Vector2(0,0)||Vector2(u,v)==Vector2(n-1,0)||Vector2(u,v)==Vector2(0,n-1)||Vector2(u,v)==Vector2(n-1,n-1)){
            cornerWall(0,Vector2(u,v),cornerEdgeU,cornerEdgeV);
        }else{ 
            if(u==0||u==n-1||v==0||v==n-1){wall(0,"outer",u,v,edge,true);}else{wall(0,"outer",u,v,box,false);}
        }
    }else{wall(0,"inner",u,v,box,false);}
    if(u==n-1){//right
        if(Vector2(w,v)==Vector2(0,0)||Vector2(w,v)==Vector2(n-1,0)||Vector2(w,v)==Vector2(0,n-1)||Vector2(w,v)==Vector2(n-1,n-1)){
            cornerWall(1,Vector2(w,v),cornerEdgeW,cornerEdgeV);
        }else{ 
            if(w==0||w==n-1||v==0||v==n-1){wall(1,"outer",n-1-w,v,edge,true);}else{wall(1,"outer",n-1-w,v,box,false);}
        }
    }else{wall(1,"inner",w,v,box,false);}
    if(w==0){//front
        if(Vector2(u,v)==Vector2(0,0)||Vector2(u,v)==Vector2(n-1,0)||Vector2(u,v)==Vector2(0,n-1)||Vector2(u,v)==Vector2(n-1,n-1)){
            cornerWall(2,Vector2(u,v),cornerEdgeU,cornerEdgeV);
        }else{ 
            if(u==0||u==n-1||v==0||v==n-1){wall(2,"outer",n-1-u,v,edge,true);}else{wall(2,"outer",n-1-u,v,box,false);}
        }
    }else{wall(2,"inner",-u,v,box,false);}
    if(u==0){//left
        if(Vector2(w,v)==Vector2(0,0)||Vector2(w,v)==Vector2(n-1,0)||Vector2(w,v)==Vector2(0,n-1)||Vector2(w,v)==Vector2(n-1,n-1)){
            cornerWall(3,Vector2(w,v),cornerEdgeW,cornerEdgeV);
        }else{ 
            if(w==0||w==n-1||v==0||v==n-1){wall(3,"outer",w,v,edge,true);}else{wall(3,"outer",w,v,box,false);}
        }
    }else{wall(3,"inner",w,v,box,false);}
    if(v==0){//bottom
        if(Vector2(w,u)==Vector2(0,0)||Vector2(w,u)==Vector2(n-1,0)||Vector2(w,u)==Vector2(0,n-1)||Vector2(w,u)==Vector2(n-1,n-1)){
            cornerWall(4,Vector2(u,w),cornerEdgeU,cornerEdgeW);
        }else{ 
            if(w==0||w==n-1||u==0||u==n-1){wall(4,"outer",u,w,edge,true);}else{wall(4,"outer",u,w,box,false);}
        }
    }else{wall(4,"inner",w,u,box,false);}
    if(v==n-1){//top
        if(Vector2(w,u)==Vector2(0,0)||Vector2(w,u)==Vector2(n-1,0)||Vector2(w,u)==Vector2(0,n-1)||Vector2(w,u)==Vector2(n-1,n-1)){
            cornerWall(5,Vector2(u,w),cornerEdgeU,cornerEdgeW);
        }else{ 
            if(w==0||w==n-1||u==0||u==n-1){wall(5,"outer",u,n-1-w,edge,true);}else{wall(5,"outer",u,n-1-w,box,false);}
        }
    }else{wall(5,"inner",w,u,box,false);}
}	

    function cornerWall (pos:int,vect:Vector2,parentObject1:GameObject,parentObject2:GameObject) {
        var parentObjects: GameObject[] = [parentObject1,parentObject2];
        var k1=1;
        var matHighlight: Material;
        var matNormal: Material;
        for (var po in parentObjects) {
            var reverseX:boolean = false;
            var reverseY:boolean = false;
            var tri = new GameObject();
            var a1 = 2*s*vect.x/(n-1);
            var b1 = 2*s*vect.y/(n-1);
            var mesh : Mesh = new Mesh ();
            tri.transform.parent = po.transform;
            tri.AddComponent(MeshFilter);
            tri.AddComponent(MeshRenderer);
            tri.GetComponent(MeshFilter).mesh = mesh;
            switch(pos){
                case 0:	//back
                    mesh.vertices = [k1*Vector3(s-a1,s-b1,0), k1*Vector3(s-a1,-s+b1,0), k1*Vector3(-s+a1,-s+b1,0)];
                    mesh.uv = [k1*Vector2 (0, 1 - b1/s), k1*Vector2 (0, 0), k1*Vector2 (1-a1/s, 0)];/////
                    tri.transform.localPosition.z = s;
                    break;
                case 1://right
                    mesh.vertices = [k1*Vector3(0,-s+b1,-s+a1), k1*Vector3(0,-s+b1,s-a1), k1*Vector3(0,s-b1,s-a1)];
                    mesh.uv = [k1*Vector2 (-1+a1/s, 0), Vector2 (0, 0), k1*Vector2 (0, 1- b1/s)];
                    tri.transform.localPosition.x = s;  reverseX = true;
                    break;
                case 2://front
                    mesh.vertices = [k1*Vector3(-s+a1,-s+b1,0), k1*Vector3(s-a1,-s+b1,0), k1*Vector3(s-a1,s-b1,0)];
                    mesh.uv = [k1*Vector2 (-1+a1/s, 0), Vector2 (0, 0), k1*Vector2 (0, 1 - b1/s)];
                    tri.transform.localPosition.z = -s;  reverseX = true;		
                    break;
                case 3://left
                    mesh.vertices = [k1*Vector3(0,s-b1,s-a1), k1*Vector3(0,-s+b1,s-a1), k1*Vector3(0,-s+b1,-s+a1)];
                    mesh.uv = [k1*Vector2 (0, 1 - b1/s), k1*Vector2 (0, 0), k1*Vector2 (1-a1/s, 0)];
                    tri.transform.localPosition.x = -s;			
                    break;
                case 4://bottom
                    mesh.vertices = [k1*Vector3(s-a1,0,s-b1), k1*Vector3(s-a1,0,-s+b1), k1*Vector3(-s+a1,0,-s+b1)];
                    mesh.uv = [k1*Vector2 (0, 1 - b1/s), k1*Vector2 (0, 0), k1*Vector2 (1-a1/s, 0)];
                    tri.transform.localPosition.y = -s;
                    break;
                case 5://top
                    mesh.vertices = [k1*Vector3(-s+a1,0,-s+b1), k1*Vector3(s-a1,0,-s+b1), k1*Vector3(s-a1,0,s-b1)];
                    mesh.uv = [k1*Vector2 (1-a1/s, 0), k1*Vector2 (0, 0), k1*Vector2 (0, -1+b1/s)];
                    //mesh.uv = [k1*Vector2 (a1/s-1, 0), k1*Vector2 (0, 0), k1*Vector2 (0, 1-b1/s)];
                    tri.transform.localPosition.y = s;	 reverseY = true;	
                    break;
            }
            var mm:int;
            switch(vect){
                case Vector2(0,n-1):	//top-left	
                    mm=5; if(reverseX){mm=4;} if(reverseY){mm=6;}
                    break;
                case Vector2(0,0):	//bottom-left	
                    mm=6; if(reverseX){mm=7;} if(reverseY){mm=5;}
                    break;
                case Vector2(n-1,0):	//bottom-left	
                    mm=7; if(reverseX){mm=6;} if(reverseY){mm=4;}
                    break;
                case Vector2(n-1,n-1):	//bottom-left	
                    mm=4; if(reverseX){mm=5;} if(reverseY){mm=7;}
                    break;
            }
			
            if(vect.x==vect.y){mesh.triangles = [0, 2, 1];}else{mesh.triangles = [0, 1, 2];}
            mesh.RecalculateNormals();
            tri.AddComponent(MeshCollider);
            tri.AddComponent(mouseInteract); tri.GetComponent(mouseInteract).n=n;
            tri.GetComponent(mouseInteract).mat=edgeMats[pos][mm+4];
            tri.GetComponent.<Renderer>().material = edgeMats[pos][mm+4];
            tri.GetComponent(mouseInteract).matOver=edgeMats[pos][mm];
		
            k1 *= -1;
        }
    }

        function wall (pos:int,inner:String,x1:int,y1:int,parentObject:GameObject,interactive:boolean) {
            var matHighlight: Material;
            var square = new GameObject();
            var mesh : Mesh = new Mesh ();
            square.transform.parent = parentObject.transform;
            square.AddComponent(MeshFilter);
            square.AddComponent(MeshRenderer);
            square.GetComponent(MeshFilter).mesh = mesh;
            mesh.vertices = [Vector3(s,s,0), Vector3(s,-s,0), Vector3(-s,-s,0),Vector3(-s,s,0)];
            mesh.uv = [Vector2 (-x1-0.5-s, y1+0.5+s)/n, Vector2 (-x1-0.5-s, y1+0.5-s)/n, Vector2 (-x1-0.5+s, y1+0.5-s)/n, Vector2 (-x1-0.5+s, y1+0.5+s)/n];
            mesh.triangles = [0, 2, 1, 0, 3, 2];
            mesh.RecalculateNormals();
            square.transform.position.z = s;
            switch(pos){
                case 0:	//back
                    break;
                case 1://right
                    square.transform.RotateAround(Vector3.zero,Vector3.up,90); 
                    break;
                case 2://front
                    square.transform.RotateAround(Vector3.zero,Vector3.up,180); 
                    break;
                case 3://left
                    square.transform.RotateAround(Vector3.zero,Vector3.up,270); 
                    break;
                case 4://bottom
                    square.transform.RotateAround(Vector3.zero,Vector3.right,90); 
                    break;
                case 5://top
                    square.transform.RotateAround(Vector3.zero,Vector3.right,270); 
                    break;
            }
            if(inner=="inner"){square.GetComponent.<Renderer>().material = mats[6];}else{
                square.GetComponent.<Renderer>().material = mats[pos];
                switch(x1){
                    case 0:	//left
                        matHighlight = edgeMats[pos][1];
                        break;
                    case n-1:	//right
                        matHighlight = edgeMats[pos][0];
                        break;
                }
                switch(y1){
                    case 0:	//bottom
                        matHighlight = edgeMats[pos][2];
                        break;
                    case n-1:	//top
                        matHighlight = edgeMats[pos][3];
                        break;
                }
            }
            if(interactive){
                square.AddComponent(MeshCollider);
                square.AddComponent(mouseInteract); square.GetComponent(mouseInteract).n=n;
                square.GetComponent(mouseInteract).mat=mats[pos];
                square.GetComponent(mouseInteract).matOver=matHighlight;
            }
        }
        /*
            function OnGUI () {
                var sw = Screen.width; var sh = Screen.height;
                var sw16 = 0.16*sw; var sw58 = 0.58*sw; var sw84 = 0.84*sw;
                GUI.skin = skin1;

                GUI.Label (Rect (0,0,sw16,sh), " ","window");
                if (GUI.Button (Rect (12,12,100,20), dict["YENÝL?"])) {
                    Rebuild();
                }
                if (GUI.Button (Rect (12,42,100,20), dict["QARIÞDIR"])) {
                    solved = false; 
                    scrambled = false;
                    Scramble();
                }
	
                var mappingOld = mapMode;
                mapMode = GUI.SelectionGrid (Rect (22, 220, 80, 80), mapMode, mapModeOptions, 1);
                if((GUI.changed)&&(mapMode!=mappingOld)){map(mapMode);}

                //if(isPirated){GUI.Label (Rect (sw16,0,sw84,sh), "The WebPlayer can run from the online webpage only","box2");}
            }*/

            function Rebuild () {

                solved = false;
                scrambled = false;
                var toDestroy = new Array();
                for (var child:Transform in transform) {
                    toDestroy.Push(child.gameObject);
                }
            var m = toDestroy.length;
            for (var i=0; i<m; i++) {
                Destroy(toDestroy[i]);
            }
            n = n1;
            //yield WaitForSeconds (10);
            //cam.GetComponent(Cameraorbit).enabled = false;
            //yield WaitForSeconds (0.1);
            init();
        }

        function Scramble () {
            var version:String;
            var axis:Vector3;
            //if(isPirated==true){return;}
            for (var i=0; i<8*n; i++) {
                var rand1 = Mathf.Round(6*Random.value-0.5);
                var rand2 = Mathf.Round(n*Random.value-0.5);
                switch(rand1){
                    case 0:
                        version = "x";axis=Vector3.right;
                        break;
                    case 1:
                        version = "x";axis=-Vector3.right;
                        break;
                    case 2:
                        version = "y";axis=Vector3.up;
                        break;
                    case 3:
                        version = "y";axis=-Vector3.up;
                        break;
                    case 4:
                        version = "z";axis=Vector3.forward;
                        break;
                    case 5:
                        version = "z";axis=-Vector3.forward;
                        break;
                }
                selectRotate(version, axis, rand2,rand2,rand2,true);
            }
            scrambled = true;
        }


        function selectRotate(version:String, axis:Vector3, ixx:int,iyy:int,izz:int,noSmoothing:boolean)
            {
                solved = false;
                var rotationArr = new List.<Transform>();//
     
                for (var box:Transform in transform) {
                    switch(version){
                        case "x":
                        if(Mathf.Round(box.transform.position.x+0.5*(n-1))==ixx){
                            rotationArr.Add(box);
                            }
            break;
                    case "y":
			if(Mathf.Round(box.transform.position.y+0.5*(n-1))==iyy){
				rotationArr.Add(box);
                }
        break;
			case"z":
			if(Mathf.Round(box.transform.position.z+0.5*(n-1))==izz){
				rotationArr.Add(box);
            }
            break;
}
}
var m = rotationArr.Count;
	
for (var i=0; i<m; i++) {
    rotationArr[i].transform.parent = rotationContainer.transform;
}
rotationDummy2.transform.RotateAround(Vector3.zero,-axis,90);
if(!tweening||noSmoothing){
      
    rotationContainer.transform.RotateAround(Vector3.zero,-axis,90);
    rotationDummy1.transform.RotateAround(Vector3.zero,-axis,90);
}else{   
    tStart = Time.time;
    yield WaitForSeconds(0.6);
 //   Debug.Log("Isleyir");
    rotationDummy1.transform.RotateAround(Vector3.zero,-axis,90);
    isTweening = false;
}
print(axis);
for (var j=0;j<m; j++) {
    rotationArr[j].transform.parent = transform;
}
if (noSmoothing == true) {rota = 0; }else if (noSmoothing == false) { rota++; }
movesText.text = "Moves:" + rota;

}
	

//sekiller
function map (ver:int) {
    var mtex:Texture = null;
    var ctex = new Array(null,null,null,null);
    switch(ver){
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
    var children = GetComponentsInChildren (Transform);
    //arrow olan hisse her xanaya sekil elave etme 
    for (var child : Transform in children) {
		if(child.gameObject.GetComponent.<Renderer>()){
			var cmat:Material = child.gameObject.GetComponent.<Renderer>().material;
			if(cmat.color!=mats[6].color){cmat.SetTexture("_MainTex", mtex);}
    var scr1:mouseInteract = child.gameObject.GetComponent(mouseInteract);
    if(scr1){
        switch(scr1.matOver.name){
            case "BL":
                scr1.mat.SetTexture("_MainTex", ctex[3]);scr1.matOver.SetTexture("_MainTex", ctex[3]);
                child.gameObject.GetComponent.<Renderer>().material.SetTexture("_MainTex", ctex[3]);
                break;
            case "TL":
                scr1.mat.SetTexture("_MainTex", ctex[0]);scr1.matOver.SetTexture("_MainTex", ctex[0]);
                child.gameObject.GetComponent.<Renderer>().material.SetTexture("_MainTex", ctex[0]);
                break;
            case "TR":
                scr1.mat.SetTexture("_MainTex", ctex[1]);scr1.matOver.SetTexture("_MainTex", ctex[1]);
                child.gameObject.GetComponent.<Renderer>().material.SetTexture("_MainTex", ctex[1]);
                break;
            case "BR":
                scr1.mat.SetTexture("_MainTex", ctex[2]);scr1.matOver.SetTexture("_MainTex", ctex[2]);
                child.gameObject.GetComponent.<Renderer>().material.SetTexture("_MainTex", ctex[2]);
                break;						
            default:
                scr1.mat.SetTexture("_MainTex", mtex);scr1.matOver.SetTexture("_MainTex", mtex);
                scr1.matOver.SetTextureScale("_Illum", Vector2(n,n));
                if(ver==1){cmat.SetTextureScale("_MainTex", Vector2(n,n));scr1.mat.SetTextureScale("_MainTex", Vector2(n,n));scr1.matOver.SetTextureScale("_MainTex", Vector2(n,n));
                }else{cmat.SetTextureScale("_MainTex", Vector2(1,1));scr1.mat.SetTextureScale("_MainTex", Vector2(1,1));scr1.matOver.SetTextureScale("_MainTex", Vector2(1,1));}
                break;
        }
    }else{if(ver==1){cmat.SetTextureScale("_MainTex", Vector2(n,n));}else{cmat.SetTextureScale("_MainTex", Vector2(1,1));}}
}
}
}

function verify () 
{

    for (var i1=0;i1<6; i1++) {
        var xequal:boolean = true;
        var yequal:boolean = true;
        var zequal:boolean = true;
        var x0 : int = Mathf.Round(2*superArr[i1][0].transform.position.x);
        var y0 : int = Mathf.Round(2*superArr[i1][0].transform.position.y);
        var z0 : int = Mathf.Round(2*superArr[i1][0].transform.position.z);
        var rot0 = superArr[i1][0].transform.rotation.eulerAngles.ToString();
        for (var i2=1;i2<n*n; i2++) {
            var x1 : int = Mathf.Round(2*superArr[i1][i2].transform.position.x);
            var y1 : int = Mathf.Round(2*superArr[i1][i2].transform.position.y);
            var z1 : int = Mathf.Round(2*superArr[i1][i2].transform.position.z);
            if(x0!=x1){xequal=false;}
            if(y0!=y1){yequal=false;}
            if(z0!=z1){zequal=false;}
            if(!xequal&&!yequal&&!zequal){return false;}
			
            var onEdge:boolean = false;//whether cube is on edge
            if(i2<=n||i2>=n*(n-1)||i2%n==0||(i2+1)%n==0){onEdge=true; }
            if(mapMode!=0||onEdge){
                var rot1 = superArr[i1][i2].transform.rotation.eulerAngles.ToString();
                if(rot0!=rot1){return false;} 
                rot0 = rot1;
            }
            if(mapMode==2&&!onEdge){
                var dif : int= 0; //the neighbouring cubes coordinates should not differ more than one
                if(x0!=x1){dif = x1-x0; if(dif!=2&&dif!=-2){return false;}}
                if(y0!=y1){dif = y1-y0; if(dif!=2&&dif!=-2){return false;}}
                if(z0!=z1){dif = z1-z0; if(dif!=2&&dif!=-2){return false;}}
            }
            x0 = x1;
            y0 = y1;
            z0 = z1;
        }
    }
    return true;
}
