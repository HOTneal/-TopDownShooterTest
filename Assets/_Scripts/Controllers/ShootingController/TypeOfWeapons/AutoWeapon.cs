namespace Controllers.ShootingController.TypeOfWeapons
{
    public class AutoWeapon : ShotLogic
    {
        public override void DelayForNextShot()
        {
            shootingCheck.DisableShooting();
            shootingCheck.StartCoroutine(shootingCheck.NextShot());
        }

        public override void TakeBulletFromPool(Unit.UnitController unit)
        {
            StartRaycast(unit, 0);
            TakeBulletFromPool(unit, 0);
        }
    }
}
