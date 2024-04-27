using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollowe : MonoBehaviour
{
    [SerializeField] Transform ShipTf;
    private void Update()
    {
        Vector3 tfPosition = transform.position;
        transform.position = Vector3.Lerp(transform.position, ShipTf.position, .1f);
    }
}
