using System;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeEffects : MonoBehaviour
    {
        [SerializeField] private GameObject m_Effect;

        public void GenerateEffects(Transform grenade)
        {
            Instantiate(m_Effect, grenade.position, Quaternion.identity);
        }
    }
}
