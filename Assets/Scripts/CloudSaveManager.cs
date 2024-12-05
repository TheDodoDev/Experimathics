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
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.Net.Http;
public class CloudSaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerData playerDataScript;
    TeacherViewManager teacherViewManagerScript;
    private bool found = false;
    private async void Awake()
    {
        found = false;
        await UnityServices.InitializeAsync();
    }

    // Update is called once per frame
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if(SceneManager.GetActiveScene().name == "TeacherViewScene" && !found)
        {
            teacherViewManagerScript = GameObject.Find("TeacherViewManager").GetComponent<TeacherViewManager>();
            found = true;
        }
    }
    public async void CreateAccount(int mode, string username, string password, string firstName, string lastName)
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
        Debug.Log(username + " " + password);
        await SignUpWithUsernamePasswordAsync(username, password);
        var data = new Dictionary<string, object> { { "accounttype", accountType }, { "firstName", firstName }, { "lastName", lastName }, { "hsA1", 0 }, { "hsA2", 0 } };

        await CloudSaveService.Instance.Data.Player.SaveAsync(data, new Unity.Services.CloudSave.Models.Data.Player.SaveOptions(new PublicWriteAccessClassOptions()));

        if (CheckIfStringsEqual(accountType, "STUDENT"))
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else if(CheckIfStringsEqual(accountType, "TEACHER"))
        {
            SceneManager.LoadScene("TeacherViewScene");
        }

    }

    public async void LoadData(string username, string password)
    {
        await SignInWithUsernamePasswordAsync(username, password);

        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "hsA1", "hsA2", "accounttype" }, new LoadOptions(new PublicReadAccessClassOptions()));

        int hsA1 = 0;
        int hsA2 = 0;
        string accountType = "";
        if (playerData.TryGetValue("hsA1", out var firstkeyName))
        {
            hsA1 = firstkeyName.Value.GetAs<int>();
        }
        if (playerData.TryGetValue("hsA2", out var secondkeyName))
        {
            hsA2 = secondkeyName.Value.GetAs<int>();
        }
        if (playerData.TryGetValue("accounttype", out var thirdkeyName))
        {
            accountType = thirdkeyName.Value.GetAs<string>();
        }
        playerDataScript.SetHighScore(hsA1, hsA2, 0, 0);
        Debug.Log(accountType);
        if (CheckIfStringsEqual(accountType, "STUDENT"))
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else if (CheckIfStringsEqual(accountType, "TEACHER"))
        {
            SceneManager.LoadScene("TeacherViewScene");
        }

    }

    //Below Code Taken From Documentation from https://docs.unity.com/ugs/manual/authentication/manual/platform-signin-username-password

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public async void StoreData(int scene, int score)
    {
        if (scene == 2)
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "hsA1", score} }, new Unity.Services.CloudSave.Models.Data.Player.SaveOptions(new PublicWriteAccessClassOptions()));
        }
        if (scene == 3)
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "hsA2", score } }, new Unity.Services.CloudSave.Models.Data.Player.SaveOptions(new PublicWriteAccessClassOptions()));
        }
    }

    public async void AddStudent(string id)
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "students" }, new LoadOptions(new PublicReadAccessClassOptions()));
        string list = "";
        if (playerData.TryGetValue("students", out var firstkeyName))
        {
            list = firstkeyName.Value.GetAs<string>();
            Debug.Log($"Aimemathics I High Score: {firstkeyName.Value.GetAs<string>()}");
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "students", list + "," + id } }, new Unity.Services.CloudSave.Models.Data.Player.SaveOptions(new PublicWriteAccessClassOptions()));

        }
        else
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { "students", id } }, new Unity.Services.CloudSave.Models.Data.Player.SaveOptions(new PublicWriteAccessClassOptions()));
        }
    }

    public async Task GetStudents()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "students" }, new LoadOptions(new PublicReadAccessClassOptions()));
        string list = "";
        if (playerData.TryGetValue("students", out var firstkeyName))
        {
            list = firstkeyName.Value.GetAs<string>();
        }
        teacherViewManagerScript.SetStudentList(list);
        Debug.Log(list);
    }

    public async void GetCurrentStudent(string id)
    {
        Debug.Log(id);
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "firstName", "lastName" }, new LoadOptions(new PublicReadAccessClassOptions("jNu73eVXMhfGFdQuhvQ9okTVb8AU")));
        string firstName = "";
        string lastName = "";
        if (playerData.TryGetValue("firstName", out var keyName))
        {
            firstName = keyName.Value.GetAs<string>();
        }
        if (playerData.TryGetValue("lastName", out var keyName1))
        {
            lastName = keyName1.Value.GetAs<string>();
        }
        Debug.Log(firstName + " " + lastName);
        teacherViewManagerScript.SetName(firstName, lastName);
    }
    public bool CheckIfStringsEqual(string s1, string s2)
    {
        if (s1.Length == s2.Length)
        {
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
