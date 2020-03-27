using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private DataWeapons[] m_DataWeapons;
    [SerializeField] private GameObject[] m_AllWeapons;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_NextWeapon;

    private void Start()
    {
        LinkManager.Instance.m_EventsManager.OnNextWeapon += NextWeapon;
    }

    public void SetWeapon(int idWeapon, BulletsQuantity bulletsQuantity)
    {
        DataWeapons weapon = m_DataWeapons[idWeapon];
        LinkManager m_LinkManager = LinkManager.Instance;

        bulletsQuantity.m_AllBulletsWeapon = weapon.QuantityAllBulletsWeapon;
        bulletsQuantity.m_QuantityBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.m_DefaultBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.m_CurrentWeapon = weapon;
        
        m_AllWeapons[bulletsQuantity.m_LastWeapon].SetActive(false);
        m_AllWeapons[bulletsQuantity.m_CurrentWeapon.IdWeapon].SetActive(true);

        m_LinkManager.m_UIManager.SetQuantityBullets();
        m_LinkManager.m_UIManager.SetWeaponIcon(weapon.Icon);
    }
    
    private void NextWeapon(Unit unit)
    {
        BulletsQuantity bulletsQuantity = unit.m_BulletsQuantity;
        m_AudioSource.PlayOneShot(m_NextWeapon);
        bulletsQuantity.m_NextWeapon = bulletsQuantity.m_CurrentWeapon.IdWeapon + 1;
        if (bulletsQuantity.m_NextWeapon >= m_DataWeapons.Length)
            bulletsQuantity.m_NextWeapon = 0;
        
        SetWeapon(bulletsQuantity.m_NextWeapon, bulletsQuantity);
    }

}