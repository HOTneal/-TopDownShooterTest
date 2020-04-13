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
            HelthCheck helthCheck = damagedUnit.GetComponent<HelthCheck>();
            
            helthCheck.whoShoot = unit;
            helthCheck.damagedWeapon = weapon;
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