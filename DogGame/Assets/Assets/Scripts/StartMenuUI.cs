using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public GameObject mainMenuUI;
    public AudioSource audioFile;

    public void Start()
    {
        audioFile = GetComponent<AudioSource>();
       // Time.timeScale = 0f;
    }
    public void StartButtonClick()
    {
        mainMenuUI.SetActive(false);
        audioFile.Pause();
        //Time.timeScale = 1f;
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
