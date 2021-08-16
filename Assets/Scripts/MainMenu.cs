using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string lvl1;
    public GameObject settingsButton;
    public Button lastGameButton;
    // Start is called before the first frame update
    void Start()
    {
       // lastGameButton.interactable = (PlayerPrefs.HasKey("currentLevel")) ? true : false ;
    }

    // Update is called once per frame
    void Update()
    {
        if(settingsButton.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                settingsButton.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(lvl1);
    }
    public void LastGame () {
        SceneManager.LoadScene(lvl1);
    }
    public void OpenOptions()
    {
        settingsButton.SetActive(true);
    }
    public void CloseOptions()
    {
        settingsButton.SetActive(false);
        
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}