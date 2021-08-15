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
    public GameManager gameManager;

    public LightFlickering[] lights;

    public GameObject lantern;

    public Material[] teddyMaterials;
    public GameObject[] teddys;

    private bool firstTime = true;
    public GameObject darkCanvas;

    int teddyIndex = 0;

    private bool isPressingE = false;
    private bool hasFadeOut = false;
    private bool lastTeddyAction = false;

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
<<<<<<< HEAD
        //Debug.Log(isTouchingDoor);
=======
>>>>>>> 561bf818c92182659f3ba2b6dadb6d6767326389
        if (doorTransform != null && isTouchingDoor)
        {
            doorTransform.Rotate(Vector3.up, 1f);
        }

        if(lastTeddyAction && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(GetLastTeddy());
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
        }else if(collision.gameObject.CompareTag("Soul")){
            gameManager.IncreaseSoul();
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

            if(teddys[teddyIndex].GetComponent<TeddyScript>().isLastTeddy)
            {
                lastTeddyAction = true;
            }

        }

        StartCoroutine(SpawnAnotherTeddy());

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

    IEnumerator SpawnAnotherTeddy()
    {
        if(teddyIndex+1 != teddys.Length)
        {
            yield return new WaitForSeconds(3.5f);
            teddys[teddyIndex].GetComponent<Rigidbody>().detectCollisions = false;
            yield return new WaitForSeconds(0.5f);
            teddys[teddyIndex].SetActive(false);
            teddyIndex++;     
            teddys[teddyIndex].SetActive(true);
        }
        
    }

    IEnumerator GetLastTeddy()
    {
        hasFadeOut = true;
        teddys[teddyIndex].GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(0.5f);
        teddys[teddyIndex].SetActive(false);
        animator.SetBool("isFading", true);
        yield return new WaitForSeconds(1f);
        darkCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        darkCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isFading", false);
    }

}
