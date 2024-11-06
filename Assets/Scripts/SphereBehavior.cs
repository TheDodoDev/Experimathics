using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> spheres = new List<GameObject>();
    [SerializeField] List<GameObject> texts = new List<GameObject>();
    [SerializeField] int[] numbers = new int[4];
    [SerializeField] GameObject problemText;

    private int correctIndex = 0;
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            if(spheres[i] != null && !spheres[i].activeSelf)
            {
                problemText.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
    }

    public void Randomize()
    {
        int operation = UnityEngine.Random.Range(0, 4);
        int operand1 = UnityEngine.Random.Range(0, 16);
        int operand2 = UnityEngine.Random.Range(0, 16);
        int target = 0;
        String op = "";
        if (operation == 0)
        {
            target = operand1 + operand2;
            op = " + ";
        }
        if (operation == 1)
        {
            int temp = 0;
            if(operand1 < operand2)
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
            for(int i = 1; i * i <= operand1; i++)
            {
                if(operand1 % i == 0)
                {
                    factors.Add(i);
                    factors.Add(operand1 / i);
                }
            }
            operand2 = factors[UnityEngine.Random.Range(0, factors.Count - 1)];
            target = operand1 / operand2;
            op = " / ";
        }
        int index = 0;
        int targetIndex = UnityEngine.Random.Range(0, 4);
        foreach (GameObject sphere in spheres)
        {
            sphere.SetActive(true);
            sphere.transform.position = new Vector3(UnityEngine.Random.Range(-7, 9) * 2, UnityEngine.Random.Range(2, 10) * 2, 10f);
            texts[index].transform.position = sphere.transform.position + Vector3.up * 1.25f;
            numbers[index] = UnityEngine.Random.Range(0, target + 10);
            while (numbers[index].Equals(target))
            {
                numbers[index] = UnityEngine.Random.Range(0, target + 10);
            }
            if (index == targetIndex)
            {
                numbers[index] = target;
            }
            texts[index].GetComponent<TextMeshProUGUI>().text = numbers[index].ToString();
            index++;
        }
        problemText.GetComponent<TextMeshProUGUI>().text = operand1 + op + operand2;
        problemText.GetComponent<TextMeshProUGUI>().color = new Color(0, 255, 255);
        correctIndex = targetIndex + 1;
    }

    public int GetCorrectIndex()
    {
        return correctIndex; 
    }
}
