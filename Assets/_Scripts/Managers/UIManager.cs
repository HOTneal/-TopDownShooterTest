using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image m_WeaponIcon;
    [SerializeField] private Text m_QuantityBullets;
    [SerializeField] private Image m_DarkPanel;
    [SerializeField] private GameObject m_DeadPanel;
    [SerializeField] private GameObject m_Joystick;
    
    private CanvasGroup m_CanvasGroup;

    private void Start()
    {
        m_CanvasGroup = m_DarkPanel.gameObject.GetComponent<CanvasGroup>();
    }

    public void SetQuantityBullets(BulletsQuantityUnit bulletsQuantity)
    {
        var bulletsInClip = bulletsQuantity.m_QuantityBulletsInClip;
        var allBullets = bulletsQuantity.m_AllBulletsWeapon;
        m_QuantityBullets.text = $"{bulletsInClip}/{allBullets}";
    }
    
    public void SetWeaponIcon(Sprite icon)
    {
        m_WeaponIcon.sprite = icon;
    }

    public void DeadPanel(bool isVisible)
    {
        m_DeadPanel.SetActive(isVisible);
    }

    public void DarkPanel(float alpha, bool isVisible)
    {
        m_CanvasGroup.alpha = alpha;
        m_DarkPanel.gameObject.SetActive(isVisible);
    }

    public void Interface(bool isActive)
    {
        m_Joystick.SetActive(isActive);
        m_QuantityBullets.enabled = isActive;
        m_WeaponIcon.enabled = isActive;
    }
}