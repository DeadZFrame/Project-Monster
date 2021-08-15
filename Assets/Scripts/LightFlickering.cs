using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;

    private void Update()
    {
        if(!isFlickering)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(FlickeringLight());
        gameObject.GetComponent<Light>().enabled = true;
    }


    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.01f, 0.2f);

        yield return new WaitForSeconds(timeDelay);

        gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
