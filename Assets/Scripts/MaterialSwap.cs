using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    public Material[] materials;
    public Material material;

    private void Awake()
    {
        material = GetComponent<Material>();
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = GetComponent < Material>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Home"))
        {
            for (int i = 0; i < materials.Length; i++)
                materials[i] = material;
        }
    }
}
