using System;
using Managers;

namespace Controllers.ShootingController.TypeOfWeapons
{
    public class ShotgunWeapon : ShotLogic
    {
        private float marginBetweenBullets = 0.7f;

        public override void DelayForNextShot()
        {
            if (!shootingCheck.isSemiGun)
            {
                shootingCheck.StartCoroutine(shootingCheck.NextShot());
                shootingCheck.DisableShooting();
                shootingCheck.isSemiGun = true;
            }
        }

        public override void GenerateBullets(Unit.UnitController unit)
        {
            var offsetBulletPos = 0.0f;
            for (int i = 1; i <= unit.bulletsQuantity.currentWeapon.QuantityBulletsPerShot; i++)
            {
                StartRaycast(unit, offsetBulletPos);
                GenerateBullets(unit, offsetBulletPos);
                offsetBulletPos = IdentifyMultipleBullets(unit, i, offsetBulletPos);
            }
        }

        private float IdentifyMultipleBullets(Unit.UnitController unit, int i, float offsetBulletPos)
        {
            var centerBullet = Math.Truncate(unit.bulletsQuantity.currentWeapon.QuantityBulletsPerShot / 2.0f);
            
            if (i < centerBullet)
                offsetBulletPos -= marginBetweenBullets;
            else
            {
                if (i == centerBullet)
                    offsetBulletPos = 0.0f;
   
                offsetBulletPos += marginBetweenBullets;
            }

            return offsetBulletPos;
        }
    }
}