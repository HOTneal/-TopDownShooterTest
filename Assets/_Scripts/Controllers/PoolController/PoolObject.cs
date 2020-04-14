using System.Collections.Generic;
using Controllers.BulletsController;
using UnityEngine;

namespace Controllers.PoolController
{
    public abstract class PoolObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        public int initialSize = 10;
        public T prefab;
        public Queue<T> objInPool = new Queue<T>();
        public Transform poolParent;

        private void Start()
        {
            AddInPool(initialSize);
        }
        
        public void AddInPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject createdBullet = Instantiate(prefab.gameObject, poolParent.position, Quaternion.identity) as GameObject;
                createdBullet.transform.SetParent(poolParent);
                
                T bulletMove = createdBullet.GetComponent<T>();
                AddInList(bulletMove);
            }
        }

        public void AddInList(T bullet)
        {
            objInPool.Enqueue(bullet);
        }

        public T DeleteFromList()
        {
            return objInPool.Dequeue();
        }

        public void CheckBulletsInPool()
        {
            if (objInPool.Count == 0)
                AddInPool(1);
        }
    }
}