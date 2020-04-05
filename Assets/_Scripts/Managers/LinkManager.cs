using Controllers;
using Controllers.BulletsController;
using Controllers.GrenadeController;
using Controllers.HelthbarController;
using Controllers.KillListController;
using Controllers.LineRendererController;
using Controllers.MobileController;
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
        public static LinkManager instance => _instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public Unit.Unit player;
        public GrenadeInstantiateController grenadeInstantiateController;
        public MobileGrenadeController mobileGrenadeController;
        public LineRendererController lineRendererController;
        public MobileInputController mobilePlayerController;
        public KillListController killListController;
        public ShootingController shootingController;
        public RespawnController respawnController;
        public GrenadeController grenadeController;
        public BulletController bulletController;
        public WeaponController weaponController;
        public DamageController damageController;
        public HelthController helthController;
        public DeadController deadController;
        public EventsManager eventsManager;
        public MobileButtons mobileButtons;
        public UnitsHolder unitsHolder;
        public UIManager uiManager;
    }
}
