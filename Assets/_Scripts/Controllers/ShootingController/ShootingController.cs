using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public enum ModeCanShooting
    {
        Shooting,
        NoShooting,
        NoAmmo
    }
    
    [SerializeField] private Transform m_PointStartRaycast;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private AudioSource m_AudioSourceShooting;
    [SerializeField] private AudioClip m_SoundNoAmmo;
    
    [HideInInspector] public DataWeapons m_CurrentWeapon;
    [SerializeField] public ModeCanShooting m_ModeCanShooting = ModeCanShooting.Shooting;
    [HideInInspector] public bool isNoAmmo = false;
    
    private Vector3 m_BulletTargetPoint;
    private RaycastHit m_Hit;
    private float m_SpeedMoveBullet = 100.0f;
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_LinkManager.m_EventsManager.OnStartPlayerShooting += StartShooting;
        m_LinkManager.m_EventsManager.OnEndPlayerShooting += EndShooting;
    }

    public void StartShooting(DataWeapons weapon, Unit unit)
    {
        if (!isNoAmmo)
        {
            if (m_ModeCanShooting == ModeCanShooting.Shooting)
                StartCoroutine(Shooting(weapon, unit));
        }
        else
        {
            if (m_ModeCanShooting == ModeCanShooting.NoAmmo)
                StartCoroutine(NoAmmo());
        }
    }
    
    private IEnumerator Shooting(DataWeapons weapon, Unit unit)
    {
        print(unit.m_Nickname);

        DisableShooting();
        m_LinkManager.m_BulletController.BulletsCount(weapon);
        m_LinkManager.m_UIManager.SetQuantityBullets();
        StartRaycast();
        GenerateBullets(weapon);
        AnimShooting(unit, weapon ,true);
        m_AudioSourceShooting.PlayOneShot(weapon.SoundShot);
        yield return new WaitForSeconds(weapon.SpeedShoot);
        EnableShooting();
    }

    private void EndShooting(DataWeapons weapon, Unit unit)
    {
        AnimShooting(unit, weapon,false);
    }
    
    private void StartRaycast()
    {
        if (Physics.Raycast(m_PointStartRaycast.position, m_PointStartRaycast.forward, out m_Hit))
        {
            //print(m_Hit.transform.name);
            m_BulletTargetPoint = m_Hit.point;
        }
    }

    private void GenerateBullets(DataWeapons weapon)
    {
        float offsetBulletPos = 0.0f;
        float marginBetweenBullets = 0.3f;
        for (int i = 0; i < weapon.QuantityBulletsPerShot; i++)
        {
            var bullet = Instantiate(m_Bullet, m_PointStartRaycast.position, Quaternion.identity);
            var bulletPos = bullet.transform.position;
            var bulletMove = bullet.GetComponent<BulletMove>();
            
            bullet.transform.position = new Vector3(bulletPos.x - offsetBulletPos, bulletPos.y, bulletPos.z);
            bulletMove.m_TargetPos = new Vector3(m_BulletTargetPoint.x - offsetBulletPos, m_BulletTargetPoint.y, m_BulletTargetPoint.z);
            bulletMove.m_Speed = m_SpeedMoveBullet;

            offsetBulletPos -= marginBetweenBullets;
        }
    }

    private IEnumerator NoAmmo()
    {
        DisableShooting();
        m_AudioSourceShooting.PlayOneShot(m_SoundNoAmmo);
        yield return new WaitForSeconds(0.3f);
        m_ModeCanShooting = ModeCanShooting.NoAmmo;
    }

    private void AnimShooting(Unit unit, DataWeapons weapon, bool value)
    {
        unit.m_Animator.SetBool(weapon.NameAnim, value);
    }

    public void EnableShooting()
    {
        m_ModeCanShooting = ModeCanShooting.Shooting;
    }
    
    public void DisableShooting()
    {
        m_ModeCanShooting = ModeCanShooting.NoShooting;
    }
}