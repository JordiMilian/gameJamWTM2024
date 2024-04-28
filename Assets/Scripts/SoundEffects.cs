using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource clipsAudioSource, musicAudioSource;
    [SerializeField] AudioClip wallHitSound, victorySound, lapCompletedSound;

    private void OnEnable()
    {
        GameEvents.Instance.OnHitEnemy += hitAudio;
        GameEvents.Instance.OnEndScreen += endScreen;
        GameEvents.Instance.OnLapCompleted += lapCompleted;
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnHitEnemy -= hitAudio;
    }

    void hitAudio()
    {
        clipsAudioSource.PlayOneShot(wallHitSound);
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
