using System;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.MobileController
{
    public class MobileShooting : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        public Vector2 inputVector;
        
        [SerializeField] private Image m_BgShootStick;
        [SerializeField] private Image m_ShootStick;
        [SerializeField] private bool m_IsShooting = false;
        [SerializeField] private bool m_IsEndShooting = false;
        [SerializeField] private float m_MaxJoyMoveAway = 2.0f;
        [SerializeField] private float m_SpeedRotate = 3.0f;

        private LinkManager m_LinkManager;
        
        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_BgShootStick = GetComponent<Image>();
            m_ShootStick = transform.GetChild(0).GetComponent<Image>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
            m_IsShooting = true;
            m_IsEndShooting = false;
            
            m_LinkManager.player.movementController.isCanRotate = false;
            m_LinkManager.player.movementController.speedRotate = m_SpeedRotate;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
            m_ShootStick.rectTransform.anchoredPosition = Vector2.zero;

            m_IsShooting = false;
            m_IsEndShooting = true;
            
            m_LinkManager.player.movementController.isCanRotate = true;
            m_LinkManager.player.movementController.speedRotate = m_LinkManager.player.movementController.speedWalk;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 m_Pos;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BgShootStick.rectTransform, eventData.position,
                eventData.pressEventCamera, out m_Pos)) return;

            var sizeDelta = m_BgShootStick.rectTransform.sizeDelta;
            m_Pos.x = (m_Pos.x / sizeDelta.x);
            m_Pos.y = (m_Pos.y / sizeDelta.y);

            inputVector = new Vector2(m_Pos.x * m_MaxJoyMoveAway , m_Pos.y * m_MaxJoyMoveAway);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            m_ShootStick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (sizeDelta.x / m_MaxJoyMoveAway), inputVector.y * (sizeDelta.y / m_MaxJoyMoveAway));
            
            m_LinkManager.player.movementController.VectorRotate(inputVector);
        }
        
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            
        }
        
        public bool Shooting()
        {
            return m_IsShooting;
        }

        public bool EndShoot()
        {
            return m_IsEndShooting;
        }
        
    }
}
