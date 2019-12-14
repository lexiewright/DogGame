using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI pausePrompt;
    public TextMeshProUGUI pauseDialogue;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;

    }

    private void Update()
    {
        if (isPaused == false && OVRInput.Get(OVRInput.RawButton.Y))
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            pausePrompt.text = "Press X to Unpause";
            pauseDialogue.text = "What would you like to do";

        }
        else if (isPaused == true && OVRInput.Get(OVRInput.RawButton.X))
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            pausePrompt.text = "Press Y to Pause";
        }
        else return;
    }

    public void ResumeButtonClicked()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenuButtonClicked()
    {
        Application.Quit();
        //SceneManager.LoadScene("StartMenu");
    }
}
