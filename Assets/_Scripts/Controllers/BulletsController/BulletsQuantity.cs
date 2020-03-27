using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsQuantity : MonoBehaviour
{
    [HideInInspector] public DataWeapons m_CurrentWeapon;
    [HideInInspector] public int m_QuantityBulletsInClip;
    [HideInInspector] public int m_AllBulletsWeapon;
    [HideInInspector] public int m_NextWeapon;
    [HideInInspector] public int m_LastWeapon;
    [HideInInspector] public int m_DefaultBulletsInClip;
}
