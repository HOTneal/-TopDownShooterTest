﻿using Controllers;
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
        public GrenadeInstantiateController GrenadeInstantiateController;
        public MobileGrenadeController MobileGrenadeController;
        public LineRendererController LineRendererController;
        public MobileInputController MobilePlayerController;
        public KillListController KillListController;
        public ShootingController ShootingController;
        public RespawnController RespawnController;
        public GrenadeController GrenadeController;
        public BulletController BulletController;
        public WeaponController WeaponController;
        public DamageController DamageController;
        public HelthController HelthController;
        public DeadController DeadController;
        public EventsManager EventsManager;
        public MobileButtons MobileButtons;
        public UnitsHolder UnitsHolder;
        public UIManager UIManager;
    }
}
