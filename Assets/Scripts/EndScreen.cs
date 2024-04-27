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
        maxSpeedText.text = playerFollowMouse.maxSpeedReached.ToString();
        gameEnded = true;
    }
    private void Update()
    {
        if (gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
