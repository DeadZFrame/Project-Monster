using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Scene scene;

    public TextMeshProUGUI dialogText, fadingText;
    public Image exorcist, demon, follower;
    public GameObject pausePanel, dialoguePanel;
    public Animation end;

    [System.NonSerialized] public bool exorcistTalking, onDialogue, trigger, first;

    public string[] dialogueArray;
    [System.NonSerialized] public string dialogue;

    private int dialogueIndex = 0;

    private void Awake()
    {
        dialogText.GetComponent<TextMeshProUGUI>();
        fadingText.GetComponent<TextMeshProUGUI>();
        //end.GetComponent<Animation>();
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        dialogue = dialogueArray[dialogueIndex];
        TextWriter.WriteText_Static(dialogText, dialogue, .1f, true, true);
        dialogueIndex++;

        trigger = false;
        if (scene.name.Equals("MainScene"))
        {
            onDialogue = true;
        }
        else if (scene.name.Equals("LEVEL 2"))
        {
            onDialogue = false;
        }

        first = true;
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

        if (scene.name.Equals("MainScene"))
        {
            if (dialogueIndex != dialogueArray.Length - 1 && onDialogue)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || trigger)
                {
                    if (TextWriter.instance.isWrited && dialogueIndex != 4 && dialogueIndex != 17)
                    {
                        dialogue = dialogueArray[dialogueIndex];
                        TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                        dialogueIndex++;
                        first = true;
                    }
                    else if (trigger)
                    {
                        dialogue = dialogueArray[dialogueIndex];
                        TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                        trigger = false;
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

            if (dialogueIndex == 4 && first)
            {
                onDialogue = false;
                first = false;
            }
            if (dialogueIndex == 17 && first)
            {
                onDialogue = false;
                first = false;
            }

            if (dialogueIndex <= 5 || dialogueIndex == 14 || dialogueIndex == 18)
                exorcistTalking = true;
            else
                exorcistTalking = false;
        }

        if (scene.name.Equals("LEVEL 2"))
        {
            if (dialogueIndex != dialogueArray.Length - 1 && onDialogue)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    if (TextWriter.instance.isWrited)
                    {
                        dialogue = dialogueArray[dialogueIndex];
                        TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                        dialogueIndex++;
                        first = true;
                    }
                    else if (trigger)
                    {
                        dialogue = dialogueArray[dialogueIndex];
                        TextWriter.WriteText_Static(dialogText, dialogue, .04f, true, true);
                        trigger = false;
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
                //onDialogue = false;
                
                Debug.Log("last text"); 
            }

            if(dialogueIndex == 6)
            {
                follower.gameObject.SetActive(true);
            }

            if(dialogueIndex == 11)
            {
                end.gameObject.SetActive(true);
                if (end.gameObject.activeInHierarchy)
                    StartCoroutine(toMainMenu(2f));
            }

            Debug.Log(dialogueIndex);

            //if(dialogueIndex == 9)
            //{
            //    end.Play();
            //}

            if (dialogueIndex == 2 || dialogueIndex == 5 || dialogueIndex == 6 || dialogueIndex == 10 || dialogueIndex == 11)
                exorcistTalking = true;
            else
                exorcistTalking = false;
        }

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

    IEnumerator toMainMenu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
    }
}