using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PsychodelicEffect : MonoBehaviour
{
    private Volume volume;
    private Bloom bloom;
    private Vignette vignette;
    private ChromaticAberration ca;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out ca);
    }

    // Update is called once per frame
    void Update()
    {
        float vignetteTime = Time.time; //* Mathf.PI;
        vignette.intensity.value = Mathf.PingPong(Time.time/3, 0.54f); //Mathf.Sin(Time.time);
        
        ca.intensity.value = 1f;
    }
}
