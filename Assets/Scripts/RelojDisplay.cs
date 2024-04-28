using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelojDisplay : MonoBehaviour
{
    [SerializeField] float minAngleDeg;
    [SerializeField] float maxAngleDeg;
    [SerializeField] GameObject AgujaPivot;
    [SerializeField] FollowMouse followMouse;
    private void Update()
    {
        float newZRotation = Mathf.Lerp(minAngleDeg, maxAngleDeg, followMouse.normalizedTotalSpeed);
        AgujaPivot.transform.eulerAngles = new Vector3(0,0,newZRotation);
    }
}
