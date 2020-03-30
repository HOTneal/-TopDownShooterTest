using UnityEngine;

namespace Controllers.BulletsController
{
    public class BulletMove : MonoBehaviour
    {
        public Vector3 TargetPos;
        public float Speed;

        private Transform m_BulletTransform;

        private void Start()
        {
            m_BulletTransform = transform;
        }

        private void Update()
        {
            m_BulletTransform.position = Vector3.MoveTowards(m_BulletTransform.position, TargetPos, Speed * Time.deltaTime);
            if (m_BulletTransform.position == TargetPos)
                DestroyObj();
        }

        private void DestroyObj()
        {
            Destroy(gameObject);
        }
    
        private void OnBecameInvisible()
        {
            DestroyObj();
        }
    }
}