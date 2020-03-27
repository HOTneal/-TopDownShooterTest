using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Transform m_PlayerTransform;
    public Animator m_Animator;
    private CharacterController m_ChController;
    private float m_SpeedWalk;
    private float m_GravityForce;
    private float m_SpeedAnim;
    private Vector3 m_MoveVector;

    private void Update()
    {
        MovePlayer();
        GravityPlayer();
    }

    public void SetPlayerMoveSettings(Transform playerTransform, Animator playerAnimator, CharacterController playerController, float playerSpeed)
    {
        m_PlayerTransform = playerTransform;
        m_Animator = playerAnimator;
        m_ChController = playerController;
        m_SpeedWalk = playerSpeed;
    }
    
    private void MovePlayer()
    {
        if (m_ChController.isGrounded)
        {
            m_MoveVector = Vector3.zero;
            m_MoveVector.x = LinkManager.Instance.m_InputController.MovePlayer().x * m_SpeedWalk;
            m_MoveVector.z = LinkManager.Instance.m_InputController.MovePlayer().y * m_SpeedWalk;

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