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
    
        public ModeCanShooting m_ModeCanShooting = ModeCanShooting.Shooting;
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
                if (m_ModeCanShooting == ModeCanShooting.Shooting)
                    StartCoroutine(m_LinkManager.ShootingController.Shooting(unit));
            }
            else
            {
                if (m_ModeCanShooting == ModeCanShooting.NoAmmo)
                    StartCoroutine(m_LinkManager.ShootingController.NoAmmo(unit));
            }
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
}
