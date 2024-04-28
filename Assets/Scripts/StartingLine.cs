using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLine : MonoBehaviour
{
    [SerializeField] Ghosts ghostRecorder;
    [SerializeField] int LapsCompleted;
    [SerializeField] int lapsToEnd = 10;

    [SerializeField] GameObject particleDeathGameObject, meshNave;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player") { return; }

        if (ghostRecorder.isRecording == false)
        {
            ghostRecorder.isRecording = true;
        }
        else
        {
            ghostRecorder.AddNewGhost();
            GameEvents.Instance.OnLapCompleted?.Invoke();
            LapsCompleted++;
            if(LapsCompleted == lapsToEnd) 
            {
                StartCoroutine(DeathScene());
                //GameEvents.Instance.OnEndScreen?.Invoke();
            }
        }
        
    }

    IEnumerator DeathScene()
    {
        particleDeathGameObject.SetActive(true);
        meshNave.SetActive(false);

        yield return new WaitForSeconds(3);

        GameEvents.Instance.OnEndScreen?.Invoke();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
