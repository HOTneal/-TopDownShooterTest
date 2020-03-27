using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventsManager : MonoBehaviour
{
    public event Action<DataWeapons, Unit> OnStartPlayerShooting;
    public event Action<DataWeapons, Unit> OnEndPlayerShooting;
    public event Action OnNextWeapon;

    public event Action OnBotShooting;

    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    public void StartPlayerShoot(Unit unit)
    {
        OnStartPlayerShooting?.Invoke(m_LinkManager.m_ShootingController.m_CurrentWeapon, unit);
    }

    public void EndPlayerShoot(Unit unit)
    {
        OnEndPlayerShooting?.Invoke(m_LinkManager.m_ShootingController.m_CurrentWeapon, unit);
    }

    public void NextWeapon()
    {
        OnNextWeapon?.Invoke();
    }

    public void BotShooting()
    {
        OnBotShooting?.Invoke();
    }
}
