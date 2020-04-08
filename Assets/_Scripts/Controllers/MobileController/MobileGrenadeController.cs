using System;
using System.Numerics;
using Controllers.GrenadeController;
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
        [SerializeField] private float m_MaxTargetMoveAway = 2.0f;
        [SerializeField] private GrenadeInstantiate m_GrenadeInstantiate;
        [SerializeField] private LineRendererController.LineRendererController m_LineRenderer;

        public bool isCanMove = false;
        [HideInInspector] public Transform pointStartGrenade;
        [HideInInspector] public Transform pointCenterGrenade;
        [HideInInspector] public Transform pointEndGrenade;
        [HideInInspector] public Vector3 lastPositionPointEnd;
        [HideInInspector] public Vector3 defaultPosPointEndGrenade;
        [HideInInspector] public Vector3 defaultPosPointCenterGrenade;

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
            m_LinkManager = LinkManager.instance;
        }

        private void Update()
        {
            if (!isCanMove)
                return;
            
            GetValuesForPointsGrenade(m_LinkManager.player.pointsForGrenade);

            ClampMovePoints(pointCenterGrenade, DistanceTarget / 2);
            ClampMovePoints(pointEndGrenade, DistanceTarget);
            
            MoveEndPoint(pointEndGrenade, 2);
            MoveEndPoint(pointCenterGrenade, 1);
            
            CheckWalls();
            SetValueLineRenderer(pointStartGrenade.position, pointCenterGrenade.position, lastPositionPointEnd);
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
            
            m_GrenadeInstantiate.GenerateGrenade(pointStartGrenade.position, lastPositionPointEnd);
            
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

            m_InputVector = new Vector2(pos.x * m_MaxTargetMoveAway , pos.y * m_MaxTargetMoveAway);
            
            m_InputVector = (m_InputVector.magnitude > 1.0f) ? m_InputVector.normalized : m_InputVector;
            m_GrenadeStick.rectTransform.anchoredPosition = new Vector2(0, m_InputVector.y * (bgSizeDelta.y / m_MaxTargetMoveAway));
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
            
            m_LinkManager.player.pointsForGrenade[numberPointInArray].localPosition = new Vector3(x, y, z);
        }

        public void SetValues(Transform[] points)
        {
            defaultPosPointEndGrenade = points[2].localPosition;
            defaultPosPointCenterGrenade = points[1].localPosition;
            
            m_TargetIcon = points[2].GetChild(0).gameObject;
        }
        
        private void DefaultValues()
        {
            pointCenterGrenade.localPosition = defaultPosPointCenterGrenade;
            pointEndGrenade.localPosition = defaultPosPointEndGrenade;
            
            m_LineRenderer.lineRenderer.SetVertexCount(0);
            m_LineRenderer.isCanDrawLine = false;
            m_LineRenderer.pointStartLine = Vector3.zero;
            m_LineRenderer.pointCenterLine = Vector3.zero;
            m_LineRenderer.pointEndLine = Vector3.zero;
        }

        private void ClampMovePoints(Transform point, float distanceTarget)
        {
            var pointPos = point.position;
            var startPointPos = pointStartGrenade.position;
            var clampX = Mathf.Clamp(pointPos.x, startPointPos.x - distanceTarget, startPointPos.x + distanceTarget);
            var clampZ = Mathf.Clamp(pointPos.z, startPointPos.z - distanceTarget, startPointPos.z + distanceTarget);

            point.position = new Vector3(clampX, pointEndGrenade.position.y, clampZ);
        }

        private void ActiveMoveTargetAndDrawLine(bool isActive)
        {
            isCanMove = isActive;
            m_LineRenderer.isCanDrawLine = isActive;
        }

        private void GetValuesForPointsGrenade(Transform[] point)
        {
            pointStartGrenade = point[0];
            pointCenterGrenade = point[1];
            pointEndGrenade = point[2];      
        }

        private void SetValueLineRenderer(Vector3 startPoint, Vector3 centerPoint, Vector3 endPoint)
        {
            var points = new Vector3[] {startPoint, centerPoint, endPoint};
            m_LineRenderer.SetValues(points);
        }
        
        private void CheckWalls()
        {
            if (!Physics.Linecast(pointStartGrenade.position, pointEndGrenade.position, out m_Hit)) return;
            var position = pointEndGrenade.position;

            if (m_Hit.collider.CompareTag("Wall"))
            {
                lastPositionPointEnd = new Vector3(m_Hit.point.x, position.y, m_Hit.point.z);
                m_TargetIcon.SetActive(false);
            }
            else
            {
                lastPositionPointEnd = position;
                m_TargetIcon.SetActive(true);
            }
        }
    }
}