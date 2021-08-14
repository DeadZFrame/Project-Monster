using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string lvl1;
    public GameObject settingsButton;
    // Start is called before the first frame update
    void Start()
    {
        
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
