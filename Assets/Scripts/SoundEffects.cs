using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource clipsAudioSource, musicAudioSource;
    [SerializeField] AudioClip wallHitSound, victorySound, lapCompletedSound, enemyHit;

    private void OnEnable()
    {
        GameEvents.Instance.OnHitEnemy += hitAudio;
        GameEvents.Instance.OnHitWall += wallHitAudio;

        GameEvents.Instance.OnEndScreen += endScreen;
        GameEvents.Instance.OnLapCompleted += lapCompleted;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnHitEnemy -= hitAudio;
        GameEvents.Instance.OnHitWall -= wallHitAudio;

        GameEvents.Instance.OnEndScreen -= endScreen;
        GameEvents.Instance.OnLapCompleted -= lapCompleted;
    }

    void hitAudio()
    {
        clipsAudioSource.PlayOneShot(enemyHit);
    }
    void wallHitAudio()
    {
        clipsAudioSource.PlayOneShot(wallHitSound, 10);
    }

    void endScreen()
    {
        musicAudioSource.Stop();
        clipsAudioSource.PlayOneShot(victorySound);
    }

    void invulnerableAudio()
    {
        TimeScaleEditor.Instance.HitStop(0.1f);
    }
    void lapCompleted()
    {
        clipsAudioSource.PlayOneShot(lapCompletedSound);
    }

}
