using Managers;
using UnityEngine;

namespace Controllers.HelthbarController
{
    public class HelthController : MonoBehaviour
    {
        private Color m_Enemy = Color.red;
        private Color m_Friendly = Color.green;
    
        public void CheckLiveUnit(Unit.Unit unit, Unit.Unit damagedUnit)
        {
            if (damagedUnit.Helth <= 0)
                LinkManager.Instance.DeadController.UnitDead(unit, damagedUnit);
        }
    
        public void SetColorBar(Unit.Unit unit)
        {
            if (unit.isEnemy)
                unit.HelthbarUnit.Helthbar.color = m_Enemy;
            else
                unit.HelthbarUnit.Helthbar.color = m_Friendly;
        }
    }
}