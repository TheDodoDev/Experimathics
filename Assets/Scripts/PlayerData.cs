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
public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    private int highScore_AimematchicsI, highScore_AimematchicsII, highScore_Acromathics;
    
    [SerializeField] CloudSaveManager cloudSaveManager;
    [SerializeField] GameObject playerIDText;
    [SerializeField] GameObject canvas;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad (canvas);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "LobbyScene") playerIDText.GetComponent<TextMeshProUGUI>().text = "Player ID: " + cloudSaveManager.GetPlayerID();
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
    
}
