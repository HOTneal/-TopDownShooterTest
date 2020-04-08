namespace Controllers.ShootingController.TypeOfWeapons
{
    public class AutoWeapon : ShotLogic
    {
        public override void Logic()
        {
            shootingCheck.DisableShooting();
            shootingCheck.StartCoroutine(shootingCheck.NextShot());
        }
    }
}
