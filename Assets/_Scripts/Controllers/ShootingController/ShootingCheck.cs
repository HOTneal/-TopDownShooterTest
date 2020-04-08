﻿using System;
using System.Collections;
using Controllers.ShootingController.TypeOfWeapons;
using Managers;
using ScriptableObjects;
using Unit;
using UnityEngine;

namespace Controllers.ShootingController
{
    public class ShootingCheck : MonoBehaviour
    {
        public enum ModeCanShooting
        {
            Shooting = 1,
            NoShooting = 2,
            NoAmmo = 3
        }
    
        public ModeCanShooting canShooting = ModeCanShooting.NoShooting;
        public float speedMoveBullet = 100.0f;
        public ShotLogic m_ShotLogic;
        public bool isShoot = true;
        public bool isSemiGun = false;
        [HideInInspector] public bool isNoAmmo = false;
        [HideInInspector] public Vector3 bulletTargetPoint;
    
        private RaycastHit m_Hit;
        private LinkManager m_LinkManager;
        private UnitController m_Unit;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_Unit = GetComponent<UnitController>();
        }

        private void Update()
        {
            if (!isShoot || isSemiGun)
                return;
            
            StartShooting(m_Unit);
        }

        public void StartShooting(Unit.UnitController unit)
        {
            if (!isNoAmmo)
            {
                if (canShooting == ModeCanShooting.Shooting)
                {
                    m_LinkManager.shootingController.Shooting(unit);
                    m_ShotLogic.Logic();
                }
            }
            else
            {
                if (canShooting == ModeCanShooting.NoAmmo)
                {
                    StartCoroutine(m_LinkManager.shootingController.NoAmmo(unit));
                    DisableShooting();
                    isShoot = false;
                }
            }
        }

        public void SetShootLogic(Unit.UnitController unit)
        {
            switch (unit.bulletsQuantity.currentWeapon.TypeShooting)
            {
                case DataWeapons.ChoiceTypeShooting.Automatic:
                    m_ShotLogic = new AutoWeapon();
                    break;
                
                case DataWeapons.ChoiceTypeShooting.Shotgun:
                    m_ShotLogic = new ShotgunWeapon();
                    break;
            }

            m_ShotLogic.shootingCheck = this;
        }
        
        public IEnumerator NextShot()
        {
            isShoot = false;
            yield return new WaitForSeconds(m_Unit.bulletsQuantity.currentWeapon.SpeedShoot);
            EnableShooting();
            isShoot = true;
            m_LinkManager.shootingController.EndShooting(m_Unit);
        }
    
        public void EnableShooting()
        {
            if (!isShoot)
                return;
            
            canShooting = ModeCanShooting.Shooting;
        }
    
        public void DisableShooting()
        {
            canShooting = ModeCanShooting.NoShooting;
            isSemiGun = false;
        }

        public void NoAmmo()
        {
            isNoAmmo = true;
            canShooting = ModeCanShooting.NoAmmo;
        }
    }
}

