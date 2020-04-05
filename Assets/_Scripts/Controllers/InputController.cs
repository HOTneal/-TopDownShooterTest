using Managers;
using UnityEngine;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }

        private void Update()
        {
            if (m_LinkManager.player == null)
                return;
        
            PlayerShoot();
            NextWeapon();
        }

        public Vector2 MovePlayer()
        {
            Vector2 axis;
            var horizontalKeyboard = Input.GetAxis("Horizontal");
            var verticalKeyboard = Input.GetAxis("Vertical");
        
            var horizontalJoystick = m_LinkManager.mobilePlayerController.Horizontal();
            var verticalJoystick = m_LinkManager.mobilePlayerController.Vertical();
        
            axis.x = horizontalKeyboard != 0 ? horizontalKeyboard : horizontalJoystick;
            axis.y = verticalKeyboard != 0 ? verticalKeyboard : verticalJoystick;
            return axis;
        }

        private void PlayerShoot()
        {
            if (Input.GetMouseButton(0) || m_LinkManager.mobileButtons.Shooting())
                m_LinkManager.player.Shoot(m_LinkManager.player);
        }

        private void NextWeapon()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) 
                m_LinkManager.player.NextWeapon();
        }
    }
}
