using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI maxSpeedText;
    [SerializeField] GameObject PanelGO;
    [SerializeField] GameObject CurrentSpeedUIPanel;
    [SerializeField] FollowMouse playerFollowMouse;
    [SerializeField] VelocityToTIme velocity2Time;
    [SerializeField] GameObject ImageLow, ImageMid, ImageHigh, ImageVictory;
    bool gameEnded;
    private void OnEnable()
    {
        GameEvents.Instance.OnEndScreen += onEndScreenUI;
    }
    private void Awake()
    {
        PanelGO.SetActive(false);
    }
    void onEndScreenUI()
    {
        PanelGO.SetActive(true);
        CurrentSpeedUIPanel.SetActive(false);
        string HighestScoreText = velocity2Time.CalculateTime(playerFollowMouse.maxSpeedReached) + playerFollowMouse.maxSpeedReached;
        maxSpeedText.text = HighestScoreText;
        int imageToDelete = velocity2Time.ChooseImage(playerFollowMouse.maxSpeedReached);
        HideEveryImageBut(imageToDelete);
        gameEnded = true;
        Time.timeScale = 0;
    }
    private void Update()
    {
        if (gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }
    }
    void HideEveryImageBut(int chosenIndex)
    {
        ImageLow.SetActive(false);
        ImageMid.SetActive(false);
        ImageHigh.SetActive(false);
        ImageVictory.SetActive(false);
        if (chosenIndex == 0) { ImageLow.SetActive(true); }
        if(chosenIndex == 1) { ImageMid.SetActive(true); }
        if(chosenIndex == 2) { ImageHigh.SetActive(true); }
        if(chosenIndex == 3) { ImageVictory.SetActive(true); }
    }
    
}
