using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused;

    string sceneName;
    public static Pause Instance;

    [SerializeField] GameObject pauseMenu, noPauseMenu;
    private void Start()
    {
        pauseMenu.SetActive(false);
        noPauseMenu.SetActive(true);
        Instance = this;
        isPaused = false;
        sceneName = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                pauseMenu.SetActive(true);
                noPauseMenu.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                noPauseMenu.SetActive(true);
                Time.timeScale = 1;
            }
        }

        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
