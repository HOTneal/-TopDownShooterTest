using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers.BotController
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private float m_DistanceAttack = 20.0f;

        public Transform target;
        public bool isAttack = true;
        
        private Unit.UnitController m_BotUnit;
        private Transform m_BotTransform;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;

            if (m_LinkManager.player != null && target == null)
                target = m_LinkManager.player.transform;
        
            m_BotUnit = GetComponent<Unit.UnitController>();
            m_BotTransform = transform;
        }

        private void Update()
        {
            if (!isAttack || m_LinkManager.player == null || target == null)
                return;

            if (Vector3.Distance(m_BotTransform.position, target.localPosition) <= m_DistanceAttack)
                m_BotUnit.Shoot();
            else
                m_BotUnit.ShootEnd();
        }

        public void SetTarget(Transform player)
        {
            target = player;
        }
    }
}