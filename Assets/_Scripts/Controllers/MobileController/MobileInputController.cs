using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.MobileController
{
    public class MobileInputController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Image m_BgJoystick;
        private Image m_MoveStick;
        private Vector2 m_InputVector;
        private float m_MaxJoyMoveAway = 2.0f;
        private Vector2 m_Pos;
        private Vector2 m_JoystickPos;

        private void Start()
        {
            m_BgJoystick = GetComponent<Image>();
            m_MoveStick = transform.GetChild(0).GetComponent<Image>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            m_InputVector = Vector2.zero;
            m_MoveStick.rectTransform.anchoredPosition = Vector2.zero;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            m_JoystickPos = m_BgJoystick.rectTransform.position;
        
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BgJoystick.rectTransform, eventData.position,
                eventData.pressEventCamera, out m_Pos)) return;
        
            m_Pos.x = (m_Pos.x / m_JoystickPos.x);
            m_Pos.y = (m_Pos.y / m_JoystickPos.y);

            m_InputVector = new Vector2(m_Pos.x * m_MaxJoyMoveAway, m_Pos.y * m_MaxJoyMoveAway);
            m_InputVector = (m_InputVector.magnitude > 1.0f) ? m_InputVector.normalized : m_InputVector;

            m_MoveStick.rectTransform.anchoredPosition = new Vector2(m_InputVector.x * (m_JoystickPos.x / m_MaxJoyMoveAway), m_InputVector.y * (m_JoystickPos.y / m_MaxJoyMoveAway));
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {

        }

        public float Horizontal()
        {
            return m_InputVector.x;
        }

        public float Vertical()
        {
            return m_InputVector.y;
        }
    }
}
