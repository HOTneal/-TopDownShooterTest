using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private float m_SpeedMoveCam;

    private void Update()
    {
        transform.LookAt(m_Target);
        transform.position = Vector3.Lerp(transform.position, m_Target.position + m_Offset, m_SpeedMoveCam * Time.deltaTime);
    }
}
