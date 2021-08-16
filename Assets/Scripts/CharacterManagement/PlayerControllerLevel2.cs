using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerControllerLevel2 : MonoBehaviour
{
    public float speed = 2f;

    Rigidbody rb;
    UIManager uI;
    public Animator DemonAnimLvl2;
    private Animator animator;
    public Animator DemonAnim;
    Transform doorTransform;
    private bool isTouchingDoor = false;
    [System.NonSerialized]public bool paranormalEvent = false;

    public PsychodelicEffect manicEffect;
    public CameraShake camShake;
    public GameManager gameManager;
    public GameObject candle;

    private bool canMove = true;
    private bool manicEffectIsOver = false;
    public GameObject lightWall;

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
    private bool hasTookLantern = false;


    private void Start()
    {
        DemonAnim = DemonAnim.GetComponent<Animator>();
        DemonAnimLvl2 = DemonAnimLvl2.GetComponent<Animator>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        uI = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        //Debug.Log(isTouchingDoor);
        if (doorTransform != null && isTouchingDoor)
        {
            doorTransform.Rotate(Vector3.up, 1f);
        }

        if(lastTeddyAction && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(GetLastTeddy());
        }

        if(Input.GetKey(KeyCode.E))
        {
            isPressingE = true;
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            isPressingE = false;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            if (!uI.onDialogue && canMove)
            {
                Movement();
                animator.SetBool("isWalking", true);
            }
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
            AudioManager.instance.Play("DoorBell");
        }

        if(collision.gameObject.CompareTag("LightWall") && hasTookLantern)
        {
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.CompareTag("LightWall") && !hasTookLantern)
        {
            Debug.Log("It's too dark to go there.");
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
            lightWall.SetActive(true);
            DemonAnim.enabled = true;
            other.gameObject.GetComponent<Renderer>().sharedMaterial = teddyMaterials[1];
            manicEffect.enabled = true;
            camShake.enabled = true;
            paranormalEvent = true;
            
            for(int i=0; i<lights.Length; i++)
            {
                lights[i].enabled = true;
            }

            if(teddys[teddyIndex].GetComponent<TeddyScript>().isFirstTeddy)
            {
                //canMove = false;
                animator.SetBool("isWalking", false);
                teddys[teddyIndex].GetComponent<TeddyScript>().isFirstTeddy = false;
            }

            else if(teddys[teddyIndex].GetComponent<TeddyScript>().isLastTeddy)
            {
                lastTeddyAction = true;
            }
            AudioManager.instance.Play("DemonDoorClip");

            if(!teddys[teddyIndex].GetComponent<TeddyScript>().isLastTeddy)
                StartCoroutine(SpawnAnotherTeddy());
        }

        if (other.gameObject.tag.Equals("Trigger"))
        {
            if (!uI.trigger)
                uI.trigger = true;
            other.gameObject.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableLanternLevel2"))
        {
            if (isPressingE)
            {
                other.gameObject.SetActive(false);
                animator.SetBool("isCandleTrue", true);
                candle.SetActive(true);
                hasTookLantern = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Teddy"))
        {
            StopCoroutine(SpawnAnotherTeddy());
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
        canMove = true;
        manicEffectIsOver = true;
    }

    IEnumerator SpawnAnotherTeddy()
    {
        yield return new WaitForSeconds(3.5f);
        teddys[teddyIndex].GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(0.5f);
        teddys[teddyIndex].SetActive(false);
        teddyIndex++;
        if(teddyIndex != teddys.Length)
            teddys[teddyIndex].SetActive(true);
    }

    IEnumerator GetLastTeddy()
    {
        hasFadeOut = true;
        teddys[teddyIndex].GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(0.5f);
        teddys[teddyIndex].SetActive(false);
        animator.SetBool("isFading", true);
        DemonAnimLvl2.enabled = true;
        yield return new WaitForSeconds(1f);
        darkCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        darkCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isFading", false);
        lastTeddyAction = false;
    }

}
