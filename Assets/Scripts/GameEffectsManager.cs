using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEffectsManager : MonoBehaviour
{
    [SerializeField] GameObject ShipMeshRoot;
    [SerializeField] Color flashColor;
    [SerializeField] float flashDelay;

    MeshRenderer[] meshRenderers;
    private void OnEnable()
    {
        GameEvents.Instance.OnHitEnemy += hitEnemyEffects;
    }
    void hitEnemyEffects()
    {

        TimeScaleEditor.Instance.HitStop(0.1f);
        meshRenderers = ShipMeshRoot.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {

        }
    }
    IEnumerator FlashMaterials(MeshRenderer renderer)
    {

        renderer.material.SetFloat("_opacity", 0);
        yield return null;
    }
     
}
