using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public void Respawn()
    {
        LinkManager link = LinkManager.Instance;
            
        link.m_RespawnController.StartCoroutine(link.m_RespawnController.Respawn(link.m_Player, 0));
        link.m_UIManager.DarkPanel(0, false);
        link.m_UIManager.DeadPanel(false);
        link.m_UIManager.Interface(true);
    }
}
