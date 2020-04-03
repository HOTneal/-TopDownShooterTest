using System.Collections.Generic;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Controllers.LineRendererController
{
    public class LineRendererController : MonoBehaviour
    {
        [SerializeField] private float m_OffsetY;

        public float VertexCount = 12.0f;
        public bool isCanDrawLine = false;
        [HideInInspector] public LineRenderer LineRenderer;
        [HideInInspector] public Vector3 PointStartLine;
        [HideInInspector] public Vector3 PointCenterLine;
        public Vector3 PointEndLine;

        private LinkManager m_LinkManager;
    
        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
            LineRenderer.startWidth = 0.15f;
            LineRenderer.endWidth = 0.15f;
        }

        void Update()
        {
            if (!isCanDrawLine)
                return;

            PointCenterLine = new Vector3((PointCenterLine.x), (m_OffsetY), (PointCenterLine.z));

            var pointList = new List<Vector3>();
        
            for (float i = 0; i <= 1; i+= 1/VertexCount)
            {
                var tangent1 = Vector3.Lerp(PointStartLine, PointCenterLine, i);
                var tangent2 = Vector3.Lerp(PointCenterLine, PointEndLine, i);
                var curve = Vector3.Lerp(tangent1, tangent2, i);
            
                pointList.Add(curve);
            }

            LineRenderer.positionCount = pointList.Count;
            LineRenderer.SetPositions(pointList.ToArray());
        }

        public void SetValues(Vector3[] point)
        {
            PointStartLine = point[0];
            PointCenterLine = point[1];
            PointEndLine = point[2];
        }
    }
}
