using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private LinkManager m_LinkManager;
    private Transform m_PlayerTransform;
    public Animator m_Animator;
    private CharacterController m_ChController;
    private float m_SpeedWalk;
    private float m_GravityForce;
    private Vector3 m_MoveVector;
    private InputController m_InputController;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
        m_InputController = GetComponent<InputController>();
    }
    
    private void Update()
    {
        if (m_LinkManager.m_Player.isDead)
            return;
        
        MovePlayer();
        GravityPlayer();
    }

    public void SetPlayerMoveSettings(Unit unit)
    {
        m_PlayerTransform = unit.transform;
        m_Animator = unit.m_Animator;
        m_ChController = unit.m_ChController;
        m_SpeedWalk = unit.m_SpeedWalk;
    }
    
    private void MovePlayer()
    {
        if (m_ChController.isGrounded)
        {
            m_MoveVector = Vector3.zero;
            m_MoveVector.x = m_InputController.MovePlayer().x * m_SpeedWalk;
            m_MoveVector.z = m_InputController.MovePlayer().y * m_SpeedWalk;

            PlayerWalkAnim();
            PlayerRotate();
        }

        m_MoveVector.y = m_GravityForce;
        m_ChController.Move(m_MoveVector * Time.deltaTime);
    }

    private void PlayerWalkAnim()
    {
        if (m_MoveVector.x != 0 || m_MoveVector.z != 0)
            m_Animator.SetBool("Walk", true);
        else
            m_Animator.SetBool("Walk", false);
    }

    private void PlayerRotate()
    {
        if (!(Vector3.Angle(Vector3.forward, m_MoveVector) > 1.0f) &&
            Vector3.Angle(Vector3.forward, m_MoveVector) != 0) return;
        
        var direct = Vector3.RotateTowards(m_PlayerTransform.forward, m_MoveVector, m_SpeedWalk * Time.deltaTime,
            0.0f);
        m_PlayerTransform.rotation = Quaternion.LookRotation(direct);
    }

    private void GravityPlayer()
    {
        if (!m_ChController.isGrounded)
            m_GravityForce -= 20.0f * Time.deltaTime;
        else
            m_GravityForce = -1.0f;
    }
    
}