using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.1f, 10.0f)] [SerializeField] float sensX, sensY;

    [SerializeField] Transform orientation;
    [SerializeField] GameObject sphereBehaviorManager;
    [SerializeField] Text scoreText, accuracyText;
    private float xRotation, yRotation;
    private int score;
    private float shotsFired, shotsHit;
    void Start()
    {
        score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (shotsFired > 0)
        {
            
            accuracyText.text = "Accuracy: " + Math.Round((shotsHit * 100f / shotsFired),2);
            Debug.Log(shotsHit + "/" + shotsFired);
        }
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * 100;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * 100;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetMouseButtonDown(0))
        {
            shotsFired += 1.0f;
            Ray shot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(shot, out hit, 60);
            if (hit.collider != null && hit.collider.name == "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex()) 
            {
                shotsHit++;
                score++;
                sphereBehaviorManager.GetComponent<SphereBehavior>().Randomize();
            }
            else if(hit.collider != null && hit.collider.name != "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex() && hit.collider.name.Contains("Sphere"))
            {
                score--;
                hit.collider.gameObject.SetActive(false);
            }
            Debug.DrawRay(shot.origin, shot.direction * 60, Color.yellow, 2f);
        }
    }
}
