using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MusicPoolSO musicPoolSO;

    [Header("Gameplay Music Transition Settings")]
    [SerializeField,Range(0.1f,2f)]  private float fadeOutTime;
    [SerializeField, Range(0.1f, 2f)] private float fadeInTime;
    [SerializeField, Range(0.1f, 2f)] private float muteTime;

    [Header("Cinematic Gameplay Music Fade Settings")]
    [SerializeField, Range(0.1f, 2f)] private float fadeOutTimeCinematics;
    [SerializeField, Range(0.1f, 2f)] private float fadeInTimeCinematics;

    [Header("Debug")]
    [SerializeField] private AudioClip currentGameplayMusic;

    private void OnEnable()
    {
       
    }


    private void OnDisable()
    {
       
    }
}
