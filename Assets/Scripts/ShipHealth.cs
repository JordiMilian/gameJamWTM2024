using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] int shipHealth = 3;
    [SerializeField] float invulnerableTime = 2;
    bool isInvulnerable;

    [SerializeField] Image spriteNova;
    [SerializeField] Sprite spriteNovaFullHp, spriteNova1hp, spriteNovaLastHp;

    [SerializeField] GameObject particleDeathGameObject, meshNave;

    private void Awake()
    {
        particleDeathGameObject.SetActive(false);
    }
    private void Start()
    {
        spriteNova.sprite = spriteNovaFullHp;
    }
    private void OnEnable()
    {
        GameEvents.Instance.OnHitEnemy += TouchedEnemy;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnHitEnemy -= TouchedEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ghost"))
        {
            if(!isInvulnerable)
            {
                GameEvents.Instance.OnHitEnemy?.Invoke();
                GameEvents.Instance.OnInvulnerable?.Invoke(invulnerableTime);
            }
        }
    }
    public void TouchedEnemy()
    {
        StartCoroutine(InvulnerableCooldown());
        RemoveHealth(1);
    }
    IEnumerator InvulnerableCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }
    void RemoveHealth(int h)
    {
        shipHealth -= h;

        if(shipHealth == 2)
        {
            spriteNova.sprite = spriteNova1hp;
        }
        if (shipHealth == 1)
        {
            spriteNova.sprite = spriteNovaLastHp;
        }
        if (shipHealth <= 0)
        {
            StartCoroutine(DeathScene());
        }
    }

    IEnumerator DeathScene()
    {
        particleDeathGameObject.SetActive(true);
        meshNave.SetActive(false);

        meshNave.GetComponentInParent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(3);

        GameEvents.Instance.OnEndScreen?.Invoke();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
