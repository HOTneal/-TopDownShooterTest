using Controllers;
using Managers;
using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour
    {
        public string nickname;
        public int helth = 100;
        public int idWeaponAtStart = 0;
        public bool isBot = false;
        public bool isEnemy = false;
        public float speedWalk;
        public Transform pointForGenerateBullets;
        public Transform pointForDamage;
        public Transform[] pointsForGrenade;
        [HideInInspector] public BulletsQuantityUnit bulletsQuantity;
        [HideInInspector] public CharacterController chController;
        [HideInInspector] public ShootingCheckUnit shootingCheck;
        [HideInInspector] public BotController botController;
        [HideInInspector] public HelthbarUnit helthbarUnit;
        [HideInInspector] public Transform pointForSpawn;
        [HideInInspector] public SetTarget setTarget;
        [HideInInspector] public Animator animator;
        
        private LinkManager m_LinkManager;
        private MovementController m_MovementController;
        private RandomWeaponBot m_RandomWeaponBot;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_MovementController = GetComponent<MovementController>();
            bulletsQuantity = GetComponent<BulletsQuantityUnit>();
            chController = GetComponent<CharacterController>();
            shootingCheck = GetComponent<ShootingCheckUnit>();
            helthbarUnit = GetComponent<HelthbarUnit>();
            animator = GetComponent<Animator>();

            if (!isBot)
            {
                m_LinkManager.player = this;
                SetPlayerMoveSettings();
                setTarget = GetComponent<SetTarget>();
                setTarget.SetTargetForBotAttack();
                SetMobileGrenadeSettings();
            }
            else
            {
                m_RandomWeaponBot = GetComponent<RandomWeaponBot>();
                botController = GetComponent<BotController>();
                idWeaponAtStart = m_RandomWeaponBot.RandomIdWeapon();
            }

            SetWeapon(idWeaponAtStart);
        }

        private void SetPlayerMoveSettings()
        {
            m_MovementController.SetPlayerMoveSettings(this);
        }
    
        public void Shoot(Unit unit)
        {
            shootingCheck.StartShooting(unit);
        }
    
        private void SetWeapon(int weaponId)
        {
            m_LinkManager.weaponController.SetWeaponParameters(weaponId, this);
        }

        public void NextWeapon()
        {
            m_LinkManager.eventsManager.NextWeapon(this);
        }

        private void SetMobileGrenadeSettings()
        {
            m_LinkManager.mobileGrenadeController.SetValues(pointsForGrenade);
        }
    }
}