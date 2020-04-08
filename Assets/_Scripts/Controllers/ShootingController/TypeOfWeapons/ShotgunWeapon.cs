using System.Collections.Generic;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public class ShotgunWeapon : ShotLogic
    {
        public override void Logic()
        {
            if (!shootingCheck.isSemiGun)
            {
                shootingCheck.StartCoroutine(shootingCheck.NextShot());
                shootingCheck.DisableShooting();
                shootingCheck.isSemiGun = true;
            }
        }
    }
}