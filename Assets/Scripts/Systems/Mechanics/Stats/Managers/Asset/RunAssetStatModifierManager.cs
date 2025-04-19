using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAssetStatModifierManager : AssetStatModifierManager
{
    public static RunAssetStatModifierManager Instance { get; private set; }

    protected override void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one RunAssetStatModifierManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
}


