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
            m_LinkManager = LinkManager.Instance;
        }

        public void BulletsCount(Unit.Unit unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
            if (bulletsQuantity.QuantityBulletsInClip == 0) return;
        
            bulletsQuantity.QuantityBulletsInClip--;
        
            if (bulletsQuantity.QuantityBulletsInClip == 0 & bulletsQuantity.AllBulletsWeapon != 0)
                StartCoroutine(ReloadWeapon(unit));
            
            if (bulletsQuantity.QuantityBulletsInClip == 0 & bulletsQuantity.AllBulletsWeapon == 0)
                NoBullets(unit);
        
            SetUIWeaponParameters(unit);
        }

        private IEnumerator ReloadWeapon(Unit.Unit unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.BulletsQuantity;
            ShootingCheckUnit shootingCheck = unit.ShootingCheck;
            Animator animator = unit.Animator;
        
            m_LinkManager.ShootingController.StopAllCoroutines();
            shootingCheck.StopAllCoroutines();
            shootingCheck.DisableShooting();
            animator.SetTrigger("ReloadGun");
            m_AudioSource.PlayOneShot(m_SoundReload);
        
            yield return new WaitForSeconds(bulletsQuantity.CurrentWeapon.ReloadTime);
        
            if (!bulletsQuantity.CurrentWeapon.InfinityBullets)
                bulletsQuantity.AllBulletsWeapon -= bulletsQuantity.DefaultBulletsInClip;
        
            m_AudioSource.Stop();
            bulletsQuantity.QuantityBulletsInClip = bulletsQuantity.DefaultBulletsInClip;
            shootingCheck.EnableShooting();

            SetUIWeaponParameters(unit);
        }

        private void NoBullets(Unit.Unit unit)
        {
            unit.ShootingCheck.isNoAmmo = true;
            unit.ShootingCheck.CanShooting = ShootingCheckUnit.ModeCanShooting.NoAmmo;
        }
    
        private void SetUIWeaponParameters(Unit.Unit unit)
        {
            if (!unit.isBot)
                m_LinkManager.UIManager.SetQuantityBullets(unit.BulletsQuantity);
        }
    }
}