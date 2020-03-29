using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsHolder : MonoBehaviour
{
    public List<Unit> m_Units = new List<Unit>();

    public void AddUnitInHolder(Unit unit)
    {
        m_Units.Add(unit);
    }

    public void DeleteUnitFromHolder(Unit unitDelete)
    {
        var find = m_Units.Find(unit => unit == unitDelete);
        m_Units.Remove(find);
    }
}