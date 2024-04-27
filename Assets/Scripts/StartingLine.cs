using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLine : MonoBehaviour
{
    [SerializeField] Ghosts ghostRecorder;

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
        }
        
    }
}
