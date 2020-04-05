using System;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeFlight : MonoBehaviour
    {
        [HideInInspector] public Vector3 startPoint;
        [HideInInspector] public Vector3 endPoint;
        public float speedFlight, height;
        public bool isActive = false;

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
            m_Animation %= speedFlight;

            m_Transform.position = MathParabola.Parabola(startPoint, endPoint, height, m_Animation / speedFlight);

            if (Vector3.Distance(m_Transform.position, endPoint) < 0.5f)
                isActive = false;
        }
    }
}
