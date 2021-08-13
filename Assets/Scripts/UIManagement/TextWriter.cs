using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI dialogText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;

    private void Update()
    {
        if(dialogText != null)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                dialogText.text = textToWrite.Substring(0, characterIndex);
                if(characterIndex >= textToWrite.Length)
                {
                    dialogText = null;
                    return;
                }
            }
        }
    }

    public void WriteText(TextMeshProUGUI dialogText, string textToWrite, float timePerCharacter)
    {
        this.dialogText = dialogText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }
}
