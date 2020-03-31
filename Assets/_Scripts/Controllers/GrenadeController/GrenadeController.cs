using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    [SerializeField] private float m_Radius = 20.0f;
    [SerializeField] private float m_ExplodeTime = 1.0f;
    [SerializeField] private DataWeapons m_Grenade;

    private LinkManager m_LinkManager;
    private GrenadeEffects m_Effects;
    
    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_Effects = GetComponent<GrenadeEffects>();
        
        StartCoroutine(ExplodeGrenade());
    }
    
    private IEnumerator ExplodeGrenade()
    {
        yield return new WaitForSeconds(m_ExplodeTime);
        
        Collider[] allUnitsInRadius = Physics.OverlapSphere(transform.position, m_Radius);
        
        foreach (var nearbyObj in allUnitsInRadius)
        {
            if (nearbyObj.TryGetComponent(out Unit.Unit unit))
                RayCastForDamage(unit);
        }
        
        m_Effects.GenerateEffects(transform);
        Destroy(gameObject);
    }

    private void RayCastForDamage(Unit.Unit nearbyUnit)
    {
        RaycastHit hit;
        Transform nearbyUnitTarget = nearbyUnit.PointForDamage;
        Vector3 pos = nearbyUnitTarget.position - transform.position;
        float distance = pos.magnitude;
        Vector3 targetDirection = pos / distance;
        
        if (Physics.Raycast(transform.position, targetDirection, out hit))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Bot"))
                Damage(m_LinkManager.m_Player, nearbyUnit, m_Grenade);
        }
    }

    private void Damage(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
    {
        m_LinkManager.DamageController.Damage(unit, damagedUnit, weapon);
        m_LinkManager.HelthController.CheckLiveUnit(unit, damagedUnit, weapon);
    }
}