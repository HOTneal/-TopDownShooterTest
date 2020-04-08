using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public event Action<Unit.UnitController> OnNextWeapon;

        public void NextWeapon(Unit.UnitController unit)
        {
            OnNextWeapon?.Invoke(unit);
        }
    }
}
