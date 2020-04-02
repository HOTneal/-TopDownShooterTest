using System;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeFlight : MonoBehaviour
    {
        [HideInInspector] public Vector3 StartPoint;
        [HideInInspector] public Vector3 EndPoint;
        public float SpeedFlight;
        public bool isActive = false;
        public float m_Height;

        private float m_Animation;
        private Transform m_Transform;

        private void Start()
        {
            m_Transform = transform;
        }

        private void Update()
        {
            if (!isActive)
                return;
            
            m_Animation += Time.deltaTime;
            m_Animation %= SpeedFlight;

            m_Transform.position = MathParabola.Parabola(StartPoint, EndPoint, m_Height, m_Animation / SpeedFlight);

            if (Vector3.Distance(m_Transform.position, EndPoint) < 0.1f)
                isActive = false;
        }
    }
}
