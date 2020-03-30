using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour {
    
        [SerializeField] private Vector3 m_Offset;
        [SerializeField] private float m_SpeedMoveCam;

        private Transform m_Target;
        private Transform m_Camera;

        private void Start()
        {
            m_Target = transform;
            if (Camera.main != null) m_Camera = Camera.main.transform;
        }

        private void Update()
        {
            m_Camera.LookAt(m_Target);
            m_Camera.position = Vector3.Lerp(m_Camera.position, m_Target.position + m_Offset, m_SpeedMoveCam * Time.deltaTime);
        }
    }
}
