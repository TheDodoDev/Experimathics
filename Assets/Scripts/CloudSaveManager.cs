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
    }

    // Update is called once per frame
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
