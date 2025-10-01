using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IDamagable
{
    public void Damage(DamageInfo info);
}

[Serializable]
public struct DamageInfo
{
    public float Damage { get; private set; }
    public Vector3 Force { get; private set; }
    public RaycastHit Hit { get; private set; }

    public DamageInfo(float damage = 0, Vector3 force = new(), RaycastHit hit = new())
    {
        Damage = damage;
        Force = force;
        Hit = hit;
    }
}