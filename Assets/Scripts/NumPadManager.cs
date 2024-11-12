using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumPadManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> squares = new List<GameObject>();
    [SerializeField] List<GameObject> texts = new List<GameObject>();
    [SerializeField] GameObject problemText;
    private int answer;
    private String sol, problem;
    private bool[,] grid;
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        problemText.GetComponent<TextMeshProUGUI>().text = problem + sol;
    }

    public void Randomize()
    {
        //Randomizing Problem
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
            operand2 = factors[UnityEngine.Random.Range(0, factors.Count - 1)];
            target = operand1 / operand2;
            op = " / ";
        }
        answer = target;
        problemText.GetComponent<TextMeshProUGUI>().text = operand1 + op + operand2 + " = ";
        problem = problemText.GetComponent<TextMeshProUGUI>().text;
        problemText.GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255);

        //Randomizing Squares
        grid = new bool[11, 11];

        for (int i = 0; i < squares.Count; i++)
        {
            int row = UnityEngine.Random.Range(0, 11);
            int col = UnityEngine.Random.Range(0, 11);
            while (true)
            {
                if (!grid[row, col])
                {
                    grid[row, col] = true;
                    squares[i].transform.position = new Vector3(row * 4 - 20, col * 4 - 20, 49.5f);
                    texts[i].transform.position = squares[i].transform.position - squares[i].transform.forward * 0.1f;
                    break;
                }
                else
                {
                    row = UnityEngine.Random.Range(0, 11);
                    col = UnityEngine.Random.Range(0, 11);
                }

            }
        }
    }

    public int GetCorrectAnswer()
    {
        return answer;
    }

    public void MoveSquare(int x)
    {
        int row = UnityEngine.Random.Range(0, 11);
        int col = UnityEngine.Random.Range(0, 11);
        while (true)
        {
            if (!grid[row, col])
            {
                grid[row, col] = true;
                squares[x].transform.position = new Vector3(row * 4 - 20, col * 4 - 20, 49.5f);
                texts[x].transform.position = squares[x].transform.position - squares[x].transform.forward * 0.1f;
                break;
            }
            else
            {
                row = UnityEngine.Random.Range(0, 11);
                col = UnityEngine.Random.Range(0, 11);
            }

        }
    }

    public void AddDigit(int a)
    {
        sol += a;
        MoveSquare(a);
    }

    public bool Verify()
    {
        int ans = 0;
        int mult = 1;
        for (int i = sol.Length - 1; i >= 0; i--)
        {
            ans += (sol[i] - '0') * mult;
            mult *= 10;
        }
        sol = "";
        if (ans == answer)
        {
            return true;
        }
        return false;
    }

    public void RemoveDigit()
    {
        if (sol.Length > 1)
        {
            sol = sol.Substring(0, sol.Length - 1);
        }
        else if(sol.Length == 1)
        {
            sol = "";
        }
    }
}
