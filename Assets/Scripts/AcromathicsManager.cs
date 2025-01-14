using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AcromathicsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject platform;
    [SerializeField] GameObject text;
    [SerializeField] List<GameObject> platforms = new List<GameObject>();
    [SerializeField] List<GameObject> texts = new List<GameObject>();
    [SerializeField] int[] numbers = new int[3];
    private int correctIndex = 0;
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Randomize()
    {
        int operation = UnityEngine.Random.Range(0, 4);
        int operand1 = UnityEngine.Random.Range(0, 16);
        int operand2 = UnityEngine.Random.Range(0, 16);
        int target = 0;
        string op = "";
        if (operation == 0)
        {
            target = operand1 + operand2;
            op = " + ";
        }
        if (operation == 1)
        {
            int temp = 0;
            if (operand1 < operand2)
            {
                temp = operand1;
                operand1 = operand2;
                operand2 = temp;
            }
            target = operand1 - operand2;
            op = " - ";
        }
        if (operation == 2)
        {
            target = operand1 * operand2;
            op = " * ";
        }
        if (operation == 3)
        {
            operand1 = UnityEngine.Random.Range(0, 225);
            List<int> factors = new List<int>();
            for (int i = 1; i * i <= operand1; i++)
            {
                if (operand1 % i == 0)
                {
                    factors.Add(i);
                    factors.Add(operand1 / i);
                }
            }
            operand2 = factors[UnityEngine.Random.Range(0, factors.Count)];
            target = operand1 / operand2;
            op = " / ";
        }

        int targetIndex = UnityEngine.Random.Range(0, 3);
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActive(true);
            platforms[i].transform.position = platform.transform.position;
            switch(i)
            {
                case 0: platforms[i].transform.position += new Vector3(10, 0, 0); break;
                case 1: platforms[i].transform.position += new Vector3(-10, 0, 0); break;
                case 2: platforms[i].transform.position += new Vector3(0, 0, 10); break;
            }
            texts[i].transform.position = platforms[i].transform.position + new Vector3(0, 4, 0);
            numbers[i] = UnityEngine.Random.Range(0, target + 10);
            while (numbers[i].Equals(target))
            {
                numbers[i] = UnityEngine.Random.Range(0, target + 10);
            }
            if (i == targetIndex)
            {
                numbers[i] = target;
                platforms[i].tag = "Correct";
            }
            else
            {
                platforms[i].tag = "Incorrect";
            }
            texts[i].GetComponent<TextMeshProUGUI>().text = numbers[i].ToString();            
            correctIndex = targetIndex;
            text.GetComponent<TextMeshProUGUI>().text = operand1 + op + operand2;
        }
    }

    public int GetCorrectIndex()
    {
        return correctIndex;
    }
}
