using System.Collections.Generic;
using Managers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Controllers.LineRendererController
{
    public class LineRendererController : MonoBehaviour
    {
        [SerializeField] private float m_VertexCount = 12;
        [SerializeField] private float m_OffsetY;

        public LineRenderer LineRenderer;
        public Transform PointStartLine;
        public Transform PointCenterLine;
        public Transform PointEndLine;
        public bool isCanDrawLine = false;
    
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

            PointCenterLine.position = new Vector3((PointCenterLine.position.x), (m_OffsetY), (PointCenterLine.position.z));

            var pointList = new List<Vector3>();
        
            for (float i = 0; i <= 1; i+= 1/m_VertexCount)
            {
                var tangent1 = Vector3.Lerp(PointStartLine.position, PointCenterLine.position, i);
                var tangent2 = Vector3.Lerp(PointCenterLine.position, PointEndLine.position, i);
                var curve = Vector3.Lerp(tangent1, tangent2, i);
            
                pointList.Add(curve);
            }

            LineRenderer.positionCount = pointList.Count;
            LineRenderer.SetPositions(pointList.ToArray());
        }
    }
}
