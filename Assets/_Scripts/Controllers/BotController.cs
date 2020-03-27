using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_DistanceLookAt = 100.0f;
    [SerializeField] private float m_DistanceAttack = 20.0f;
    
    private Transform m_BotTransform;
    private Unit m_Bot;

    private void Start()
    {
        m_Bot = GetComponent<Unit>();
        m_BotTransform = transform;
    }

    private void Update()
    {
        if (Vector3.Distance(m_BotTransform.position, m_Target.position) <= m_DistanceLookAt && m_Bot.isEnemy)
        {
            m_BotTransform.LookAt(m_Target);
            
            if (Vector3.Distance(m_BotTransform.position, m_Target.position) <= m_DistanceAttack)
                Attack();
        }
    }

    private void Attack()
    {
        m_Bot.Shoot(m_Bot);
    }
}