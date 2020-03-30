using UnityEngine;

namespace Controllers.HelthbarController
{
    public class HelthbarRotate : MonoBehaviour
    {
        private Transform m_Transform;

        private void Start()
        {
            m_Transform = transform;
        }

        private void LateUpdate()
        {
            m_Transform.LookAt(Camera.main.transform);
            m_Transform.Rotate(0, 180, 0);
        }
    }
}