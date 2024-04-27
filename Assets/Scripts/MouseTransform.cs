using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTransform : MonoBehaviour
{
    [SerializeField] FollowMouse followMouse;
    private void Update()
    {
        transform.position = followMouse.mousePositionInPlane;
    }
}
