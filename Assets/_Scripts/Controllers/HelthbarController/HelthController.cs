using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.HelthbarController
{
    public class HelthController : MonoBehaviour
    {
        private Color m_Enemy = Color.red;
        private Color m_Friendly = Color.green;
    
        public void CheckLiveUnit(Unit.UnitController unit, Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            if (damagedUnit.health <= 0)
                LinkManager.instance.deadController.UnitDead(unit, damagedUnit, weapon);
        }
    
        public void SetColorBar(Unit.UnitController unit)
        {
            if (unit.isEnemy)
                unit.helthbarUnit.helthbar.color = m_Enemy;
            else
                unit.helthbarUnit.helthbar.color = m_Friendly;
        }
    }
}