using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ToastPlugin;
public class mouseInteract : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private List<Transform> gameobjs = new List<Transform>();
    private string firstPosParName,centerParName;
    Vector3 startPos, dragPos;
    string rotationThru="",childElem="";
    float ixxx; float iyyy; float izzz;
    public int n=3;
    int rotDefiner = 1,rotDefinerTwo=1;
    public Transform superCube;
    public KubikRub cubeScript; //main script
    public Transform CameraObject;
    public SetLock setLock;
    public Material mat;
    public Material matOver;
    Vector3 axis;
    string version;
    bool dragging = false,rotated=false,startFromE=false,canRot=false,cameraScript=true,rotThru=false;
    private static GameObject instance;
    private GameObject onDrag, onDown, onUp,sameGM,mainIDea,allGMKing;
    void Start()
    {
        
        superCube = transform.parent.parent.parent.transform; 
        cubeScript = superCube.GetComponent<KubikRub>();
        CameraObject = superCube.parent.transform.Find("CameraObject");
        setLock = CameraObject.GetComponent<SetLock>();
    }

    public void OnPointerDown(PointerEventData pd)
    {
        try
        {
            allGMKing = new GameObject();
            cubeScript.cam.GetComponent<Cam1_1>().enabled = false;
            rotated = false;
            gameobjs.Clear();
            getRayObjectItself(pd);
            firstPosParName = getRayObject(pd).name;
            startPos = pd.position;
            if (firstPosParName.Contains("edge"))
            {
                startFromE = true;
                sameGM = getRayObject(pd).transform.parent.gameObject;
                rotationThru = "edge";
                rotThru = true;
            }
            else if (firstPosParName.Contains("U"))
            {
                sameGM = getRayObject(pd).transform.parent.gameObject;
                rotationThru="U";
                rotThru = true;
            }
            else if (firstPosParName.Contains("W"))
            {
                sameGM = getRayObject(pd).transform.parent.gameObject;
                rotationThru = "W";
                rotThru = true;
            }
            else if (firstPosParName.Contains("V"))
            {
                sameGM = getRayObject(pd).transform.parent.gameObject;
                rotationThru = "V";
                rotThru = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    public void OnDrag(PointerEventData pd)
    {
        try
        {
            dragPos = pd.position;
            if (pd.pointerCurrentRaycast.gameObject != null)
            {
                onDrag = pd.pointerCurrentRaycast.gameObject;
          
                cubeScript.cam.GetComponent<Cam1_1>().enabled = false;
                cameraScript = true;
                if (!dragging)
                {
                    centerParName = pd.pointerCurrentRaycast.gameObject.transform.parent.name;
                    dragging = true;
                }
                if (gameobjs.Count < 2)
                {
                    getRayObjectItself(pd);
                }
                if (!pd.pointerCurrentRaycast.gameObject.transform.parent.name.Equals(firstPosParName))
                {
                    canRot = true;
                }
                
                if ((gameobjs.Count >= 2 ||(startFromE && canRot)) && !rotated)
                {
                    rotated = true;
                    allGMKing = this.gameObject;
                    OnMouseUpp();//to rotate
                    return;
                }
                else if (!rotated && rotThru && pd.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.name.Equals(sameGM.name) && !childElem.Equals(pd.pointerCurrentRaycast.gameObject.name))
                {

                    rotThru = false;
                    if (rotationThru.Equals(pd.pointerCurrentRaycast.gameObject.transform.parent.name) || rotationThru.Contains("edge"))
                    {
                        allGMKing = this.gameObject;
                        rotDefiner = -1;
                        OnMouseUpp();
                        rotated = true;
                        return;
                    }
                    else
                    {
                   //     Debug.Log("UnResponsive");

                        GameObject gameObjectCh = pd.pointerCurrentRaycast.gameObject;
                        gameObjectCh = gameObjectCh.transform.parent.parent.gameObject;
                        int countGM = gameObjectCh.transform.childCount;
                        mainIDea = new GameObject();
                        allGMKing = new GameObject();
                        for (int i = 0; i < countGM; i++)
                        {
                            GameObject gmChild = gameObjectCh.transform.GetChild(i).gameObject;
                            if (gmChild.name.Length==1 && !gmChild.name.Equals(rotationThru))
                            {
                                if (gmChild.transform.Find(childElem))
                                {
                                    mainIDea = gmChild.transform.Find(childElem).gameObject;
                                    //Debug.Log(this.gameObject.transform.parent.name + "    <-->   " + mainIDea.transform.parent.name);
                                    allGMKing = mainIDea;
                                    break;
                                }
                            }
                            
                        }
                        rotDefiner = -1;
                        rotDefinerTwo = 1;
                        OnMouseUpp();
                        rotated = true;
                        return;

                    }
                }

            }
        }
        catch(Exception ex)
        {
         //   ToastHelper.ShowToast(ex.ToString());
        }
    }
    
    public void OnPointerUp(PointerEventData pd)
    {
        try
        {
            if(setLock.getLocked())
                cubeScript.cam.GetComponent<Cam1_1>().enabled = true;
            
            gameobjs.Clear();
            dragging = false;
            centerParName = "";
            firstPosParName = "";
            rotated = false;
            startFromE = false;
            canRot = false;
            cameraScript = true;
            sameGM = new GameObject();
            rotationThru = "";
            rotDefiner = 1;
            rotDefinerTwo = 1;
            childElem = "";
            rotThru = true;
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }


    string showList()
    {
        string st = "";
        for (int i = 0; i < gameobjs.Count; i++) {

            if(gameobjs[i]!=null)
                st+=gameobjs[i].name+" ";
                
        }
        return st;
    }
    void OnMouseUpp()
    {
        
        if (cubeScript.tweening)
        {
            if (cubeScript.isTweening) { return;} else { cubeScript.isTweening = true; }
        }



        float ix = Mathf.Round(transform.parent.transform.position.x + 0.5f * (n - 1));
        float iy = Mathf.Round(transform.parent.transform.position.y + 0.5f * (n - 1));
        float iz = Mathf.Round(transform.parent.transform.position.z + 0.5f * (n - 1));


        float ixx = Mathf.Round(2 * allGMKing.transform.position.x);
        float iyy = Mathf.Round(2 * allGMKing.transform.position.y);
        float izz = Mathf.Round(2 * allGMKing.transform.position.z);


        foreach (Transform child in allGMKing.transform.parent.transform)
        {
            if (child.transform != allGMKing.transform)
            {
                ixxx = Mathf.Round(2 * child.transform.position.x);
                iyyy = Mathf.Round(2 * child.transform.position.y);
                izzz = Mathf.Round(2 * child.transform.position.z);
            }
        }
        if (rotDefiner == 1)//for normal beforehand rotation
        {


            try
            {
                int x, y, z;
                x = Convert.ToInt16(gameobjs[0].position.x - gameobjs[1].position.x);
                y = Convert.ToInt16(gameobjs[0].position.y - gameobjs[1].position.y);
                z = Convert.ToInt16(gameobjs[0].position.z - gameobjs[1].position.z);

                if (x == 0 && y == 0)
                {
                    izzz = z * n;
                    ixxx = -4;
                    iyyy = -4;
                }
                if (z == 0 && y == 0)
                {
                    ixxx = x * n;
                    izzz = -4;
                    iyyy = -4;
                }
                if (x == 0 && z == 0)
                {
                    iyyy = y * n;
                    ixxx = -4;
                    izzz = -4;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }


            if (izz == n)
            {
                if (ixxx == -n || (ixxx % 3 == 0 && (ixxx / 3) < 0)) { version = "y"; axis = -Vector3.up; }
                if (ixxx == n || (ixxx % 3 == 0 && (ixxx / 3) > 0)) { version = "y"; axis = Vector3.up; }
                if (iyyy == -n || (iyyy % 3 == 0 && (iyyy / 3) < 0)) { version = "x"; axis = Vector3.right; }
                if (iyyy == n || (iyyy % 3 == 0 && (iyyy / 3) > 0)) { version = "x"; axis = -Vector3.right; }
            }
            if (iyy == n)
            {
                if (izzz == -n || (izzz % 3 == 0 && (izzz / 3) < 0)) { version = "x"; axis = -Vector3.right; }
                if (izzz == n || (izzz % 3 == 0 && (izzz / 3) > 0)) { version = "x"; axis = Vector3.right; }
                if (ixxx == -n || (ixxx % 3 == 0 && (ixxx / 3) < 0)) { version = "z"; axis = Vector3.forward; }
                if (ixxx == n || (ixxx % 3 == 0 && (ixxx / 3) > 0)) { version = "z"; axis = -Vector3.forward; }
            }
            if (ixx == n)
            {
                if (izzz == -n || (izzz % 3 == 0 && (izzz / 3) < 0)) { version = "y"; axis = Vector3.up; }
                if (izzz == n || (izzz % 3 == 0 && (izzz / 3) > 0)) { version = "y"; axis = -Vector3.up;}
                if (iyyy == -n || (iyyy % 3 == 0 && (iyyy / 3) < 0)) { version = "z"; axis = -Vector3.forward;  }
                if (iyyy == n || (iyyy % 3 == 0 && (iyyy / 3) > 0)) { version = "z"; axis = Vector3.forward; }
            }
            if (izz == -n)
            {
                if (ixxx == -n || (ixxx % 3 == 0 && (ixxx / 3) < 0)) { version = "y"; axis = Vector3.up; }
                if (ixxx == n || (ixxx % 3 == 0 && (ixxx / 3) > 0)) { version = "y"; axis = -Vector3.up;  }
                if (iyyy == -n || (iyyy % 3 == 0 && (iyyy / 3) < 0)) { version = "x"; axis = -Vector3.right;}
                if (iyyy == n || (iyyy % 3 == 0 && (iyyy / 3) > 0)) { version = "x"; axis = Vector3.right;  }
            }
            if (iyy == -n)
            {
                if (izzz == -n || (izzz % 3 == 0 && (izzz / 3) < 0)) { version = "x"; axis = Vector3.right; }
                if (izzz == n || (izzz % 3 == 0 && (izzz / 3) > 0)) { version = "x"; axis = -Vector3.right;  }
                if (ixxx == -n || (ixxx % 3 == 0 && (ixxx / 3) < 0)) { version = "z"; axis = -Vector3.forward;  }
                if (ixxx == n || (ixxx % 3 == 0 && (ixxx / 3) > 0)) { version = "z"; axis = Vector3.forward; }
            }
            if (ixx == -n)
            {
                if (izzz == -n || (izzz % 3 == 0 && (izzz / 3) < 0)) { version = "y"; axis = -Vector3.up; }
                if (izzz == n || (izzz % 3 == 0 && (izzz / 3) > 0)) { version = "y"; axis = Vector3.up;}
                if (iyyy == -n || (iyyy % 3 == 0 && (iyyy / 3) < 0)) { version = "z"; axis = Vector3.forward;}
                if (iyyy == n || (iyyy % 3 == 0 && (iyyy / 3) > 0)) { version = "z"; axis = -Vector3.forward; }

            }
        }
        else//for another case rotation
        {
            //Debug.Log(String.Format("ixx: {0}; iyy: {1}; izz: {2};[[[<==>]]]; ixxx: {3};iyyy: {4}; izzz: {5};\n-------------------------------------",ixx, iyy, izz, ixxx, iyyy, izzz));
            if (izz == -n)
            {
                if (ixxx == -n) { version = "y"; axis = -Vector3.up; }
                if (ixxx == n) { version = "y"; axis = Vector3.up; }
                if (iyyy == -n) { version = "x"; axis = Vector3.right; }
                if (iyyy == n) { version = "x"; axis = -Vector3.right; }
            }///*bundan sonraki iki if i komente almisdin
            if (iyy == -n)
            {
                if (izzz == -n) { version = "x"; axis = -Vector3.right; }
                if (izzz == n) { version = "x"; axis = Vector3.right; }
                if (ixxx == -n) { version = "z"; axis = Vector3.forward; }
                if (ixxx == n) { version = "z"; axis = -Vector3.forward; }
            }
            if (ixx == -n)
            {
                if (izzz == -n) { version = "y"; axis = Vector3.up; }
                if (izzz == n) { version = "y"; axis = -Vector3.up; }
                if (iyyy == -n) { version = "z"; axis = -Vector3.forward; }
                if (iyyy == n) { version = "z"; axis = Vector3.forward; }
            }
            if (izz == n)
            {
                if (ixxx == -n) { version = "y"; axis = Vector3.up; }
                if (ixxx == n) { version = "y"; axis = -Vector3.up; }
                if (iyyy == -n) { version = "x"; axis = -Vector3.right; }
                if (iyyy == n) { version = "x"; axis = Vector3.right; }
            }///*bundan sonraki iki if-i komente almiosdin
            if (iyy == n)
            {
                if (izzz == -n) { version = "x"; axis = Vector3.right; }
                if (izzz == n) { version = "x"; axis = -Vector3.right; }
                if (ixxx == -n) { version = "z"; axis = -Vector3.forward; }
                if (ixxx == n) { version = "z"; axis = Vector3.forward; }
            }
            if (ixx == n)
            {
                if (izzz == -n) { version = "y"; axis = -Vector3.up; }
                if (izzz == n) { version = "y"; axis = Vector3.up; }
                if (iyyy == -n) { version = "z"; axis = Vector3.forward; }
                if (iyyy == n) { version = "z"; axis = -Vector3.forward; }
            }
        }
     //   Debug.Log(String.Format("version:{0} ;axis:{1} ;ix:{2} ;iy:{3} ;iz:{4} ;\n____________________________________",version,axis,ix,iy,iz));
        cubeScript.AnimeRotate(version, axis, ix, iy, iz, false);
          
        
    } //MouseUp
  
    private GameObject getRayObject(PointerEventData pointer)
    {

        GameObject gm=new GameObject();
        
        gm =pointer.pointerCurrentRaycast.gameObject;
        childElem =gm.name;
        gm = gm.gameObject.transform.parent.gameObject;
        return gm;
    }
    private void getRayObjectItself(PointerEventData pointer)
    {

            GameObject gm = pointer.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
            if (gameobjs.Count < 3)
            {
                if (isIN(gm.transform.parent))
                {
                    gameobjs.Add(gm.transform.parent);
                }
            }

    }


    private bool isIN(Transform gameObject) {
        bool f = true;
        for (int i = 0; i < gameobjs.Count; i++) {

            try
            {
                if (gameObject != null && gameObject.name.Equals(gameobjs[i].name))
                {
                    f = false;
                    break;
                }
                

            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }
            
        }
        return f;
    }
}