using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.CloudSave.Models.Data.Player;
using Unity.Services.CloudSave;
using UnityEngine;

public class TeacherViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string list;
    private string curFirst = "";
    private string curLast = "";
    CloudSaveManager cloudSaveManager;
    [SerializeField] GameObject addStudentInput;
    [SerializeField] GameObject button;
    [SerializeField] GameObject canvas;
    async void Start()
    {
        cloudSaveManager = GameObject.Find("CloudSaveManager").GetComponent<CloudSaveManager>();
        UpdateView();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdateView(); 
        }
    }

    public void SetStudentList(string studentList)
    {
        this.list = studentList;
        Debug.Log(list);
    }

    public void AddStudent()
    {
        string id = addStudentInput.GetComponent<TextMeshProUGUI>().text;
        id = id.Substring(0, id.Length - 1);
        cloudSaveManager.AddStudent(id);
        addStudentInput.GetComponent<TextMeshProUGUI>().text = "";
        UpdateView();
    }

    public async void UpdateView()
    {
        await cloudSaveManager.GetStudents();
        string[] students = list.Split(' ');
        Debug.Log(list);
        foreach(string student in students)
        {
            Debug.Log(student);
        }
        int row = 0;
        int col = 0;
        foreach (string student in students)
        {
            GameObject o = Instantiate(button);
            o.transform.SetParent(canvas.transform, false);
            o.transform.localPosition = new Vector3(col * 350 - 780, 330 - 150 * row, 0);
            cloudSaveManager.GetCurrentStudent(student);
            o.transform.GetComponentInChildren<TextMeshProUGUI>().text = curFirst + " " + curLast;
            if (col == 5)
            {
                col = 0;
                row++;
            }
            else
            {
                col++;
            }
        }
    }

    public void SetName(string curFirst, string curLast)
    {
        this.curFirst = curFirst;
        this.curLast = curLast;
    }
}
