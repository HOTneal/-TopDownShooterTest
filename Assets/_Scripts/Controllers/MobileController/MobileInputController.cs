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
            Vector2 m_Pos;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BgJoystick.rectTransform, eventData.position,
                eventData.pressEventCamera, out m_Pos)) return;
        
            m_Pos.x = (m_Pos.x / m_BgJoystick.rectTransform.sizeDelta.x);
            m_Pos.y = (m_Pos.y / m_BgJoystick.rectTransform.sizeDelta.y);

            m_InputVector = new Vector2(m_Pos.x * 2 , m_Pos.y * 2);
            
            print(m_InputVector.magnitude);

            m_InputVector = (m_InputVector.magnitude > 1.0f) ? m_InputVector.normalized : m_InputVector;

            m_MoveStick.rectTransform.anchoredPosition = new Vector2(m_InputVector.x * (m_BgJoystick.rectTransform.sizeDelta.x / 2), m_InputVector.y * (m_BgJoystick.rectTransform.sizeDelta.y / 2));
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
