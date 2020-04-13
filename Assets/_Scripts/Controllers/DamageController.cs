using System;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        public void Damage(Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            damagedUnit.health -= Convert.ToInt32(weapon.Damage);
            damagedUnit.helthbarUnit.helthbar.fillAmount -= weapon.Damage / 100;
        }
    }
}