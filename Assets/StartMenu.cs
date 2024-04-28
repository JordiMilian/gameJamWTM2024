using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string mainScene;

    public float maxScale, minScale, scaleIncrement;
    bool isScaling = false;
    public void StartButton()
    {
        SceneManager.LoadScene(mainScene);
    }

    private void Start()
    {
        Cursor.visible = true;
        isScaling = true;

        StartCoroutine(PulseButton());
    }
    IEnumerator PulseButton()
    {
        while (true)
        {
            if (isScaling == true)
            {
                while (transform.localScale.x < maxScale)
                {
                    Vector3 newScale = new Vector3(transform.localScale.x + scaleIncrement * Time.deltaTime, transform.localScale.y + scaleIncrement * Time.deltaTime, transform.localScale.z + scaleIncrement * Time.deltaTime);
                    transform.localScale = newScale;
                    yield return null;
                }

                isScaling = false;
            }
            else
            {
                while (transform.localScale.x > minScale)
                {
                    Vector3 newScale = new Vector3(transform.localScale.x - scaleIncrement * Time.deltaTime, transform.localScale.y - scaleIncrement * Time.deltaTime, transform.localScale.z - scaleIncrement * Time.deltaTime);
                    transform.localScale = newScale;
                    yield return null;
                }

                isScaling = true;
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
