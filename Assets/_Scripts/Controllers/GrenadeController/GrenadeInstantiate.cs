using System;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeInstantiate : MonoBehaviour
    {
        [SerializeField] private GameObject m_Grenade;

        public void GenerateGrenade(Vector3 pointStart, Vector3 pointEnd)
        {
            GameObject createdGrenade = Instantiate(m_Grenade, Vector3.zero, Quaternion.identity) as GameObject;
            GrenadeFlight grenadeFlight = createdGrenade.GetComponent<GrenadeFlight>();
            
            grenadeFlight.startPoint = pointStart;
            grenadeFlight.endPoint = pointEnd;
            grenadeFlight.isActive = true;
        }
    }
}