using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText, fadingText;
    public Image exorcist, demon;
    public GameObject pausePanel, dialoguePanel;

    [System.NonSerialized]public bool exorcistTalking, onDialogue;

    public string[] dialogueArray;
    string dialogue;

    private int dialogueIndex = 0;

    private void Awake()
    {
        dialogText.GetComponent<TextMeshProUGUI>();
        fadingText.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        dialogue = dialogueArray[dialogueIndex];
        TextWriter.WriteText_Static(dialogText, dialogue, .1f, true, true);
    }

    private void Update()
    {
        DialogueManager();
        PauseManager();
    }

    public void DialogueManager()
    {
        if (dialogueIndex != dialogueArray.Length - 1)
        {
            onDialogue = true;
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
            fadingText.gameObject.SetActive(true);
        }
        else
        {
            fadingText.gameObject.SetActive(false);
            onDialogue = false;
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

        if (onDialogue)
            dialoguePanel.SetActive(true);
            
        else
            dialoguePanel.SetActive(false);
    }

    public void PauseManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
    }
}