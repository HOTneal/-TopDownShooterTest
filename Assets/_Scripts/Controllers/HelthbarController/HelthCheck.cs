using System;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.HelthbarController
{
    public class HelthCheck : MonoBehaviour
    {
        public Unit.UnitController whoShoot;
        public DataWeapons damagedWeapon;

        private Unit.UnitController m_Unit;

        private void Start()
        {
            m_Unit = GetComponent<Unit.UnitController>();
        }

        private void Update()
        {
            if (m_Unit.health <= 0)
                LinkManager.instance.deadController.UnitDead(whoShoot, m_Unit, damagedWeapon);
        }
    }
}