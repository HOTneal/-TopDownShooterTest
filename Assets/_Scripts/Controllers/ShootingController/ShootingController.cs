using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public IEnumerator Shooting(Unit unit)
    {
        DataWeapons weapon = unit.m_BulletsQuantity.m_CurrentWeapon;
        unit.m_ShootingCheck.DisableShooting();
        m_LinkManager.m_BulletController.BulletsCount(unit);
        m_LinkManager.m_UIManager.SetQuantityBullets();
        StartRaycast(unit);
        GenerateBullets(weapon, unit);
        AnimShooting(unit,true);
        m_AudioSourceShooting.PlayOneShot(weapon.SoundShot);
        
        yield return new WaitForSeconds(weapon.SpeedShoot);
        
        unit.m_ShootingCheck.EnableShooting();
        EndShooting(unit);
    }

    private void EndShooting(Unit unit)
    {
        AnimShooting(unit,false);
    }
    
    private void StartRaycast(Unit unit)
    {
        if (Physics.Raycast(unit.m_PointForGenerateBullets.position, unit.m_PointForGenerateBullets.forward, out m_Hit))
        {
            Unit damageUnit = new Unit();

            if (m_Hit.transform.TryGetComponent(out Unit returnUnit))
                damageUnit = returnUnit;

            unit.m_ShootingCheck.m_BulletTargetPoint = m_Hit.point;
            m_LinkManager.m_DamageController.Damage(unit, damageUnit);
        }
    }

    private void GenerateBullets(DataWeapons weapon, Unit unit)
    {
        var offsetBulletPos = 0.0f;
        var marginBetweenBullets = 0.3f;
        ShootingCheck shootingCheck = unit.m_ShootingCheck;
        
        for (int i = 0; i < weapon.QuantityBulletsPerShot; i++)
        {
            var bullet = Instantiate(m_Bullet, unit.m_PointForGenerateBullets.position, Quaternion.identity);
            var bulletPos = bullet.transform.position;
            var bulletMove = bullet.GetComponent<BulletMove>();
            
            bullet.transform.position = new Vector3(bulletPos.x - offsetBulletPos, bulletPos.y, bulletPos.z);
            bulletMove.m_TargetPos = new Vector3(shootingCheck.m_BulletTargetPoint.x - offsetBulletPos, shootingCheck.m_BulletTargetPoint.y, shootingCheck.m_BulletTargetPoint.z);
            bulletMove.m_Speed = shootingCheck.m_SpeedMoveBullet;

            offsetBulletPos -= marginBetweenBullets;
        }
    }

    public IEnumerator NoAmmo(Unit unit)
    {
        unit.m_ShootingCheck.DisableShooting();
        m_AudioSourceShooting.PlayOneShot(m_SoundNoAmmo);
        
        yield return new WaitForSeconds(0.3f);
        
        unit.m_ShootingCheck.m_ModeCanShooting = ShootingCheck.ModeCanShooting.NoAmmo;
    }

    private void AnimShooting(Unit unit, bool value)
    {
        unit.m_Animator.SetBool(unit.m_BulletsQuantity.m_CurrentWeapon.NameAnim, value);
    }
}