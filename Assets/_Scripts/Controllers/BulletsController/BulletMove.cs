using UnityEngine;

namespace Controllers.BulletsController
{
    public class BulletMove : MonoBehaviour
    {
        public Vector3 targetPos;
        public float speed;

        private Transform m_BulletTransform;

        private void Start()
        {
            m_BulletTransform = transform;
        }

        private void Update()
        {
            m_BulletTransform.position = Vector3.MoveTowards(m_BulletTransform.position, targetPos, speed * Time.deltaTime);
            if (m_BulletTransform.position == targetPos)
                DestroyObj();
        }

        public void SetTargetPostion()
        {
            targetPos = new Vector3();
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