using Managers;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        public Animator animator;

        private LinkManager m_LinkManager;
        private Transform m_PlayerTransform;
        private CharacterController m_ChController;
        private float m_SpeedWalk;
        private float m_GravityForce;
        private Vector3 m_MoveVector;
        private InputController m_InputController;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_InputController = GetComponent<InputController>();
        }
    
        private void Update()
        {
            if (m_LinkManager.player == null)
                return;
        
            MovePlayer();
            GravityPlayer();
        }

        public void SetPlayerMoveSettings(Unit.Unit unit)
        {
            m_PlayerTransform = unit.transform;
            animator = unit.animator;
            m_ChController = unit.chController;
            m_SpeedWalk = unit.speedWalk;
        }
    
        private void MovePlayer()
        {
            if (m_ChController.isGrounded)
            {
                m_MoveVector = Vector3.zero;
                m_MoveVector.x = m_InputController.MovePlayer().x * m_SpeedWalk;
                m_MoveVector.z = m_InputController.MovePlayer().y * m_SpeedWalk;

                PlayerWalkAnim();
                PlayerRotate();
            }

            m_MoveVector.y = m_GravityForce;
            m_ChController.Move(m_MoveVector * Time.deltaTime);
        }

        private void PlayerWalkAnim()
        {
            if (m_MoveVector.x != 0 || m_MoveVector.z != 0)
                animator.SetBool("Walk", true);
            else
                animator.SetBool("Walk", false);
        }
        
        
        private void PlayerRotate()
        {
            if (!(Vector3.Angle(Vector3.forward, m_MoveVector) > 1.0f) &&
                Vector3.Angle(Vector3.forward, m_MoveVector) != 0) return;
        
            var direct = Vector3.RotateTowards(m_PlayerTransform.forward, m_MoveVector, m_SpeedWalk * Time.deltaTime,
                0.0f);
            m_PlayerTransform.rotation = Quaternion.LookRotation(direct);
        }

        private void GravityPlayer()
        {
            if (!m_ChController.isGrounded)
                m_GravityForce -= 20.0f * Time.deltaTime;
            else
                m_GravityForce = -1.0f;
        }
    
    }
}