using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
public class CloudSaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Signed In");
    }

    // Update is called once per frame
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public async void CreateAccount(int mode, string username, string password, string classcode)
    {
        string accountType = "";
        if (mode == 0)
        {
            accountType = "STUDENT";
        }
        if (mode == 1)
        {
            accountType = "TEACHER";
        }
        var playerData = new Dictionary<string, object>{
          {"username", username},
          {"password", password},
          {"classcode", classcode},
          {"accounttype", accountType}
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
        Debug.Log($"Saved data {string.Join(',', playerData)}");
    }

    public async void LoadData(string username, string password, string playerID)
    {

    }
}
