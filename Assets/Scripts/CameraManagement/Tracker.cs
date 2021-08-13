using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public float smoothSpeed = 0.125f;

    public Transform player;

    public Vector3 offset;
    Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 cameraPos = player.position + offset;
        Vector3 smoothTrack = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, smoothSpeed);
        transform.position = smoothTrack;
    }
}
