using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.1f, 10.0f)] [SerializeField] float sensX, sensY;

    [SerializeField] Transform orientation;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject sphereBehaviorManager;
    private float xRotation, yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * 100;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * 100;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        Vector3 targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f);

        targetPos = Camera.main.ScreenToWorldPoint(targetPos);

        crosshair.transform.position = targetPos;

        crosshair.transform.LookAt(transform);

        if (Input.GetMouseButtonDown(0))
        {
            Ray shot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(shot, out hit, 60);
            if (hit.collider != null && hit.collider.name == "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex()) 
            {
                sphereBehaviorManager.GetComponent<SphereBehavior>().Randomize();
            }
            else if(hit.collider != null && hit.collider.name != "Sphere_" + sphereBehaviorManager.GetComponent<SphereBehavior>().GetCorrectIndex() && hit.collider.name.Contains("Sphere"))
            {
                hit.collider.gameObject.SetActive(false);
            }
            Debug.DrawRay(shot.origin, shot.direction * 60, Color.yellow, 2f);
        }
    }
}
