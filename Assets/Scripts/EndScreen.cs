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
        string HighestScore = velocity2Time.CalculateTime(playerFollowMouse.maxSpeedReached);
        maxSpeedText.text = HighestScore;
        gameEnded = true;
    }
    private void Update()
    {
        if (gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
