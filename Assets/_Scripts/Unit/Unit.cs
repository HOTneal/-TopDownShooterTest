using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string m_Nickname;
    public int m_Helth = 100;
    public bool isBot = false;
    public bool isEnemy = false;
    public bool isDead = false;
    public Transform m_PointForGenerateBullets;
    public BulletsQuantityUnit m_BulletsQuantity;
    public ShootingCheckUnit m_ShootingCheck;
    public float m_SpeedWalk;
    public Transform m_PointForSpawn;
    [HideInInspector] public Animator m_Animator;
    [HideInInspector] public CharacterController m_ChController;
    [HideInInspector] public int m_IdDefaultWeaponAtStart = 0;
    [HideInInspector] public HelthbarUnit m_HelthbarUnit;
    [HideInInspector] public BotController m_BotController;
    [HideInInspector] public SetTarget m_SetTarget;

    private LinkManager m_LinkManager;
    private MovementController m_MovementController;
    
    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_Animator = GetComponent<Animator>();
        m_ChController = GetComponent<CharacterController>();
        m_PointForGenerateBullets = transform.GetChild(0).GetComponent<Transform>();
        m_BulletsQuantity = GetComponent<BulletsQuantityUnit>();
        m_ShootingCheck = GetComponent<ShootingCheckUnit>();
        m_HelthbarUnit = GetComponent<HelthbarUnit>();
        m_MovementController = GetComponent<MovementController>();

        if (!isBot)
        {
            m_LinkManager.m_Player = this;
            SetPlayerMoveSettings();
            m_SetTarget = GetComponent<SetTarget>();
            m_SetTarget.SetTargetForBotAttack();
        }
        else
            m_BotController = GetComponent<BotController>();

        SetWeapon();
    }

    private void SetPlayerMoveSettings()
    {
        m_MovementController.SetPlayerMoveSettings(this);
    }
    
    public void Shoot(Unit unit)
    {
        //m_LinkManager.m_EventsManager.StartShoot(unit);
        m_ShootingCheck.StartShooting(unit);
    }
    
    private void SetWeapon()
    {
        m_LinkManager.m_WeaponController.SetWeaponParameters(m_IdDefaultWeaponAtStart, this);
    }

    public void NextWeapon()
    {
        m_LinkManager.m_EventsManager.NextWeapon(this);
    }
}