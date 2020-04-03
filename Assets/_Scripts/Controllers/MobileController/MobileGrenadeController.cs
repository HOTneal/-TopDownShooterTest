using System;
using System.Numerics;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Controllers.MobileController
{
    public class MobileGrenadeController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private float m_SpeedMoveEndPointGrenade = 10.0f;
        [SerializeField] private float DistanceTarget = 15.0f;

        public bool isCanMove = false;
        public Transform PointStartGrenade;
        public Transform PointCenterGrenade;
        public Transform PointEndGrenade;
        public Vector3 m_LastPositionPointEnd;
        [HideInInspector] public Vector3 DefaultPosPointEndGrenade;
        [HideInInspector] public Vector3 DefaultPosPointCenterGrenade;

        private LinkManager m_LinkManager;
        private Vector2 m_InputVector;
        private Image m_BgGrenade;
        private Image m_GrenadeStick;
        private RaycastHit m_Hit;
        private GameObject m_TargetIcon;

        private void Start()
        {
            m_BgGrenade = GetComponent<Image>();
            m_GrenadeStick = transform.GetChild(0).GetComponent<Image>();
            m_LinkManager = LinkManager.Instance;
        }

        private void Update()
        {
            if (!isCanMove)
                return;
            
            GetValuesForPointsGrenade(m_LinkManager.m_Player.PointsForGrenade);

            ClampMovePoints(PointCenterGrenade, DistanceTarget / 2);
            ClampMovePoints(PointEndGrenade, DistanceTarget);
            
            MoveEndPoint(PointEndGrenade, 2);
            MoveEndPoint(PointCenterGrenade, 1);
            
            CheckWalls();
            SetValueLineRenderer(PointStartGrenade.position, PointCenterGrenade.position, m_LastPositionPointEnd);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            ActiveMoveTargetAndDrawLine(true);
            OnDrag(eventData);
            m_TargetIcon.SetActive(true);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            ActiveMoveTargetAndDrawLine(false);
            m_InputVector = Vector2.zero;
            m_GrenadeStick.rectTransform.anchoredPosition = Vector2.zero;
            
            m_LinkManager.GrenadeInstantiateController.GenerateGrenade(PointStartGrenade.position, m_LastPositionPointEnd);
            
            DefaultValues();
            m_TargetIcon.SetActive(false);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            var bgSizeDelta = m_BgGrenade.rectTransform.sizeDelta;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BgGrenade.rectTransform, eventData.position,
                eventData.pressEventCamera, out pos)) return;

            pos.x = (pos.x / bgSizeDelta.x);
            pos.y = (pos.y / bgSizeDelta.y);

            m_InputVector = new Vector2(pos.x * 2 , pos.y * 2);
            
            m_InputVector = (m_InputVector.magnitude > 1.0f) ? m_InputVector.normalized : m_InputVector;
            m_GrenadeStick.rectTransform.anchoredPosition = new Vector2(m_InputVector.x * (bgSizeDelta.x / 2), m_InputVector.y * (bgSizeDelta.y / 2));
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            
        }

        private void MoveEndPoint(Transform pointGrenade, int numberPointInArray)
        {
            var point = pointGrenade.localPosition;
            var x = point.x;
            var y = point.y;
            var z = point.z - m_InputVector.y * m_SpeedMoveEndPointGrenade * Time.deltaTime;
            
            m_LinkManager.m_Player.PointsForGrenade[numberPointInArray].localPosition = new Vector3(x, y, z);
        }

        public void SetValues(Transform[] points)
        {
            DefaultPosPointEndGrenade = points[2].localPosition;
            DefaultPosPointCenterGrenade = points[1].localPosition;
            
            m_TargetIcon = points[2].GetChild(0).gameObject;
        }
        
        private void DefaultValues()
        {
            PointCenterGrenade.localPosition = DefaultPosPointCenterGrenade;
            PointEndGrenade.localPosition = DefaultPosPointEndGrenade;
            
            m_LinkManager.LineRendererController.LineRenderer.SetVertexCount(0);
            m_LinkManager.LineRendererController.isCanDrawLine = false;
            m_LinkManager.LineRendererController.PointStartLine = Vector3.zero;
            m_LinkManager.LineRendererController.PointCenterLine = Vector3.zero;
            m_LinkManager.LineRendererController.PointEndLine = Vector3.zero;
        }

        private void ClampMovePoints(Transform point, float distanceTarget)
        {
            var pointPos = point.position;
            var startPointPos = PointStartGrenade.position;
            var clampX = Mathf.Clamp(pointPos.x, startPointPos.x - distanceTarget, startPointPos.x + distanceTarget);
            var clampZ = Mathf.Clamp(pointPos.z, startPointPos.z - distanceTarget, startPointPos.z + distanceTarget);

            point.position = new Vector3(clampX, PointEndGrenade.position.y, clampZ);
        }

        private void ActiveMoveTargetAndDrawLine(bool isActive)
        {
            isCanMove = isActive;
            m_LinkManager.LineRendererController.isCanDrawLine = isActive;
        }

        private void GetValuesForPointsGrenade(Transform[] point)
        {
            PointStartGrenade = point[0];
            PointCenterGrenade = point[1];
            PointEndGrenade = point[2];      
        }

        private void SetValueLineRenderer(Vector3 startPoint, Vector3 centerPoint, Vector3 endPoint)
        {
            var points = new Vector3[] {startPoint, centerPoint, endPoint};
            m_LinkManager.LineRendererController.SetValues(points);
        }
        
        private void CheckWalls()
        {
            if (!Physics.Linecast(PointStartGrenade.position, PointEndGrenade.position, out m_Hit)) return;
            var position = PointEndGrenade.position;

            if (m_Hit.collider.CompareTag("Wall"))
            {
                m_LastPositionPointEnd = new Vector3(m_Hit.point.x, position.y, m_Hit.point.z);
                m_TargetIcon.SetActive(false);
            }
            else
            {
                m_LastPositionPointEnd = position;
                m_TargetIcon.SetActive(true);
            }
        }
    }
}