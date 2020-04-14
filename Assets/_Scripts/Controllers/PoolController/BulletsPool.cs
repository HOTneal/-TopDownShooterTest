using System.Collections.Generic;
using UnityEngine;

namespace Controllers.PoolController
{
    public class BulletsPool : MonoBehaviour
    {
        [SerializeField] private GameObject m_Bullet;
    
        private Queue<GameObject> m_BulletsInPool = new Queue<GameObject>();
        private Transform m_PoolParent;

        private void Start()
        {
            m_PoolParent = transform;
            AddInPool(10);
        }

        private void AddInPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject createdBullet = Instantiate(m_Bullet, m_PoolParent.position, Quaternion.identity) as GameObject;
                createdBullet.transform.SetParent(m_PoolParent);
                AddInList(createdBullet);
            }
        }

        public void AddInList(GameObject bullet)
        {
            m_BulletsInPool.Enqueue(bullet);
        }

        public GameObject DeleteFromList()
        {
            return m_BulletsInPool.Dequeue();
        }

        public void CheckBulletsInPool()
        {
            if (m_BulletsInPool.Count == 0)
                AddInPool(1);
        }
    }
}
