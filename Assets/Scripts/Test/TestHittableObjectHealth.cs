using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHittableObjectHealth : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private HittableObjectHealth hittableObjectHealth;

    private void Update()
    {
        Test();
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            hittableObjectHealth.TakeDamage(new DamageData { damage = 1, isCrit = true, damageSource = null });
        }
    }
}
