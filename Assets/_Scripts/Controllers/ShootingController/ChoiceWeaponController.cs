using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceWeaponController : MonoBehaviour
{
    [SerializeField] private DataWeapons[] m_DataWeapons;
    [SerializeField] private GameObject[] m_AllWeapons;
    [SerializeField] private int m_CurrentWeapon;
    [SerializeField] private int m_LastWeapon;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_NextWeapon;
    
    private int m_NextNumberWeapon;
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_LinkManager.m_EventsManager.OnNextWeapon += NextWeapon;
        SetWeapon(m_DataWeapons[0]);
    }

    private void NextWeapon()
    {
        m_AudioSource.PlayOneShot(m_NextWeapon);
        m_NextNumberWeapon = m_CurrentWeapon + 1;
        if (m_NextNumberWeapon >= m_DataWeapons.Length)
            m_NextNumberWeapon = 0;
        
        SetWeapon(m_DataWeapons[m_NextNumberWeapon]);
    }

    private void SetWeapon(DataWeapons weapon)
    {
        m_LastWeapon = m_CurrentWeapon;
        m_CurrentWeapon = weapon.IdWeapon;
        m_AllWeapons[m_LastWeapon].SetActive(false);
        m_AllWeapons[m_CurrentWeapon].SetActive(true);

        m_LinkManager.m_BulletController.m_AllBulletsWeapon = weapon.QuantityAllBulletsWeapon;
        m_LinkManager.m_BulletController.m_QuantityBulletsInClip = weapon.QuantityBulletsInClip;
        m_LinkManager.m_BulletController.m_DefaultBulletsInClip = weapon.QuantityBulletsInClip;
        m_LinkManager.m_UIManager.SetQuantityBullets();
        m_LinkManager.m_UIManager.SetWeaponIcon(weapon.Icon);
        m_LinkManager.m_ShootingController.m_CurrentWeapon = weapon;
    }
    
}