using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image m_WeaponIcon;
    [SerializeField] private Text m_QuantityBullets;
    [SerializeField] private Image m_HealthBar;
    
    private LinkManager m_LinkManager;
    

    public void SetWeaponIcon(Sprite icon)
    {
        m_WeaponIcon.sprite = icon;
    }

    public void SetQuantityBullets()
    {
        m_LinkManager = LinkManager.Instance;
        var bulletsInClip = m_LinkManager.m_BulletController.m_QuantityBulletsInClip.ToString();
        var allBullets = m_LinkManager.m_BulletController.m_AllBulletsWeapon.ToString();
        m_QuantityBullets.text = $"{bulletsInClip}/{allBullets}";
    }
}
