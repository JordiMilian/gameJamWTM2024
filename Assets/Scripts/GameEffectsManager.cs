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
        GameEvents.Instance.OnInvulnerable += InvulnerableEffects;
    }
    private void Awake()
    {
        meshRenderers = ShipMeshRoot.GetComponentsInChildren<MeshRenderer>();
    }
    void hitEnemyEffects()
    {
        TimeScaleEditor.Instance.HitStop(0.1f);
    }
    void InvulnerableEffects(float time)
    {
        StartCoroutine(FlashMaterials(time));
    }
    IEnumerator FlashMaterials(float time)
    {
        float timer = 0;
        float switchTimer = 0;
        int currentOpacity = 1;
        while (timer < time)
        {
            timer += Time.deltaTime;
            switchTimer += Time.deltaTime;
            if (switchTimer > flashDelay)
            {
                if (currentOpacity == 1) { SwithOpacity(0); currentOpacity = 0; }
                else { SwithOpacity(1); currentOpacity = 1; }
                switchTimer = 0;
            }
            
            yield return null;  
        }
        SwithOpacity(1);
    }
    void SwithOpacity(int opacity)
    {
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material.SetFloat("_opacity", opacity);
        }
    }
     
}
