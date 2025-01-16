using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.1f, 10.0f)] [SerializeField] float sensX, sensY;

    [SerializeField] Transform orientation;
    [SerializeField] GameObject sphereBehaviorManager;
    [SerializeField] GameObject numPadManager;
    [SerializeField] AcromathicsManager acromathicsManager;
    [SerializeField] Text scoreText, accuracyText, timerText;
    [SerializeField] TextMeshProUGUI sensInput;
    [SerializeField] Slider sensSlider;
    private GameObject playerData;

    private float xRotation, yRotation;
    private int score;
    private float shotsFired, shotsHit;
    private bool canTurn;
    void Start()
    {
        playerData = GameObject.Find("PlayerData");
        score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (SceneManager.GetActiveScene().name != "LobbyScene" && SceneManager.GetActiveScene().name != "TitleScene") StartCoroutine(Timer());
        canTurn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "LobbyScene")
        {
            scoreText.text = "Score: " + score;
        }
        if (shotsFired > 0)
        {
            
            accuracyText.text = "Accuracy: " + Math.Round((shotsHit * 100f / shotsFired),2);
            Debug.Log(shotsHit + "/" + shotsFired);
        }
        if(canTurn)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * 100;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * 100;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        

        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Aimemathics I Scene")
        {
            shotsFired += 1.0f;
            Ray shot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(shot, out hit, 60);
            if (hit.collider != null && hit.collider.name == "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex()) 
            {
                shotsHit++;
                score++;
                scoreText.text = "Score: " + score;
                sphereBehaviorManager.GetComponent<SphereBehavior>().Randomize();
            }
            else if(hit.collider != null && hit.collider.name != "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex() && hit.collider.name.Contains("Sphere"))
            {
                score--;
                scoreText.text = "Score: " + score;
                hit.collider.gameObject.SetActive(false);
            }
        }
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Aimemathics II Scene")
        {
            shotsFired += 1.0f;
            Ray shot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(shot, out hit, 60);
            if (hit.collider != null && hit.collider.name.Contains("NumPad_") && hit.collider.name.Length == 8)
            {
                shotsHit++;
                numPadManager.GetComponent<NumPadManager>().AddDigit(hit.collider.name[^1] - '0');
                
            }
            if(hit.collider != null && hit.collider.name.Substring(7).Equals("Enter"))
            {
                shotsHit++;
                bool correct = numPadManager.GetComponent<NumPadManager>().Verify();
                if (correct)
                {
                    numPadManager.GetComponent<NumPadManager>().Randomize();
                    score++;
                }
                else
                {
                    score--;
                }
            }
            if (hit.collider != null && hit.collider.name.Substring(7).Equals("Back"))
            {
                shotsHit++;
                numPadManager.GetComponent<NumPadManager>().RemoveDigit();
            }
        }
    }

    IEnumerator Timer()
    {
        int time = 60;
        if (SceneManager.GetActiveScene().name == "Acromathics Scene")
        {
            time = 120;
        }
        timerText.text = "Time Left: " + time;
        Debug.Log("Timer Started");
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            timerText.text = "Time Left: " + (--time);
            if (transform.parent.position.y < -10)
            {
                time = 0;
            }
        }
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        playerData.GetComponent<PlayerData>().SetHighScore(SceneManager.GetActiveScene().buildIndex, score);
        SceneManager.LoadScene("LobbyScene");
    }

    public void SetSens(float sens)
    {
        sensX = sens;
        sensY = sens;
    }

    public void SetCanTurn(bool b)
    {
        canTurn = b;
    }

    public void IncrementScore()
    {
        score++;
    }
}
