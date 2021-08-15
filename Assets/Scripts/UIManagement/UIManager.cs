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

    [System.NonSerialized] public bool exorcistTalking, onDialogue, trigger;

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
        dialogueIndex++;

        trigger = false;
        onDialogue = true;
    }

    private void Update()
    {
        DialogueManager();
        PauseManager();
    }

    public void DialogueManager()
    {
        if (trigger)
            onDialogue = true;

        if (onDialogue)
            dialoguePanel.SetActive(true);
        else
            dialoguePanel.SetActive(false);

        if (dialogueIndex != dialogueArray.Length - 1 && onDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || trigger)
            {
                if (TextWriter.instance.isWrited && dialogueIndex != 4)
                {
                    dialogue = dialogueArray[dialogueIndex];
                    TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                    dialogueIndex++;
                }
                else if (trigger)
                {
                    dialogue = dialogueArray[dialogueIndex];
                    TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                    trigger = false;
                }
                else if (dialogueIndex == 4 && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && dialogueIndex == 4)
                {
                    dialogueIndex++;
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

        if (dialogueIndex == 4 && dialogue != dialogueArray[dialogueIndex])
        {
            onDialogue = false;
        }

        if (dialogueIndex < 4 || dialogueIndex == 6 || dialogueIndex == 12)
            exorcistTalking = true;
        else
            exorcistTalking = false;

        if (!exorcistTalking)
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

    public void PauseManager()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
    }
}