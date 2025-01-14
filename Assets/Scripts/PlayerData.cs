using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication.PlayerAccounts;
using System;
using Unity.Services.CloudSave.Models.Data.Player;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    private int highScore_AimematchicsI, highScore_AimematchicsII, highScore_Acromathics;
    
    [SerializeField] CloudSaveManager cloudSaveManager;
    [SerializeField] GameObject playerIDText;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_InputField sensInput;
    [SerializeField] Slider sensSlider;
    [SerializeField] Toggle sprintToggle;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad (menu);
    }


    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene") AdjustSensWithSlider();
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            playerIDText.GetComponent<TextMeshProUGUI>().text = "Player ID: " + cloudSaveManager.GetPlayerID();
        }

        if (Input.GetKeyDown(KeyCode.M) && SceneManager.GetActiveScene().name != "TitleScene")
        {
            menu.SetActive(!menu.activeSelf);
            
            if(menu.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerCam>().SetCanTurn(!Cursor.visible);

        }
    }

    public void SetHighScore(int scene, int score)
    {
        if(scene == 2)
        {
            if(highScore_AimematchicsI < score)
            {
                highScore_AimematchicsI = score;
                cloudSaveManager.StoreData(scene, score);
            }
        }
        if (scene == 3)
        {
            if (highScore_AimematchicsII < score)
            {
                highScore_AimematchicsII = score;
                cloudSaveManager.StoreData(scene, score);
            }
        }
        if (scene == 5)
        {
            if (highScore_Acromathics < score)
            {
                highScore_Acromathics = score;
                cloudSaveManager.StoreData(scene, score);
            }
        }
        Debug.Log("Aimemathics I High Score: " + highScore_AimematchicsI);
        Debug.Log("Aimemathics II High Score: " + highScore_AimematchicsII);
    }

    public void SetHighScore(int hsA1, int hsA2, int hsAc, int p, int x)
    {
        highScore_AimematchicsI = hsA1;
        highScore_AimematchicsII = hsA2;
        highScore_Acromathics = hsAc;
    }

    public void AdjustSensWithSlider()
    {
        if(SceneManager.GetActiveScene().name != "TitleScene") GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerCam>().SetSens(sensSlider.value);
        sensInput.text = sensSlider.value.ToString("#.##");
    }

    public void AdjustSensWithInput()
    {
        try
        {
            float sens = float.Parse(sensInput.text);
            GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerCam>().SetSens(sens);
            sensSlider.value = sens;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            GameObject.Find("Player").transform.GetChild(0).GetComponent<PlayerCam>().SetSens(sensSlider.value);
            sensInput.text = sensSlider.value.ToString("#.##");
        }
    }

    public void ToggleSprint()
    {
        GameObject.Find("Player").GetComponent<PlayerControl>().SetSprintToggle(sprintToggle.isOn);
    }

}
