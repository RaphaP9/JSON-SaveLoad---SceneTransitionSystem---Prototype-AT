using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseUIInGameOptionsContentButtonsHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PauseUIContentsHandler pauseUIContentsHandler;

    [Header("UI Components")]
    [SerializeField] private Button inGameAudioOptionsButton;
    [SerializeField] private Button inGameGraphicsOptionsButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        inGameAudioOptionsButton.onClick.AddListener(ShowInGameOptionsAudioContent);
        inGameGraphicsOptionsButton.onClick.AddListener(ShowInGameOptionsGraphicsContent);
        backButton.onClick.AddListener(ShowMainContent);
    }

    private void ShowInGameOptionsAudioContent() => pauseUIContentsHandler.ShowInGameAudioOptionsContent();
    private void ShowInGameOptionsGraphicsContent() => pauseUIContentsHandler.ShowInGameGraphicsOptionsContent();
    private void ShowMainContent() => pauseUIContentsHandler.ShowMainContent();
}
