using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Image exorcist, demon;

    private bool exorcistTalking;

    public string[] dialogueArray;
    string dialogue;

    private int dialogueIndex = 0;

    private void Awake()
    {
        dialogText.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        dialogue = dialogueArray[dialogueIndex];
        TextWriter.WriteText_Static(dialogText, dialogue, .1f, true, true);
    }

    private void Update()
    {
        DialogueManager();
    }

    public void DialogueManager()
    {
        if (dialogueIndex != dialogueArray.Length - 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (TextWriter.instance.isWrited)
                {
                    dialogueIndex++;
                    dialogue = dialogueArray[dialogueIndex];
                    TextWriter.WriteText_Static(dialogText, dialogue, .1f, true, true);
                }
                else
                {
                    TextWriter.WriteText_Static(dialogText, dialogue, 0f, true, true);
                }
            }
        }

        if (exorcistTalking)
        {
            exorcist.gameObject.SetActive(true);
            demon.gameObject.SetActive(false);
        }
        else
        {
            exorcist.gameObject.SetActive(false);
            demon.gameObject.SetActive(true);
        }
    }
}