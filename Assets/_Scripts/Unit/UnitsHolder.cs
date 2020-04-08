using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitsHolder : MonoBehaviour
    {
        public List<UnitController> Units = new List<UnitController>();

        public void AddUnitInHolder(UnitController unit)
        {
            Units.Add(unit);
        }

        public void DeleteUnitFromHolder(UnitController unitDelete)
        {
            var find = Units.Find(unit => unit == unitDelete);
            Units.Remove(find);
        }
    }
}