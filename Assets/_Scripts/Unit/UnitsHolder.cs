using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitsHolder : MonoBehaviour
    {
        public List<Unit> Units = new List<Unit>();

        public void AddUnitInHolder(Unit unit)
        {
            Units.Add(unit);
        }

        public void DeleteUnitFromHolder(Unit unitDelete)
        {
            var find = Units.Find(unit => unit == unitDelete);
            Units.Remove(find);
        }
    }
}