using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float drag = 5f;
    public float slideSpeed = 10f;
    public float slideDuration = 0.5f;
    public float slideThreshold = 0.1f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSliding = false;
    private float slideTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }  

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (!isSliding) { 
            characterController.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && move.magnitude > slideThreshold){
            StartSlide(move);
        }

        if(isSliding){
            SlideMovement();
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (isGrounded && !isSliding && velocity.magnitude > 0.1f){
            velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.deltaTime);
            velocity.z = Mathf.Lerp(velocity.z, 0, drag * Time.deltaTime);
        }
    }

    void StartSlide (Vector3 moveDirection){
        isSliding = true;
        slideTimer = slideDuration;
        velocity = moveDirection * slideSpeed;
    }

    void SlideMovement (){
        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0){
            isSliding = false;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}
