using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolumeUIHandler : VolumeUIHandler
{
    private void OnEnable()
    {
        SFXVolumeManager.OnSFXVolumeManagerInitialized += SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged += SFXVolumeManager_OnSFXVolumeChanged;
    }

    private void OnDisable()
    {
        SFXVolumeManager.OnSFXVolumeManagerInitialized -= SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged -= SFXVolumeManager_OnSFXVolumeChanged;
    }

    protected override void SetVolumeManager() => volumeManager = SFXVolumeManager.Instance;
    private void SFXVolumeManager_OnSFXVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        InitializeUI();
    }
    private void SFXVolumeManager_OnSFXVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        if (!volumeManager) return;
        UpdateVisual();
    }
}
