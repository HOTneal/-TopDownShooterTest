using System;
using ScriptableObjects;
using Unit;
using UnityEngine;

namespace Controllers
{
    public class DamageController : MonoBehaviour
    {
        public void Damage(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
        
            damagedUnit.helth -= Convert.ToInt32(weapon.Damage);
            damagedUnit.helthbarUnit.Helthbar.fillAmount -= weapon.Damage / 100;
        }
    }
}