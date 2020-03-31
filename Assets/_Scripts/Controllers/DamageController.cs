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
            BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
        
            damagedUnit.Helth -= Convert.ToInt32(weapon.Damage);
            damagedUnit.HelthbarUnit.Helthbar.fillAmount -= weapon.Damage / 100;
        }
    }
}