using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    #region Singltone
    private static LinkManager _instance;
    public static LinkManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Unit m_Player;
    public MobileInputController m_MobilePlayerController;
    public ShootingController m_ShootingController;
    public RespawnController m_RespawnController;
    public BulletController m_BulletController;
    public WeaponController m_WeaponController;
    public DamageController m_DamageController;
    public HelthController m_HelthController;
    public DeadController m_DeadController;
    public EventsManager m_EventsManager;
    public UnitsHolder m_UnitsHolder;
    public UIManager m_UIManager;
}
