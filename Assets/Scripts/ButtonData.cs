using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonData : MonoBehaviour
{
    // Start is called before the first frame update

    string studentID;
    string name;
    int hsA1, hsA2;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject statsText;
    private GameObject[] studentButtons;
    void Start()
    {
        backButton = GameObject.Find("Canvas").transform.Find("Back Button").gameObject;
        statsText = GameObject.Find("Canvas").transform.Find("Stats Text").gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetStudentID() { return studentID; }
    public string GetName() { return name; }
    public int GetHSA1() { return hsA1; }
    public int GetHSA2() { return hsA2; }

    public void SetStudentID(string studentID){ this.studentID = studentID; }
    public void SetName(string name) { this.name = name; }
    public void SetHSA1(int hsA1) { this.hsA1 = hsA1; }
    public void SetHSA2(int hsA2) { this.hsA2 = hsA2; }
    public void StudentSelected()
    {
        studentButtons = GameObject.FindGameObjectsWithTag("Student Button");
        foreach (GameObject button in studentButtons)
        {
            button.SetActive(false);
        }
        backButton.SetActive(true);
        statsText.SetActive(true);
        statsText.GetComponent<TextMeshProUGUI>().text = "Aimemathics I High Score: " + hsA1 + "\nAimemathics II High Score: " + hsA2;
    }
    public void ResetView()
    {
        foreach (GameObject button in studentButtons)
        {
            button.SetActive(true);
        }
    }
}
