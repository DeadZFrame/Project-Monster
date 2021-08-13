using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter; 
    public TextMeshProUGUI dialogText;

    private void Awake()
    {
        dialogText.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        textWriter.WriteText(dialogText, "My goals are beyond of your understanding!", .3f);
    }
}
