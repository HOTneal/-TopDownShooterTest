using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private DataWeapons[] m_DataWeapons;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_NextWeapon;

    private void Start()
    {
        LinkManager.Instance.m_EventsManager.OnNextWeapon += NextWeapon;
    }

    public void SetWeaponParameters(int idWeapon, Unit unit)
    {
        DataWeapons weapon = m_DataWeapons[idWeapon];
        LinkManager linkManager = LinkManager.Instance;
        BulletsQuantityUnit bulletsQuantity = unit.m_BulletsQuantity;

        SetBullets(bulletsQuantity, weapon);

        if (unit.isBot) 
            return;
        
        SetUIWeaponParameters(linkManager, bulletsQuantity, weapon);
        bulletsQuantity.m_AllWeaponUnit[bulletsQuantity.m_LastWeapon].SetActive(false);
        bulletsQuantity.m_AllWeaponUnit[bulletsQuantity.m_CurrentWeapon.IdWeapon].SetActive(true);
    }

    private void SetBullets(BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
    {
        bulletsQuantity.m_AllBulletsWeapon = weapon.QuantityAllBulletsWeapon;
        bulletsQuantity.m_QuantityBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.m_DefaultBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.m_CurrentWeapon = weapon;       
    }
    
    private void SetUIWeaponParameters (LinkManager linkManager, BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
    {
        linkManager.m_UIManager.SetQuantityBullets(bulletsQuantity);
        linkManager.m_UIManager.SetWeaponIcon(weapon.Icon);
    }
    
    private void NextWeapon(Unit unit)
    {
        BulletsQuantityUnit bulletsQuantity = unit.m_BulletsQuantity;
        m_AudioSource.PlayOneShot(m_NextWeapon);
        bulletsQuantity.m_LastWeapon = bulletsQuantity.m_CurrentWeapon.IdWeapon;
        bulletsQuantity.m_NextWeapon = bulletsQuantity.m_CurrentWeapon.IdWeapon + 1;
        
        if (bulletsQuantity.m_NextWeapon >= m_DataWeapons.Length)
            bulletsQuantity.m_NextWeapon = 0;
        
        SetWeaponParameters(bulletsQuantity.m_NextWeapon, unit);
    }

}