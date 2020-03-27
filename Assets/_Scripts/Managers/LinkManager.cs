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
    public MovementController m_MovementController;
    public ShootingController m_ShootingController;
    public BulletController m_BulletController;
    public WeaponController m_WeaponController;
    public DamageController m_DamageController;
    public InputController m_InputController;
    public EventsManager m_EventsManager;
    public UIManager m_UIManager;
}
