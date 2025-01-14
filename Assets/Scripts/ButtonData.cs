using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] string studentID;
    [SerializeField] string name;
    [SerializeField] int hsA1, hsA2, hsAc;
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
    public void SetHSAC(int hsAc) { this.hsAc = hsAc; }
    public void StudentSelected()
    {
        studentButtons = GameObject.FindGameObjectsWithTag("Student Button");
        foreach (GameObject button in studentButtons)
        {
            button.GetComponent<Image>().enabled = false;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            button.GetComponent<Button>().enabled = false;
            Debug.Log(button.GetComponent<ButtonData>().GetName());
        }
        backButton.SetActive(true);
        statsText.SetActive(true);
        statsText.GetComponent<TextMeshProUGUI>().text = "Aimemathics I High Score: " + hsA1 + "\nAimemathics II High Score: " + hsA2 + "\nAcromathics High Score: " + hsAc;
        Debug.Log(studentID);
    }
    public void ResetView()
    {
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Student Button"))
        {
            button.GetComponent<Image>().enabled = true;
            button.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            button.GetComponent<Button>().enabled = true;

        }
    }
}
