using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.HelthbarController
{
    public class HelthController : MonoBehaviour
    {
        private Color m_Enemy = Color.red;
        private Color m_Friendly = Color.green;
    
        public void CheckLiveUnit(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
        {
            if (damagedUnit.helth <= 0)
                LinkManager.instance.deadController.UnitDead(unit, damagedUnit, weapon);
        }
    
        public void SetColorBar(Unit.Unit unit)
        {
            if (unit.isEnemy)
                unit.helthbarUnit.Helthbar.color = m_Enemy;
            else
                unit.helthbarUnit.Helthbar.color = m_Friendly;
        }
    }
}