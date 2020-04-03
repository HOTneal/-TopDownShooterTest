using Controllers;
using Managers;
using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour
    {
        public string Nickname;
        public int Helth = 100;
        public bool isBot = false;
        public bool isEnemy = false;
        public float SpeedWalk;
        public Transform PointForGenerateBullets;
        public Transform PointForDamage;
        public Transform[] PointsForGrenade;
        [HideInInspector] public int IdDefaultWeaponAtStart = 0;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public CharacterController ChController;
        [HideInInspector] public HelthbarUnit HelthbarUnit;
        [HideInInspector] public BotController BotController;
        [HideInInspector] public SetTarget SetTarget;
        [HideInInspector] public BulletsQuantityUnit BulletsQuantity;
        [HideInInspector] public ShootingCheckUnit ShootingCheck;
        [HideInInspector] public Transform PointForSpawn;
        
        private LinkManager m_LinkManager;
        private MovementController m_MovementController;

        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
            BulletsQuantity = GetComponent<BulletsQuantityUnit>();
            Animator = GetComponent<Animator>();
            ChController = GetComponent<CharacterController>();
            ShootingCheck = GetComponent<ShootingCheckUnit>();
            HelthbarUnit = GetComponent<HelthbarUnit>();
            m_MovementController = GetComponent<MovementController>();

            if (!isBot)
            {
                m_LinkManager.m_Player = this;
                SetPlayerMoveSettings();
                SetTarget = GetComponent<SetTarget>();
                SetTarget.SetTargetForBotAttack();
                SetMobileGrenadeSettings();
            }
            else
                BotController = GetComponent<BotController>();

            SetWeapon();
        }

        private void SetPlayerMoveSettings()
        {
            m_MovementController.SetPlayerMoveSettings(this);
        }
    
        public void Shoot(Unit unit)
        {
            ShootingCheck.StartShooting(unit);
        }
    
        private void SetWeapon()
        {
            m_LinkManager.WeaponController.SetWeaponParameters(IdDefaultWeaponAtStart, this);
        }

        public void NextWeapon()
        {
            m_LinkManager.EventsManager.NextWeapon(this);
        }

        private void SetMobileGrenadeSettings()
        {
            m_LinkManager.MobileGrenadeController.SetValues(PointsForGrenade);
        }
    }
}