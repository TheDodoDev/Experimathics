using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject nameInput, passwordInput, codeInput, studentButton, teacherButton, backButton, 
        loginButton, registerButton, nameInputText, passwordInputText, codeInputText, keyInput, keyInputText;

    private const int STUDENT = 0;
    private const int TEACHER = 1;
    private int mode = -1;

    private CloudSaveManager cloudSaveManager;
    void Start()
    {
        cloudSaveManager = GameObject.Find("CloudSaveManager").GetComponent<CloudSaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == STUDENT)
        {
            studentButton.SetActive(false);
            teacherButton.SetActive(false);
            nameInput.SetActive(true);
            passwordInput.SetActive(true);
            codeInput.SetActive(true);
            backButton.SetActive(true);
            loginButton.SetActive(true);
            registerButton.SetActive(true);
            keyInput.SetActive(true);
        }
        if (mode == TEACHER)
        {
            studentButton.SetActive(false);
            teacherButton.SetActive(false);
            nameInput.SetActive(true);
            passwordInput.SetActive(true);
            backButton.SetActive(true);
            loginButton.SetActive(true);
            registerButton.SetActive(true);
            keyInput.SetActive(true);
        }
        if(mode == -1)
        {
            studentButton.SetActive(true);
            teacherButton.SetActive(true);
            nameInput.SetActive(false);
            passwordInput.SetActive(false);
            codeInput.SetActive(false);
            backButton.SetActive(false);
            loginButton.SetActive(false);
            registerButton.SetActive(false);
            keyInput.SetActive(false);
        }
    }

    public void SetMode(int mode)
    {
        this.mode = mode;
    }

    public void CreateUser()
    {
        string username = nameInputText.GetComponent<TextMeshProUGUI>().text;
        string password = passwordInputText.GetComponent<TextMeshProUGUI>().text;
        string classcode = codeInputText.GetComponent<TextMeshProUGUI>().text;
        if(username.Length > 1 && password.Length > 1 && classcode.Length > 1)
            cloudSaveManager.CreateAccount(mode, username.Substring(0, username.Length - 1), password.Substring(0, password.Length - 1), classcode.Substring(0, classcode.Length - 1));
    }

    public void LoadUserData()
    {
        string username = nameInputText.GetComponent<TextMeshProUGUI>().text;
        string password = passwordInputText.GetComponent<TextMeshProUGUI>().text;
        string key = keyInputText.GetComponent<TextMeshProUGUI>().text;
        if(username.Length > 1 && password.Length > 1 && key.Length > 1)
            cloudSaveManager.LoadData(username.Substring(0, username.Length - 1), password.Substring(0, password.Length - 1), key.Substring(0, key.Length - 1));
    }

    public void Login()
    {
        if (mode == STUDENT)
        {
            if (CheckIfStringsEqual(nameInputText.GetComponent<TextMeshProUGUI>().text.ToString(), "S") && CheckIfStringsEqual(passwordInputText.GetComponent<TextMeshProUGUI>().text.ToString(), "1"))
            {

                SceneManager.LoadScene("LobbyScene");
            }
        }
    }

    public bool CheckIfStringsEqual(string s1, string s2)
    {
        Debug.Log(s1[^1]);
        if (s1.Length - 1 == s2.Length)
        {
            for (int i = 0; i < s1.Length - 1; i++)
            {
                Debug.Log(s1[i] + " " + s2[i]);
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
