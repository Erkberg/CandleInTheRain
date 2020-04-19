using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ErksUnityLibrary;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceAtmo;
    public AudioSource audioSourceSounds;

    [Header("Sounds")]
    public AudioClip candleBurn;
    public AudioClip candleSmoke;
    public AudioClip interactSuccess;
    public AudioClip interactFail;
    public AudioClip candleUpgrade;
    public AudioClip candleDanger;
    public AudioClip mystery;

    public void SetAtmoVolumePercentage(float value)
    {
        audioSourceAtmo.volume = value;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSourceSounds.PlayOneShotRandomVolumePitch(clip);
    }
}
