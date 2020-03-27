using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float m_SpeedWalk;
    
    public string m_Nickname;
    public bool isBot = false;
    public bool isEnemy = false;
    public Transform m_PointForGenerateBullets;
    [HideInInspector] public Animator m_Animator;
    
    private CharacterController m_ChController;
    private LinkManager m_LinkManager;
    private BotController m_BotController;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_Animator = GetComponent<Animator>();
        m_ChController = GetComponent<CharacterController>();
        m_PointForGenerateBullets = transform.GetChild(0).GetComponent<Transform>();

        if (!isBot)
            SetPlayerMoveSettings();
        else
        {
            m_BotController = GetComponent<BotController>();
            BotController();
        }
    }

    private void SetPlayerMoveSettings()
    {
        m_LinkManager.m_MovementController.SetPlayerMoveSettings(transform, m_Animator, m_ChController, m_SpeedWalk);
    }

    private void BotController()
    {
        
    }
    
    public void Shoot(Unit unit)
    {
        m_LinkManager.m_EventsManager.StartPlayerShoot(unit);
    }
}