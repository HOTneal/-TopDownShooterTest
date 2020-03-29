using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private float m_DistanceLookAt = 100.0f;
    [SerializeField] private float m_DistanceAttack = 20.0f;

    public Transform m_Target;
    
    private Unit m_TargetUnit;
    private Unit m_BotUnit;
    private Transform m_BotTransform;
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        m_TargetUnit = m_Target.GetComponent<Unit>();
        m_BotUnit = GetComponent<Unit>();
        m_BotTransform = transform;
        m_LinkManager = LinkManager.Instance;
    }

    private void Update()
    {
        if (m_LinkManager.m_Player == null)
            return;
        
        if (Vector3.Distance(m_BotTransform.position, m_Target.position) <= m_DistanceLookAt)
        {
            //m_BotTransform.LookAt(m_Target);
            
            if (Vector3.Distance(m_BotTransform.position, m_Target.position) <= m_DistanceAttack)
                Attack();
        }
    }

    private void Attack()
    {
        m_BotUnit.Shoot(m_BotUnit);
    }

    public void SetTarget(Transform player)
    {
        m_Target = player;
    }
}