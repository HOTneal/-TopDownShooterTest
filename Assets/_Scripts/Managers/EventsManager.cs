using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventsManager : MonoBehaviour
{
    public event Action<Unit.Unit> OnNextWeapon;

    public void NextWeapon(Unit.Unit unit)
    {
        OnNextWeapon?.Invoke(unit);
    }
}
