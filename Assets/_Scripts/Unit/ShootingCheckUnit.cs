using Managers;
using UnityEngine;

namespace Unit
{
    public class ShootingCheckUnit : MonoBehaviour
    {
        public enum ModeCanShooting
        {
            Shooting = 1,
            NoShooting = 2,
            NoAmmo = 3
        }
    
        public ModeCanShooting canShooting = ModeCanShooting.Shooting;
        public float speedMoveBullet = 100.0f;
        [HideInInspector] public bool isNoAmmo = false;
        [HideInInspector] public Vector3 bulletTargetPoint;
    
        private RaycastHit m_Hit;
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }

        public void StartShooting(Unit unit)
        {
            if (!isNoAmmo)
            {
                if (canShooting == ModeCanShooting.Shooting)
                    StartCoroutine(m_LinkManager.shootingController.Shooting(unit));
            }
            else
            {
                if (canShooting == ModeCanShooting.NoAmmo)
                    StartCoroutine(m_LinkManager.shootingController.NoAmmo(unit));
            }
        }
    
        public void EnableShooting()
        {
            canShooting = ModeCanShooting.Shooting;
        }
    
        public void DisableShooting()
        {
            canShooting = ModeCanShooting.NoShooting;
        }
    }
}
