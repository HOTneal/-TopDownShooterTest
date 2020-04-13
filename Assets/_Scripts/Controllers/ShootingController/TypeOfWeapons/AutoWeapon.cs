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

        private void StartRaycast(Unit.UnitController unit)
        {
            LinkManager.instance.shootingController.StartRaycast(unit, 0);
        }

        public override void GenerateBullets(Unit.UnitController unit)
        {
            StartRaycast(unit);
            LinkManager.instance.shootingController.GenerateBullets(unit, 0);
        }
    }
}
