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
            m_LinkManager = LinkManager.Instance;
        }

        public void UnitDead(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
        {
            m_LinkManager.KillListController.AddKillToList(unit, damagedUnit, weapon);
            if (!damagedUnit.isBot)
                PlayerDead(damagedUnit);
            else
                BotDead(damagedUnit);
        }

        private void Respawn(Unit.Unit unit)
        {
            StartCoroutine(m_LinkManager.RespawnController.Respawn(unit, m_DefalutTimeRespawn));
        }
    
        private void PlayerDead(Unit.Unit unit)
        {
            m_LinkManager.UIManager.DeadPanel(true);
            m_LinkManager.UIManager.DarkPanel(0.8f, true);
            m_LinkManager.UIManager.Interface(false);
            m_LinkManager.UnitsHolder.DeleteUnitFromHolder(unit);
            m_LinkManager.MobileGrenadeController.isCanMove = false;
            Destroy(unit.gameObject);
        }

        private void BotDead(Unit.Unit unit)
        {
            Respawn(unit);
            m_LinkManager.UnitsHolder.DeleteUnitFromHolder(unit);
            Destroy(unit.gameObject);
        }
    }
}
