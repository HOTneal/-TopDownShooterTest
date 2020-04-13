using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public abstract class ShotLogic : MonoBehaviour
    {
        public ShootingCheck shootingCheck;

        public abstract void DelayForNextShot();

        public abstract void GenerateBullets(Unit.UnitController unit);
    }
}