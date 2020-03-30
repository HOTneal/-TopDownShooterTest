using Controllers;
using Controllers.BulletsController;
using Controllers.HelthbarController;
using Controllers.MobileControllers;
using Controllers.RespawnController;
using Controllers.ShootingController;
using Unit;
using UnityEngine;

namespace Managers
{
    public class LinkManager : MonoBehaviour
    {
        #region Singltone
        private static LinkManager _instance;
        public static LinkManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public Unit.Unit m_Player;
        public MobileInputController MobilePlayerController;
        public ShootingController ShootingController;
        public RespawnController RespawnController;
        public BulletController BulletController;
        public WeaponController WeaponController;
        public DamageController DamageController;
        public HelthController HelthController;
        public DeadController DeadController;
        public EventsManager EventsManager;
        public UnitsHolder UnitsHolder;
        public UIManager UIManager;
    }
}
