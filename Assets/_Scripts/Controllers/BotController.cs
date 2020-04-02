using Managers;
using UnityEngine;

namespace Controllers
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private float m_DistanceLookAt = 100.0f;
        [SerializeField] private float m_DistanceAttack = 20.0f;

        public Transform Target;
        public bool isAttack = true;
    
        private Unit.Unit m_TargetUnit;
        private Unit.Unit m_BotUnit;
        private Transform m_BotTransform;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.Instance;

            if (m_LinkManager.m_Player != null && Target == null)
                Target = m_LinkManager.m_Player.PointForDamage.transform;
        
            m_BotUnit = GetComponent<Unit.Unit>();
            m_BotTransform = transform;
        }

        private void Update()
        {
            if (!isAttack)
                return;

            if (m_LinkManager.m_Player == null || Target == null)
                return;

            if (Vector3.Distance(m_BotTransform.position, Target.localPosition) <= m_DistanceLookAt)
            {
                if (Vector3.Distance(m_BotTransform.position, Target.localPosition) <= m_DistanceAttack)
                    Attack();
            }
        }

        private void Attack()
        {
            m_BotUnit.Shoot(m_BotUnit);
        }

        public void SetTarget(Transform player)
        {
            Target = player;
        }
    }
}