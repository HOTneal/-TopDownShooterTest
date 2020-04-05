using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class DeadController : MonoBehaviour
    {
        private float m_DefalutTimeRespawn = 5.0f;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }

        public void UnitDead(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
        {
            m_LinkManager.killListController.AddKillToList(unit, damagedUnit, weapon);
            if (!damagedUnit.isBot)
                PlayerDead(damagedUnit);
            else
                BotDead(damagedUnit);
        }

        private void Respawn(Unit.Unit unit)
        {
            StartCoroutine(m_LinkManager.respawnController.Respawn(unit, m_DefalutTimeRespawn));
        }
    
        private void PlayerDead(Unit.Unit unit)
        {
            m_LinkManager.uiManager.DeadPanel(true);
            m_LinkManager.uiManager.DarkPanel(0.8f, true);
            m_LinkManager.uiManager.Interface(false);
            m_LinkManager.unitsHolder.DeleteUnitFromHolder(unit);
            m_LinkManager.mobileGrenadeController.isCanMove = false;
            Destroy(unit.gameObject);
        }

        private void BotDead(Unit.Unit unit)
        {
            Respawn(unit);
            m_LinkManager.unitsHolder.DeleteUnitFromHolder(unit);
            Destroy(unit.gameObject);
        }
    }
}
