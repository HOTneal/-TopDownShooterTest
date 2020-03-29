using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthController : MonoBehaviour
{
    private Color m_Enemy = Color.red;
    private Color m_Friendly = Color.green;
    public void CheckLiveUnit(Unit unit)
    {
        if (unit.m_Helth <= 0)
            LinkManager.Instance.m_DeadController.UnitDead(unit);
    }
    
    public void SetColorBar(Unit unit)
    {
        if (unit.isEnemy)
            unit.m_HelthbarUnit.m_Helthbar.color = m_Enemy;
        else
            unit.m_HelthbarUnit.m_Helthbar.color = m_Friendly;
    }
}