using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletMove : MonoBehaviour
{
     public Vector3 m_TargetPos;
     public float m_Speed;

     private Transform m_BulletTransform;

     private void Start()
     {
         m_BulletTransform = transform;
     }

    private void Update()
    {
        m_BulletTransform.position = Vector3.MoveTowards(m_BulletTransform.position, m_TargetPos, m_Speed * Time.deltaTime);
        if (m_BulletTransform.position == m_TargetPos)
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