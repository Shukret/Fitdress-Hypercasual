using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class TrapCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var damageable = other.gameObject.GetComponent<IDamageable>();
        if (other.gameObject.CompareTag("Player"))
        {
            damageable?.Damage(transform);
        }
    }
}
