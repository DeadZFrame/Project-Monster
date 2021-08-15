using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityBarScript : MonoBehaviour
{
    public GameObject player;
    Vector3 velocity = Vector3.zero;
    public Vector3 offset;

    PlayerController playerBase;

    private void Awake()
    {
        playerBase = GameObject.Find("Player").GetComponent<PlayerController>();
        gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(player.transform.position) + offset;
        Vector3 smoothToPos = Vector3.SmoothDamp(transform.position, wantedPos, ref velocity, 0.1f);

        transform.position = smoothToPos;

        if (playerBase.paranormalEvent)
        {
            gameObject.GetComponent<Slider>().value += 0.5f * Time.fixedDeltaTime;
        }
    }
}
