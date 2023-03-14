using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform cam;

    [Header("Settings")]
    public float health;
    public float stamina;
    public float walkSpeed;
    public float sprintSpeed;
    public float airMultiplier;
    public float jumpHeight;
    public float groundDrag;

    private float horizontalInput;
    private float verticalInput;
    private bool isSprinting;
    float turnSmoothVelocity;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    private bool isGrounded;

    private void Start()
    {
        //Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Check if player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        inputListener();
        speedController();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        movement();
    }

    private void movement()
    {
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //Calculate orientation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.2f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Calculate movement direction
            if (!isGrounded)
            {
                Debug.Log("Jumping");
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f * airMultiplier, ForceMode.Force);

            } else if (isSprinting)
            {
                Debug.Log("Sprinting");
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force);
            } else
            {
                Debug.Log("Walking");
                rb.AddForce(moveDirection.normalized * walkSpeed * 10f, ForceMode.Force);
            }
        }
    }

    private void speedController()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limit velocity if exceeded
        if (isSprinting)
        {
            if (flatVelocity.magnitude > sprintSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * sprintSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        } else
        {
            if (flatVelocity.magnitude > walkSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * walkSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
    }

    private void inputListener()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump") && isGrounded)
        {
            //Reset the Y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        } else
        {
            isSprinting = false;
        }
    }

    private void staminaController()
    {
        if(stamina <= 0)
        {
            //Do something
        }
    }

    public void setStamina(float amount)
    {
        stamina = amount;
    }

    private void reduceStamina(float amount)
    {
        stamina -= amount;
    }


    private void healthController()
    {
        if (health <= 0)
        {
            //Do something
        }
    }

    public void setHealth(float amount)
    {
        health = amount;
    }

    private void reduceHealth(float amount)
    {
        health -= amount;
    }
}
