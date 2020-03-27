using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public void Damage(Unit unit, Unit damageUnit)
    {
        BulletsQuantity bulletsQuantity = unit.m_BulletsQuantity;
        damageUnit.m_Helth -= bulletsQuantity.m_CurrentWeapon.Damage;
    }
}