using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string m_Nickname;
    public bool isBot = false;
    public bool isEnemy = false;
    public Transform m_PointForGenerateBullets;
    public BulletsQuantity m_BulletsQuantity;
    public ShootingCheck m_ShootingCheck;
    [HideInInspector] public float m_SpeedWalk;
    [HideInInspector] public Animator m_Animator;
    [HideInInspector] public CharacterController m_ChController;
    [HideInInspector] public int m_IdDefaultWeaponAtStart = 0;
    
    private LinkManager m_LinkManager;
    private BotController m_BotController;
    
    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_Animator = GetComponent<Animator>();
        m_ChController = GetComponent<CharacterController>();
        m_PointForGenerateBullets = transform.GetChild(0).GetComponent<Transform>();
        m_BulletsQuantity = GetComponent<BulletsQuantity>();
        m_ShootingCheck = GetComponent<ShootingCheck>();

        if (!isBot)
            SetPlayerMoveSettings();
        else
            m_BotController = GetComponent<BotController>();
        
        SetWeapon();
    }

    private void SetPlayerMoveSettings()
    {
        m_LinkManager.m_MovementController.SetPlayerMoveSettings(this);
    }
    
    public void Shoot(Unit unit)
    {
        //m_LinkManager.m_EventsManager.StartShoot(unit);
        m_ShootingCheck.StartShooting(unit);
    }
    
    private void SetWeapon()
    {
        m_LinkManager.m_WeaponController.SetWeapon(m_IdDefaultWeaponAtStart, m_BulletsQuantity);
    }

    public void NextWeapon()
    {
        m_LinkManager.m_EventsManager.NextWeapon(this);
    }
}