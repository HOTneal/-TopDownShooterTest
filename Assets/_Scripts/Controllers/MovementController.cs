using Managers;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        public Animator animator;
        public bool isCanRotate = true;
        public Vector3 moveVector;
        public float speedRotate;
        public float speedWalk;

        private LinkManager m_LinkManager;
        private Transform m_PlayerTransform;
        private CharacterController m_ChController;
        private float m_GravityForce;
        private Vector3 m_InputVector;
        private InputController m_InputController;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_InputController = GetComponent<InputController>();
            speedRotate = speedWalk;
        }
    
        private void Update()
        {
            if (m_LinkManager.player == null)
                return;
        
            MovePlayer();
            GravityPlayer();
        }

        public void SetPlayerMoveSettings(Unit.UnitController unit)
        {
            m_PlayerTransform = unit.transform;
            animator = unit.animator;
            m_ChController = unit.chController;
            speedWalk = unit.speedWalk;
        }
    
        private void MovePlayer()
        {
            if (m_ChController.isGrounded)
            {
                m_InputVector = Vector3.zero;
                m_InputVector.x = m_InputController.MovePlayer().x * speedWalk;
                m_InputVector.z = m_InputController.MovePlayer().y * speedWalk;
                
                PlayerWalkAnim();
                PlayerSpeedWalkAnim();
                PlayerRotate();
            }

            m_InputVector.y = m_GravityForce;
            m_ChController.Move(m_InputVector * Time.deltaTime);
        }

        private void PlayerWalkAnim()
        {
            if (m_InputVector.x != 0 || m_InputVector.z != 0)
                animator.SetBool("Walk", true);
            else
                animator.SetBool("Walk", false);
        }

        private void PlayerRotate()
        {
            if (!(Vector3.Angle(Vector3.forward, moveVector) > 1.0f) &&
                Vector3.Angle(Vector3.forward, moveVector) != 0) return;
            
            if (isCanRotate)
                moveVector = m_InputVector;
            
            var direct = Vector3.RotateTowards(m_PlayerTransform.forward, moveVector, speedRotate * Time.deltaTime,
                0.0f);
            m_PlayerTransform.rotation = Quaternion.LookRotation(direct);
        }

        public void VectorRotate(Vector3 inputVector)
        {
            if (isCanRotate)
                return;
            
            var rotate = new Vector3(inputVector.x, 0, inputVector.y);
            moveVector = rotate;
        }

        private void PlayerSpeedWalkAnim()
        {
            var speedAnim = Mathf.Clamp(((Mathf.Abs(m_InputVector.x) + Mathf.Abs(m_InputVector.z)) / 5), 0.0f, 2.0f);
            m_LinkManager.player.animator.SetFloat("SpeedAnim", speedAnim);
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