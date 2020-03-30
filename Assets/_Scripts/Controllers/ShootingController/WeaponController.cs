using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private DataWeapons[] m_DataWeapons;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_NextWeapon;

    private void Start()
    {
        LinkManager.Instance.EventsManager.OnNextWeapon += NextWeapon;
    }

    public void SetWeaponParameters(int idWeapon, Unit.Unit unit)
    {
        LinkManager linkManager = LinkManager.Instance;
        DataWeapons weapon = m_DataWeapons[idWeapon];
        BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
        
        SetBullets(bulletsQuantity, weapon);

        if (unit.isBot) 
            return;
        
        SetUIWeaponParameters(linkManager, bulletsQuantity, weapon);
        bulletsQuantity.AllWeaponUnit[bulletsQuantity.LastWeapon].SetActive(false);
        bulletsQuantity.AllWeaponUnit[bulletsQuantity.CurrentWeapon.IdWeapon].SetActive(true);
    }

    private void SetBullets(BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
    {
        bulletsQuantity.AllBulletsWeapon = weapon.QuantityAllBulletsWeapon;
        bulletsQuantity.QuantityBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.DefaultBulletsInClip = weapon.QuantityBulletsInClip;
        bulletsQuantity.CurrentWeapon = weapon;
    }
    
    private void SetUIWeaponParameters (LinkManager linkManager, BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
    {
        linkManager.UIManager.SetQuantityBullets(bulletsQuantity);
        linkManager.UIManager.SetWeaponIcon(weapon.Icon);
    }
    
    private void NextWeapon(Unit.Unit unit)
    {
        BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
        m_AudioSource.PlayOneShot(m_NextWeapon);
        bulletsQuantity.LastWeapon = bulletsQuantity.CurrentWeapon.IdWeapon;
        bulletsQuantity.NextWeapon = bulletsQuantity.CurrentWeapon.IdWeapon + 1;
        
        if (bulletsQuantity.NextWeapon >= m_DataWeapons.Length)
            bulletsQuantity.NextWeapon = 0;
        
        SetWeaponParameters(bulletsQuantity.NextWeapon, unit);
    }

}