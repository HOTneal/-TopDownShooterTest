using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public void Damage(Unit unit, Unit damagedUnit)
    {
        BulletsQuantityUnit bulletsQuantity = unit.m_BulletsQuantity;
        
        damagedUnit.m_Helth -= Convert.ToInt32(bulletsQuantity.m_CurrentWeapon.Damage);
        damagedUnit.m_HelthbarUnit.m_Helthbar.fillAmount -= unit.m_BulletsQuantity.m_CurrentWeapon.Damage / 100;
    }
}