using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCheckUnit : MonoBehaviour
{
    public enum ModeCanShooting
    {
        Shooting,
        NoShooting,
        NoAmmo
    }
    
    public ModeCanShooting m_ModeCanShooting = ModeCanShooting.Shooting;
    public float m_SpeedMoveBullet = 100.0f;
    [HideInInspector] public bool isNoAmmo = false;
    [HideInInspector] public Vector3 m_BulletTargetPoint;
    
    private RaycastHit m_Hit;
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        //m_LinkManager.m_EventsManager.OnStartShooting += StartShooting;
    }

    public void StartShooting(Unit unit)
    {
        if (!isNoAmmo)
        {
            if (m_ModeCanShooting == ModeCanShooting.Shooting)
                StartCoroutine(m_LinkManager.m_ShootingController.Shooting(unit));
        }
        else
        {
            if (m_ModeCanShooting == ModeCanShooting.NoAmmo)
                StartCoroutine(m_LinkManager.m_ShootingController.NoAmmo(unit));
        }
    }
    
    public void EnableShooting()
    {
        m_ModeCanShooting = ModeCanShooting.Shooting;
    }
    
    public void DisableShooting()
    {
        m_ModeCanShooting = ModeCanShooting.NoShooting;
    }
}
