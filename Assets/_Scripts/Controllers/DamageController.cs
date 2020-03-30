using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public void Damage(Unit.Unit unit, Unit.Unit damagedUnit)
    {
        BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
        
        damagedUnit.Helth -= Convert.ToInt32(bulletsQuantity.CurrentWeapon.Damage);
        damagedUnit.HelthbarUnit.Helthbar.fillAmount -= unit.BulletsQuantity.CurrentWeapon.Damage / 100;
    }
}