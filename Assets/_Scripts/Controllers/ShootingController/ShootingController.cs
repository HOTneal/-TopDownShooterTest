using System.Collections;
using Controllers.BulletsController;
using Managers;
using ScriptableObjects;
using Unit;
using UnityEngine;

namespace Controllers.ShootingController
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private GameObject m_Bullet;
        [SerializeField] private AudioSource m_AudioSourceShooting;
        [SerializeField] private AudioClip m_SoundNoAmmo;

        private RaycastHit m_Hit;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
        }

        public IEnumerator Shooting(Unit.Unit unit)
        {
            DataWeapons weapon = unit.BulletsQuantity.CurrentWeapon;
        
            unit.ShootingCheck.DisableShooting();
            m_LinkManager.BulletController.BulletsCount(unit);
            StartRaycast(unit);
            GenerateBullets(weapon, unit);
            AnimShooting(unit,true);
            m_AudioSourceShooting.PlayOneShot(weapon.SoundShot);

            yield return new WaitForSeconds(weapon.SpeedShoot);
        
            unit.ShootingCheck.EnableShooting();
            EndShooting(unit);
        }

        private void EndShooting(Unit.Unit unit)
        {
            AnimShooting(unit,false);
        }
    
        private void StartRaycast(Unit.Unit unit)
        {
            if (Physics.Raycast(unit.PointForGenerateBullets.position, unit.PointForGenerateBullets.forward, out m_Hit))
            {
                unit.ShootingCheck.BulletTargetPoint = m_Hit.point;

                if (m_Hit.transform.TryGetComponent(out Unit.Unit damagedUnit))
                    Damage(unit, damagedUnit);
            }
        }

        private void GenerateBullets(DataWeapons weapon, Unit.Unit unit)
        {
            var offsetBulletPos = 0.0f;
            var marginBetweenBullets = 0.3f;
            ShootingCheckUnit shootingCheck = unit.ShootingCheck;
        
            for (int i = 0; i < weapon.QuantityBulletsPerShot; i++)
            {
                var bullet = Instantiate(m_Bullet, unit.PointForGenerateBullets.position, Quaternion.identity);
                var bulletPos = bullet.transform.position;
                var bulletMove = bullet.GetComponent<BulletMove>();
            
                bullet.transform.position = new Vector3(bulletPos.x - offsetBulletPos, bulletPos.y, bulletPos.z);
                bulletMove.TargetPos = new Vector3(shootingCheck.BulletTargetPoint.x - offsetBulletPos, shootingCheck.BulletTargetPoint.y, shootingCheck.BulletTargetPoint.z);
                bulletMove.Speed = shootingCheck.SpeedMoveBullet;

                offsetBulletPos -= marginBetweenBullets;
            }
        }

        public IEnumerator NoAmmo(Unit.Unit unit)
        {
            unit.ShootingCheck.DisableShooting();
            m_AudioSourceShooting.PlayOneShot(m_SoundNoAmmo);
        
            yield return new WaitForSeconds(0.3f);
        
            unit.ShootingCheck.CanShooting = ShootingCheckUnit.ModeCanShooting.NoAmmo;
        }

        private void AnimShooting(Unit.Unit unit, bool value)
        {
            unit.Animator.SetBool(unit.BulletsQuantity.CurrentWeapon.NameAnim, value);
        }

        private void Damage(Unit.Unit unit, Unit.Unit damagedUnit)
        {
            m_LinkManager.DamageController.Damage(unit, damagedUnit, unit.BulletsQuantity.CurrentWeapon);
            m_LinkManager.HelthController.CheckLiveUnit(unit, damagedUnit, unit.BulletsQuantity.CurrentWeapon);
        }
    }
}