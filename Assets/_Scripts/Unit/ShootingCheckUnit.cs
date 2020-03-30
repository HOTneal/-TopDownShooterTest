using Managers;
using UnityEngine;

namespace Unit
{
    public class ShootingCheckUnit : MonoBehaviour
    {
        public enum ModeCanShooting
        {
            Shooting,
            NoShooting,
            NoAmmo
        }
    
        public ModeCanShooting CanShooting = ModeCanShooting.Shooting;
        public float SpeedMoveBullet = 100.0f;
        [HideInInspector] public bool isNoAmmo = false;
        [HideInInspector] public Vector3 BulletTargetPoint;
    
        private RaycastHit m_Hit;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
        }

        public void StartShooting(Unit unit)
        {
            if (!isNoAmmo)
            {
                if (CanShooting == ModeCanShooting.Shooting)
                    StartCoroutine(m_LinkManager.ShootingController.Shooting(unit));
            }
            else
            {
                if (CanShooting == ModeCanShooting.NoAmmo)
                    StartCoroutine(m_LinkManager.ShootingController.NoAmmo(unit));
            }
        }
    
        public void EnableShooting()
        {
            CanShooting = ModeCanShooting.Shooting;
        }
    
        public void DisableShooting()
        {
            CanShooting = ModeCanShooting.NoShooting;
        }
    }
}
