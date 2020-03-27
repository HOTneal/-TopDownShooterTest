using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private Transform m_BotTransform;
    private Unit m_Bot;

    private void Start()
    {
        m_Bot = GetComponent<Unit>();
        m_BotTransform = transform;
    }

    private void Update()
    {
        if (Vector3.Distance(m_BotTransform.position, m_Target.position) <= 5.0f && m_Bot.isEnemy)
            m_Bot.Shoot(m_Bot);
    }
    
}