using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffects : MonoBehaviour
{
    [SerializeField] private GameObject m_Effect;

    public void GenerateEffects(Transform grenade)
    {
        Instantiate(m_Effect, grenade.position, Quaternion.identity);
    }
}
