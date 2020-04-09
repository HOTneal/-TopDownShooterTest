using UnityEngine;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public abstract class ShotLogic : MonoBehaviour
    {
        [HideInInspector] public ShootingCheck shootingCheck;
        public abstract void Logic();
    }
}