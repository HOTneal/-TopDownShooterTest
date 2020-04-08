using UnityEngine;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public class ShotLogic : MonoBehaviour
    {
        [HideInInspector] public ShootingCheck shootingCheck;
        public virtual void Logic() {}
    }
}