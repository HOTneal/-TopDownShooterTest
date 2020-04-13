using Managers;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public class AutoWeapon : ShotLogic
    {
        public override void DelayForNextShot()
        {
            shootingCheck.DisableShooting();
            shootingCheck.StartCoroutine(shootingCheck.NextShot());
        }

        public override void GenerateBullets(Unit.UnitController unit)
        {
            StartRaycast(unit, 0);
            GenerateBullets(unit, 0);
        }
    }
}
