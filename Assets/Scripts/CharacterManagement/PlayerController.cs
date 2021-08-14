using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;

    Rigidbody rb;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Input.anyKey)
            Movement();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isFading", true);
        }
    }

    public void Movement()
    {
        animator.SetBool("isWalking", true);

        Vector3 currentPos = rb.position;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        inputVector = Vector3.ClampMagnitude(inputVector, 1);

        Vector3 movement = inputVector * speed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;

        rb.MovePosition(newPos);

        transform.rotation = Quaternion.LookRotation(inputVector);
    }
}
