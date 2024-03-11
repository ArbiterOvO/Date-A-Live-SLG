using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSoundManager : Singleton<FightSoundManager>
{
    [Header("音效资源")]
    public AudioClip enemyAttackClip;
    public AudioClip roleAttackClip;
    public AudioClip skillAttackClip;
    public AudioClip shootClip;
    public AudioClip upSlef;
    public AudioClip debuff;
    
    public void setAudio(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
