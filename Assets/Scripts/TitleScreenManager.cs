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
    [SerializeField]
    GameObject nameInput, passwordInput, firstNameInput, lastNameInput, studentButton, teacherButton, backButton,
        loginButton, registerButton, nameInputText, passwordInputText, firstNameInputText, lastNameInputText;

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
            backButton.SetActive(true);
            loginButton.SetActive(true);
            registerButton.SetActive(true);
            firstNameInput.SetActive(true);
            lastNameInput.SetActive(true);
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
            firstNameInput.SetActive(true);
            lastNameInput.SetActive(true);
        }
        if(mode == -1)
        {
            studentButton.SetActive(true);
            teacherButton.SetActive(true);
            nameInput.SetActive(false);
            passwordInput.SetActive(false);
            backButton.SetActive(false);
            loginButton.SetActive(false);
            registerButton.SetActive(false);
            firstNameInput.SetActive(false);
            lastNameInput.SetActive(false);
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
        string firstName = firstNameInputText.GetComponent<TextMeshProUGUI>().text;
        string lastName = lastNameInputText.GetComponent<TextMeshProUGUI>().text;
        if(username.Length > 1 && password.Length > 1 && firstName.Length > 1 && lastName.Length > 1)
            cloudSaveManager.CreateAccount(mode, username.Substring(0, username.Length - 1), password.Substring(0, password.Length - 1), firstName.Substring(0, firstName.Length - 1), lastName.Substring(0, lastName.Length - 1));
    }

    public void LoadUserData()
    {
        string username = nameInputText.GetComponent<TextMeshProUGUI>().text;
        string password = passwordInputText.GetComponent<TextMeshProUGUI>().text;
        if(username.Length > 1 && password.Length > 1) cloudSaveManager.LoadData(username.Substring(0, username.Length - 1), password.Substring(0, password.Length - 1));
    }

    public bool CheckIfStringsEqual(string s1, string s2)
    {
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
