using System.Collections;
using System.Collections.Generic;
using Controllers.BulletsController;
using Managers;
using ScriptableObjects;
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
            m_LinkManager = LinkManager.instance;
        }

        public void Shooting(Unit.UnitController unit)
        {
            DataWeapons weapon = unit.bulletsQuantity.currentWeapon;
            
            m_LinkManager.bulletController.BulletsCount(unit);
            StartRaycast(unit);
            GenerateBullets(weapon, unit);
            AnimShooting(unit,true);
            m_AudioSourceShooting.PlayOneShot(weapon.SoundShot);
        }

        public void EndShooting(Unit.UnitController unit)
        {
            AnimShooting(unit,false);
        }
    
        private void StartRaycast(Unit.UnitController unit)
        {
            if (Physics.Raycast(unit.pointForGenerateBullets.position, unit.pointForGenerateBullets.forward, out m_Hit))
            {
                unit.shootingCheck.bulletTargetPoint = m_Hit.point;

                if (m_Hit.transform.TryGetComponent(out Unit.UnitController damagedUnit))
                    Damage(unit, damagedUnit);
            }
        }

        private void GenerateBullets(DataWeapons weapon, Unit.UnitController unit)
        {
            var offsetBulletPos = 0.0f;
            var marginBetweenBullets = 0.3f;
            ShootingCheck shootingCheck = unit.shootingCheck;
        
            for (int i = 0; i < weapon.QuantityBulletsPerShot; i++)
            {
                var bullet = Instantiate(m_Bullet, unit.pointForGenerateBullets.position, Quaternion.identity);
                var bulletPos = bullet.transform.position;
                var bulletMove = bullet.GetComponent<BulletMove>();
            
                bullet.transform.position = new Vector3(bulletPos.x - offsetBulletPos, bulletPos.y, bulletPos.z);
                bulletMove.targetPos = new Vector3(shootingCheck.bulletTargetPoint.x - offsetBulletPos, shootingCheck.bulletTargetPoint.y, shootingCheck.bulletTargetPoint.z);
                bulletMove.speed = shootingCheck.speedMoveBullet;

                offsetBulletPos -= marginBetweenBullets;
            }
        }

        public IEnumerator NoAmmo(Unit.UnitController unit)
        {
            m_AudioSourceShooting.PlayOneShot(m_SoundNoAmmo);
        
            yield return new WaitForSeconds(0.3f);
            
            unit.shootingCheck.isNoAmmo = true;
            unit.shootingCheck.EnableShooting();
        }

        private void AnimShooting(Unit.UnitController unit, bool value)
        {
            unit.animator.SetBool(unit.bulletsQuantity.currentWeapon.NameAnim, value);
        }

        private void Damage(Unit.UnitController unit, Unit.UnitController damagedUnit)
        {
            m_LinkManager.damageController.Damage(unit, damagedUnit, unit.bulletsQuantity.currentWeapon);
            m_LinkManager.helthController.CheckLiveUnit(unit, damagedUnit, unit.bulletsQuantity.currentWeapon);
        }
    }
}