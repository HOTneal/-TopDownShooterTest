using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventsManager : MonoBehaviour
{
    public event Action<Unit> OnStartShooting;
    public event Action<Unit> OnNextWeapon;

    public void StartShoot(Unit unit)
    {
        OnStartShooting?.Invoke(unit);
    }

    public void NextWeapon(Unit unit)
    {
        OnNextWeapon?.Invoke(unit);
    }
}
