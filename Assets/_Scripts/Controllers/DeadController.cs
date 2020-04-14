using Managers;
using ScriptableObjects;
using Unit;
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

        public void UnitDead(Unit.UnitController unit, Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            m_LinkManager.killListController.AddKillToList(unit, damagedUnit, weapon);
            if (!damagedUnit.isBot)
                PlayerDead(damagedUnit);
            else
                BotDead(damagedUnit);
        }

        private void Respawn(Unit.UnitController unit)
        {
            StartCoroutine(m_LinkManager.respawnController.Respawn(unit, m_DefalutTimeRespawn));
        }
    
        private void PlayerDead(Unit.UnitController unit)
        {
            m_LinkManager.uiManager.DeadPanel(true);
            m_LinkManager.uiManager.DarkPanel(0.8f, true);
            m_LinkManager.uiManager.Interface(false);
            m_LinkManager.unitsHolder.DeleteUnitFromHolder(unit);
            m_LinkManager.mobileGrenadeController.isCanMove = false;
            ReturnUnits(unit);
        }

        private void BotDead(Unit.UnitController unit)
        {
            Respawn(unit);
            m_LinkManager.unitsHolder.DeleteUnitFromHolder(unit);
            ReturnUnits(unit);
        }

        private void ReturnUnits(UnitController unit)
        {
            unit.gameObject.SetActive(false);
            m_LinkManager.playerPool.AddInList(unit);
            unit.ResetParameters();
        }
    }
}
