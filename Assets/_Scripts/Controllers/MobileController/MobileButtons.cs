using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.MobileController
{
    public class MobileButtons : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image m_Shoot;
        [SerializeField] private Image m_Grenade;
        [SerializeField] private bool isShooting = false;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            switch (eventData.pointerEnter.tag)
            {
                case "MobileShootStick":
                    isShooting = true;
                    break;
                
                case "MobileGrenadeStick":
                    Grenade();
                    break;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            isShooting = false;
        }

        public bool Shooting()
        {
            return isShooting;
        }

        private void Grenade()
        {
            print("grenade");
        }
        
    }
}
