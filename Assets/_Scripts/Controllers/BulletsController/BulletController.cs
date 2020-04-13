using System.Collections;
using Controllers.ShootingController;
using Managers;
using Unit;
using UnityEngine;

namespace Controllers.BulletsController
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private AudioClip m_SoundReload;
        [SerializeField] private AudioSource m_AudioSource;

        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }

        public void BulletsCount(Unit.UnitController unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
            if (bulletsQuantity.quantityBulletsInClip == 0) return;
        
            bulletsQuantity.quantityBulletsInClip--;
        
            if (bulletsQuantity.quantityBulletsInClip == 0 & bulletsQuantity.allBulletsWeapon != 0)
                StartCoroutine(ReloadWeapon(unit));
            
            if (bulletsQuantity.quantityBulletsInClip == 0 & bulletsQuantity.allBulletsWeapon == 0)
                NoBullets(unit);
        
            SetUIWeaponParameters(unit);
        }

        private IEnumerator ReloadWeapon(Unit.UnitController unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
            ShootingCheck shootingCheck = unit.shootingCheck;
            Animator animator = unit.animator;
        
            m_LinkManager.shootingController.StopAllCoroutines();

            shootingCheck.DisableShooting();
            shootingCheck.isShoot = false;
            shootingCheck.isNoAmmo = true;
            shootingCheck.StopAllCoroutines();
            
            animator.SetTrigger("ReloadGun");
            
            m_AudioSource.PlayOneShot(m_SoundReload);
        
            yield return new WaitForSeconds(bulletsQuantity.currentWeapon.ReloadTime);
        
            if (!bulletsQuantity.currentWeapon.InfinityBullets)
                bulletsQuantity.allBulletsWeapon -= bulletsQuantity.defaultBulletsInClip;
        
            m_AudioSource.Stop();
            
            bulletsQuantity.quantityBulletsInClip = bulletsQuantity.defaultBulletsInClip;

            SetUIWeaponParameters(unit);
            shootingCheck.isNoAmmo = false;
            shootingCheck.isShoot = true;
        }

        private void NoBullets(Unit.UnitController unit)
        {
            unit.shootingCheck.isNoAmmo = true;
            unit.shootingCheck.NoAmmo();
        }
    
        private void SetUIWeaponParameters(Unit.UnitController unit)
        {
            if (!unit.isBot)
                m_LinkManager.uiManager.SetQuantityBullets(unit.bulletsQuantity);
        }
    }
}