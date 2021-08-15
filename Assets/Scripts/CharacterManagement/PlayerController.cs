using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;

    Rigidbody rb;

    private Animator animator;
    public Animator DemonAnim;
    Transform doorTransform;
    private bool isTouchingDoor = false;
    [System.NonSerialized]public bool paranormalEvent = false;

    public PsychodelicEffect manicEffect;
    public CameraShake camShake;

    public LightFlickering[] lights;

    public GameObject lantern;

    public Material[] teddyMaterials;

    private bool firstTime = true;
    public GameObject darkCanvas;
    private void Start()
    {
        DemonAnim = DemonAnim.GetComponent<Animator>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(isTouchingDoor);
        if (doorTransform != null && isTouchingDoor)
        {
            doorTransform.Rotate(Vector3.up, 1f);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            Movement();
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("isFading", true);
        //}
    }

    public void Movement()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            doorTransform = collision.gameObject.transform;
            isTouchingDoor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            doorTransform = null;
            isTouchingDoor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Teddy"))
        {
            DemonAnim.enabled = true;
            other.gameObject.GetComponent<Renderer>().sharedMaterial = teddyMaterials[1];
            manicEffect.enabled = true;
            camShake.enabled = true;
            paranormalEvent = true;
            
            for(int i=0; i<lights.Length; i++)
            {
                lights[i].enabled = true;
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Teddy"))
        {
            other.gameObject.GetComponent<Renderer>().sharedMaterial = teddyMaterials[0];

            manicEffect.enabled = false;
            camShake.enabled = false;
            paranormalEvent = false;

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;
                lights[i].gameObject.SetActive(false);
            }

            if(firstTime)
            {
                StartCoroutine(LetThereBeDark());
            }

            lantern.SetActive(true);
        }
    }

    IEnumerator LetThereBeDark()
    {
        darkCanvas.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        darkCanvas.SetActive(false);
        firstTime = false;
    }

}
