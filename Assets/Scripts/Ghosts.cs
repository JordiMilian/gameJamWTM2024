using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    [Serializable]
    public class GhostClass
    {
        public bool isReproducing;
        public Transform GhostTf;
        public int reproducingIndex;
        public GhostClass(Transform tF)
        {
            GhostTf = tF;
            isReproducing = true;
            reproducingIndex = 0;
        }
    }
    public List<GhostClass> GhostsList = new List<GhostClass>();

    List<Vector3> RecordedPosition = new List<Vector3>();
    [SerializeField] Transform playerShipTf;
    [SerializeField] Transform ghostShipTf;
    [SerializeField] GameObject GhostPrefab;
    public bool isRecording;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { isRecording = !isRecording; }
        if (Input.GetKeyDown(KeyCode.G)) { AddNewGhost(); }
    }
    private void FixedUpdate()
    {
        if (isRecording)
        {
            RecordedPosition.Add(playerShipTf.position);
        }
        playGhosts();
    }
    void playGhosts()
    {
        foreach (GhostClass ghost in GhostsList)
        {
            if (!ghost.isReproducing) { continue; }

            ghost.GhostTf.position = RecordedPosition[ghost.reproducingIndex];
            ghost.reproducingIndex++;
            if(ghost.reproducingIndex == RecordedPosition.Count - 1) { ghost.isReproducing = false; }
        }
    }
    public void AddNewGhost()
    {
        GameObject newGhost = Instantiate(GhostPrefab, RecordedPosition[0], Quaternion.identity);
        GhostsList.Add(new GhostClass(newGhost.transform));
        
    }
}
