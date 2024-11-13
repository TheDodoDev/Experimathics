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
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }
        CheckIfOnGround();
    }

    void LateUpdate()
    {
        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, movementSpeed);
        if (SceneManager.GetActiveScene().name == "Aimemathics I Scene")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    void CheckIfOnGround()
    {

        RaycastHit hit = new RaycastHit();

        Physics.Raycast(transform.position + Vector3.down, Vector3.down, out hit, 0.1f);

        Debug.DrawRay(transform.position + Vector3.down, Vector3.down * 0.1f, Color.green, 0.1f, false);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            isGrounded = true;
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
    }

}
