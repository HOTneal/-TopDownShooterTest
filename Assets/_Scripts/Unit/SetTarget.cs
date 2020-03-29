using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget : MonoBehaviour
{
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    public void SetTargetForBotAttack()
    {
        foreach (var value in LinkManager.Instance.m_UnitsHolder.m_Units)
        {
            if (value.isBot)
                value.m_BotController.m_Target = transform;
        }
    }
}
