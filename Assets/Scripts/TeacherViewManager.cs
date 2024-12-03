using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeacherViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string list = "";
    CloudSaveManager cloudSaveManager;
    [SerializeField] GameObject addStudentInput;
    [SerializeField] GameObject button;
    void Start()
    {
        cloudSaveManager = GameObject.Find("CloudSaveManager").GetComponent<CloudSaveManager>();
        cloudSaveManager.GetStudents();
        string[] students = list.Split(',');
        foreach (string student in students)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStudentList(string studentList)
    {
        this.list = studentList;
    }

    public void AddStudent()
    {
        string id = addStudentInput.GetComponent<TextMeshProUGUI>().text;
        cloudSaveManager.AddStudent(id);
        cloudSaveManager.GetStudents();
    }
}
