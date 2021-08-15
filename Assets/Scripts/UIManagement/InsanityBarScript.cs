using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityBarScript : MonoBehaviour
{
    public GameObject player;
    public Animation background, fill;

    Vector3 velocity = Vector3.zero;
    public Vector3 offset;

    PlayerController playerBase;

    private void Awake()
    {
        playerBase = GameObject.Find("Player").GetComponent<PlayerController>();
        gameObject.GetComponent<Slider>();

        background.GetComponent<Animation>();
        fill.GetComponent<Animation>();
    }

    private void Start()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Slider>().value = 0;
    }

    private void Update()
    {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(player.transform.position) + offset;
        Vector3 smoothToPos = Vector3.SmoothDamp(transform.position, wantedPos, ref velocity, 0.1f);

        transform.position = smoothToPos;

        if (playerBase.paranormalEvent)
        {
            gameObject.GetComponent<Slider>().value += 0.2f * Time.fixedDeltaTime;

            background.Play();
        }
        else
        {
            if (gameObject.GetComponent<Slider>().value != 0)
            {
                background.gameObject.SetActive(true);
                fill.Play();
            }
            else
            {
                var fillColor = fill.gameObject.GetComponent<Image>().color;
                fillColor.a = 1;
                background.gameObject.SetActive(false);
            }
                
            gameObject.GetComponent<Slider>().value -= 0.1f * Time.fixedDeltaTime;
        }
    }
}
