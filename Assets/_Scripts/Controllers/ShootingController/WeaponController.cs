using Managers;
using ScriptableObjects;
using Unit;
using UnityEngine;

namespace Controllers.ShootingController
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private DataWeapons[] m_DataWeapons;
        [SerializeField] private AudioSource m_AudioSource;
        [SerializeField] private AudioClip m_NextWeapon;

        private void Start()
        {
            LinkManager.instance.eventsManager.OnNextWeapon += NextWeapon;
        }

        public void SetWeaponParameters(int idWeapon, Unit.UnitController unit)
        {
            LinkManager linkManager = LinkManager.instance;
            DataWeapons weapon = m_DataWeapons[idWeapon];
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
            
            SetBullets(bulletsQuantity, weapon);
            unit.shootingCheck.SetShootLogic(unit);

            if (unit.isBot)
                return;
        
            SetUIWeaponParameters(linkManager, bulletsQuantity, weapon);
            bulletsQuantity.allWeaponUnit[bulletsQuantity.lastWeapon].SetActive(false);
            bulletsQuantity.allWeaponUnit[bulletsQuantity.currentWeapon.IdWeapon].SetActive(true);
        }

        private void SetBullets(BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
        {
            bulletsQuantity.allBulletsWeapon = weapon.QuantityAllBulletsWeapon;
            bulletsQuantity.quantityBulletsInClip = weapon.QuantityBulletsInClip;
            bulletsQuantity.defaultBulletsInClip = weapon.QuantityBulletsInClip;
            bulletsQuantity.currentWeapon = weapon;
        }
    
        private void SetUIWeaponParameters (LinkManager linkManager, BulletsQuantityUnit bulletsQuantity, DataWeapons weapon)
        {
            linkManager.uiManager.SetQuantityBullets(bulletsQuantity);
            linkManager.uiManager.SetWeaponIcon(weapon.Icon);
        }
    
        private void NextWeapon(Unit.UnitController unit)
        {
            BulletsQuantityUnit bulletsQuantity = unit.bulletsQuantity;
            
            m_AudioSource.PlayOneShot(m_NextWeapon);
            bulletsQuantity.lastWeapon = bulletsQuantity.currentWeapon.IdWeapon;
            bulletsQuantity.nextWeapon = bulletsQuantity.currentWeapon.IdWeapon + 1;
        
            if (bulletsQuantity.nextWeapon >= m_DataWeapons.Length)
                bulletsQuantity.nextWeapon = 0;
        
            SetWeaponParameters(bulletsQuantity.nextWeapon, unit);
        }

    }
}