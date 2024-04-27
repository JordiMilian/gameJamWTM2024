using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    Collider ownCollider;
    private void Awake()
    {
        ownCollider = GetComponent<Collider>();
        ownCollider.enabled = false;
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        ownCollider.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.LogError("YOU DIED");
        }
    }
}
