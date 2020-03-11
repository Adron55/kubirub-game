using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonGeneral : MonoBehaviour
{
    [Header("Scene: Game")]
    public GameObject info;
    public GameObject settings;
    public GameObject languagePanel;
    public GameObject quitPopOp;
    public GameObject cameraObject;

    [Header("Scene: Discounts")]
    public GameObject discountPopUp;
    public GameObject discounts;
    public GameObject oldDiscounts;
    public GameObject deletePopUp;
    public GameObject feedbackPopUp;
    public GameObject feedbackMessage;
    public GameObject feedbackSent;


    [Header("Scene: My Profile")]
    public GameObject PassEditMode;
    public GameObject editMode;
    public GameObject genderPopUp;
    public GameObject logOutPopUp;
    public GameObject sceneManager;
    public void goToOnlineGame()
    {
        string[] objects = { "MainGameObject", "GameobjectForSavingİMG", "rotationContainer", "rotationDummy1", "rotationDummy2" };
        foreach (string names in objects)
        {
            Destroy(GameObject.Find(names));
            
        }
        SceneManager.LoadSceneAsync("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case ("beforeLoginAndRegister"):
                    Application.Quit();
                    break;
                case ("Login"):
                    SceneManager.LoadSceneAsync("BeforeLoginAndRegister");
                    break;
                case ("Register"):
                    SceneManager.LoadSceneAsync("BeforeLoginAndRegister");
                    break;
                case ("Game"):
                    if (info.activeSelf)
                    {
                        info.SetActive(false);
                    }
                    else if (languagePanel.activeSelf)
                    {
                        languagePanel.SetActive(false);
                    }
                    else if (settings.activeSelf)
                    {
                        settings.SetActive(false);
                    }
                    else if(quitPopOp.activeSelf)
                    {
                        quitPopOp.SetActive(false);
                    }
                    else
                    {
                        quitPopOp.SetActive(true);
                    }
                    if(cameraObject.GetComponentInParent<SetLock>().getLocked())
                        cameraObject.GetComponent<Cam1_1>().enabled = true;
                    break;
                case ("Discounts"):
                    if (deletePopUp.activeSelf)
                    {
                        deletePopUp.SetActive(false);
                    }
                    else if (discountPopUp.activeSelf)
                    {
                        discountPopUp.SetActive(false);
                    }
                    else if (feedbackSent.activeSelf)
                    {
                        feedbackSent.SetActive(false);
                    }
                    else if (feedbackMessage.activeSelf)
                    {
                        feedbackMessage.SetActive(false);
                    }
                    else if (feedbackPopUp.activeSelf)
                    {
                        feedbackPopUp.SetActive(false);
                    }
                    else if (oldDiscounts.activeSelf)
                    {
                        oldDiscounts.SetActive(false);
                        discounts.SetActive(true);
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync("Game");
                    }
                    //
                    break;
                case ("My Profile"):
                    if (genderPopUp.activeSelf)
                    {
                        genderPopUp.SetActive(false);
                    }
                    else if (PassEditMode.activeSelf)
                    {
                        sceneManager.GetComponent<passwordforprofile>().cancelPassEditMode();
                    }
                    else if (editMode.activeSelf)
                    {
                        sceneManager.GetComponent<passwordforprofile>().cancelEditMode();
                    }
                    else if (logOutPopUp.activeSelf)
                    {
                        logOutPopUp.SetActive(false);
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync("Game");
                    }
                    break;
            }
        }
    }
}
