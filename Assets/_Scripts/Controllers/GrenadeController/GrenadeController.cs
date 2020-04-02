using System.Collections;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeController : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 20.0f;
        [SerializeField] private float m_ExplodeTime = 1.0f;
        [SerializeField] private DataWeapons m_Grenade;
        [SerializeField] private GrenadeEffects m_Effects;

        private LinkManager m_LinkManager;
    
        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
        }
    
        public IEnumerator ExplodeGrenade(Transform grenade)
        {
            yield return new WaitForSeconds(m_ExplodeTime);
            
            Collider[] allUnitsInRadius = Physics.OverlapSphere(grenade.position, m_Radius);
        
            foreach (var nearbyObj in allUnitsInRadius)
            {
                if (nearbyObj.TryGetComponent(out Unit.Unit unit))
                    RayCastForDamage(unit, grenade);
            }
        
            m_Effects.GenerateEffects(grenade);
            Destroy(grenade.gameObject);
        }

        private void RayCastForDamage(Unit.Unit nearbyUnit, Transform grenade)
        {
            RaycastHit hit;
            Transform nearbyUnitTarget = nearbyUnit.PointForDamage;
            Vector3 pos = nearbyUnitTarget.position - grenade.position;
            float distance = pos.magnitude;
            Vector3 targetDirection = pos / distance;
        
            if (Physics.Raycast(grenade.position, targetDirection, out hit))
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
}