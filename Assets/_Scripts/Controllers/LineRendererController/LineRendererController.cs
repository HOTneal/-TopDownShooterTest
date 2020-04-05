using System.Collections.Generic;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Controllers.LineRendererController
{
    public class LineRendererController : MonoBehaviour
    {
        [SerializeField] private float m_OffsetY;

        public float vertexCount = 12.0f;
        public bool isCanDrawLine = false;
        public LineRenderer lineRenderer;
        [HideInInspector] public Vector3 pointStartLine;
        [HideInInspector] public Vector3 pointCenterLine;
        [HideInInspector] public Vector3 pointEndLine;

        private LinkManager m_LinkManager;
    
        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.15f;
        }

        void Update()
        {
            if (!isCanDrawLine)
                return;

            pointCenterLine = new Vector3((pointCenterLine.x), (m_OffsetY), (pointCenterLine.z));

            var pointList = new List<Vector3>();
        
            for (float i = 0; i <= 1; i+= 1/vertexCount)
            {
                var tangent1 = Vector3.Lerp(pointStartLine, pointCenterLine, i);
                var tangent2 = Vector3.Lerp(pointCenterLine, pointEndLine, i);
                var curve = Vector3.Lerp(tangent1, tangent2, i);
            
                pointList.Add(curve);
            }

            lineRenderer.positionCount = pointList.Count;
            lineRenderer.SetPositions(pointList.ToArray());
        }

        public void SetValues(Vector3[] point)
        {
            pointStartLine = point[0];
            pointCenterLine = point[1];
            pointEndLine = point[2];
        }
    }
}
