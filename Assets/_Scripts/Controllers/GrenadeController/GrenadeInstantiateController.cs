using System;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeInstantiateController : MonoBehaviour
    {
        [SerializeField] private GameObject m_Grenade;

        public void GenerateGrenade(Vector3 pointStart, Vector3 pointEnd)
        {
            GameObject createdGrenade = Instantiate(m_Grenade, Vector3.zero, Quaternion.identity) as GameObject;
            GrenadeFlight grenadeFlight = createdGrenade.GetComponent<GrenadeFlight>();
            
            grenadeFlight.StartPoint = pointStart;
            grenadeFlight.EndPoint = pointEnd;
            grenadeFlight.isActive = true;
        }
    }
}