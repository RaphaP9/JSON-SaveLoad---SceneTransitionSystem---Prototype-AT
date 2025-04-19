using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxShieldStatResolver : NumericStatResolver
{
    public static MaxShieldStatResolver Instance { get; private set; }

    protected override void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one MaxShieldStatResolver instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    protected override NumericStatType GetNumericStatType() => NumericStatType.MaxShield;
}

