using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] Transform orientation;
    [SerializeField] float jumpForce;
    private float horizontalInput, verticalInput;

    private float movementSpeed;
    private Vector3 moveDirection = Vector3.forward;
    private Rigidbody rb;
    private bool isGrounded;
    private float airMultiplier;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        airMultiplier = 1f;
        movementSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (isGrounded)
        {
            airMultiplier = 1f;
        }
        else
        {
            airMultiplier = 0.0f;
        }
        moveDirection = orientation.forward * verticalInput * airMultiplier + orientation.right * horizontalInput * airMultiplier;
        if (isGrounded && Input.GetAxisRaw("Jump") != 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (movementSpeed == walkSpeed)
            {
                movementSpeed = sprintSpeed;
            }
            else
            {
                movementSpeed = walkSpeed;
            }
        }
        if(transform.position.y < -10)
        {
            transform.position = Vector3.zero + Vector3.up * 3;
        }
    }

    void LateUpdate()
    {
        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, movementSpeed);
        if (SceneManager.GetActiveScene().name == "Aimemathics I Scene" || SceneManager.GetActiveScene().name == "Aimemathics II Scene")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Portal_Aimemathics_I")
        {
            SceneManager.LoadScene("Aimemathics I Scene");
        }
        if(other.gameObject.name == "Portal_Aimemathics_II")
        {
            SceneManager.LoadScene("Aimemathics II Scene");
        }
        if (other.gameObject.name == "Portal_Acromathics")
        {
            SceneManager.LoadScene("Acromathics Scene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        Debug.Log("Exited Ground");
    }

}
