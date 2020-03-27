using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private AudioClip m_SoundReload;
    [SerializeField] private AudioSource m_AudioSource;
    
    public int m_QuantityBulletsInClip;
    public int m_AllBulletsWeapon;
    [HideInInspector] public int m_DefaultBulletsInClip;

    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    public void BulletsCount(DataWeapons weapon)
    {
        if (m_QuantityBulletsInClip == 0) return;
        
        m_QuantityBulletsInClip--;
        if (m_QuantityBulletsInClip == 0 & m_AllBulletsWeapon != 0)
            StartCoroutine(ReloadWeapon(weapon));
            
        if (m_QuantityBulletsInClip == 0 & m_AllBulletsWeapon == 0)
            NoBullets();
    }

    private IEnumerator ReloadWeapon(DataWeapons weapon)
    {
        Animator animator = m_LinkManager.m_MovementController.m_Animator;
        m_LinkManager.m_ShootingController.StopAllCoroutines();
        m_LinkManager.m_ShootingController.DisableShooting();
        animator.SetTrigger("ReloadGun");

        m_AudioSource.PlayOneShot(m_SoundReload);
        yield return new WaitForSeconds(weapon.ReloadTime);
        if (!weapon.InfinityBullets)
            m_AllBulletsWeapon -= m_DefaultBulletsInClip;
        m_AudioSource.Stop();
        m_QuantityBulletsInClip = m_DefaultBulletsInClip;
        LinkManager.Instance.m_UIManager.SetQuantityBullets();
        m_LinkManager.m_ShootingController.EnableShooting();
    }

    private void NoBullets()
    {
        m_LinkManager.m_ShootingController.isNoAmmo = true;
        m_LinkManager.m_ShootingController.m_ModeCanShooting = ShootingController.ModeCanShooting.NoAmmo;
    }
}