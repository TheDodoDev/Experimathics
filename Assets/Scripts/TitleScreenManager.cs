using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject nameInput, passwordInput, codeInput, studentButton, teacherButton, backButton, loginButton, registerButton;

    private const int STUDENT = 0;
    private const int TEACHER = 1;
    private int mode = -1;

    void Start()
    {
        
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
        }
    }

    public void SetMode(int mode)
    {
        this.mode = mode;
    }

    public void CreateUser()
    {
        
    }
}
