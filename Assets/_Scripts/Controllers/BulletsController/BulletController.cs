using System.Collections;
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

        public void BulletsCount(Unit.Unit unit)
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

        private IEnumerator ReloadWeapon(Unit.Unit unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
            ShootingCheckUnit shootingCheck = unit.shootingCheck;
            Animator animator = unit.animator;
        
            m_LinkManager.shootingController.StopAllCoroutines();
            
            shootingCheck.StopAllCoroutines();
            shootingCheck.DisableShooting();
            
            animator.SetTrigger("ReloadGun");
            
            m_AudioSource.PlayOneShot(m_SoundReload);
        
            yield return new WaitForSeconds(bulletsQuantity.currentWeapon.ReloadTime);
        
            if (!bulletsQuantity.currentWeapon.InfinityBullets)
                bulletsQuantity.allBulletsWeapon -= bulletsQuantity.defaultBulletsInClip;
        
            m_AudioSource.Stop();
            
            bulletsQuantity.quantityBulletsInClip = bulletsQuantity.defaultBulletsInClip;
            shootingCheck.EnableShooting();

            SetUIWeaponParameters(unit);
        }

        private void NoBullets(Unit.Unit unit)
        {
            unit.shootingCheck.isNoAmmo = true;
            unit.shootingCheck.canShooting = ShootingCheckUnit.ModeCanShooting.NoAmmo;
        }
    
        private void SetUIWeaponParameters(Unit.Unit unit)
        {
            if (!unit.isBot)
                m_LinkManager.uiManager.SetQuantityBullets(unit.bulletsQuantity);
        }
    }
}