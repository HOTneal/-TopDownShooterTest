using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.MobileController
{
    public class MobileButtons : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image m_Shoot;
        [SerializeField] private Image m_Grenade;
        [SerializeField] private bool m_IsShooting = false;
        [SerializeField] private bool m_IsEndShooting = false;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            m_IsEndShooting = false;
            
            switch (eventData.pointerEnter.tag)
            {
                case "MobileShootStick":
                    m_IsShooting = true;
                    break;
                
                case "MobileGrenadeStick":
                    Grenade();
                    break;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            m_IsShooting = false;
            m_IsEndShooting = true;
        }

        public bool Shooting()
        {
            return m_IsShooting;
        }

        public bool EndShoot()
        {
            return m_IsEndShooting;
        }

        private void Grenade()
        {
            print("grenade");
        }
        
    }
}
