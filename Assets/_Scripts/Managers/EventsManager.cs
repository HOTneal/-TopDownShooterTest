using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public event Action<Unit.Unit> OnNextWeapon;

        public void NextWeapon(Unit.Unit unit)
        {
            OnNextWeapon?.Invoke(unit);
        }
    }
}
