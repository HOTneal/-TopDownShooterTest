using System;
using ScriptableObjects;
using Unit;
using UnityEngine;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        public void Damage(Unit.UnitController unit, Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
        
            damagedUnit.health -= Convert.ToInt32(weapon.Damage);
            damagedUnit.helthbarUnit.helthbar.fillAmount -= weapon.Damage / 100;
        }
    }
}