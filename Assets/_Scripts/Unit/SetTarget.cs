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
        SetTargetForBotAttack();
    }

    public void SetTargetForBotAttack()
    {
        foreach (var element in LinkManager.Instance.m_UnitsHolder.m_Units)
        {
            if (element.isBot)
                element.m_BotController.m_Target = GetComponent<Transform>();
        }

    }
}
