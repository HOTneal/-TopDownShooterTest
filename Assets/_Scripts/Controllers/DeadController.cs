using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadController : MonoBehaviour
{
    private float m_DefalutTimeRespawn = 5.0f;
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    public void UnitDead(Unit unit)
    {
        if (!unit.isBot)
            PlayerDead(unit);
        else
            BotDead(unit);
    }

    private void Respawn(Unit unit)
    {
        StartCoroutine(m_LinkManager.m_RespawnController.Respawn(unit, m_DefalutTimeRespawn));
    }
    
    private void PlayerDead(Unit unit)
    {
        m_LinkManager.m_UIManager.DeadPanel(true);
        m_LinkManager.m_UIManager.DarkPanel(0.8f, true);
        m_LinkManager.m_UIManager.Interface(false);
        unit.isDead = true;
        m_LinkManager.m_UnitsHolder.DeleteUnitFromHolder(unit);
        Destroy(unit.gameObject);
    }

    private void BotDead(Unit unit)
    {
        Respawn(unit);
        m_LinkManager.m_UnitsHolder.DeleteUnitFromHolder(unit);
        Destroy(unit.gameObject);
    }
}
