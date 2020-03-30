using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthController : MonoBehaviour
{
    private Color m_Enemy = Color.red;
    private Color m_Friendly = Color.green;
    
    public void CheckLiveUnit(Unit.Unit unit)
    {
        if (unit.Helth <= 0)
            LinkManager.Instance.DeadController.UnitDead(unit);
    }
    
    public void SetColorBar(Unit.Unit unit)
    {
        if (unit.isEnemy)
            unit.HelthbarUnit.Helthbar.color = m_Enemy;
        else
            unit.HelthbarUnit.Helthbar.color = m_Friendly;
    }
}