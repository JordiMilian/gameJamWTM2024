using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLine : MonoBehaviour
{
    [SerializeField] Ghosts ghostRecorder;
    [SerializeField] int LapsCompleted;
    [SerializeField] int lapsToEnd = 10;
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
            if(LapsCompleted == lapsToEnd) { GameEvents.Instance.OnEndScreen?.Invoke(); }
        }
        
    }
}
