using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public void Respawn()
    {
        LinkManager link = LinkManager.Instance;
            
        link.RespawnController.StartCoroutine(link.RespawnController.Respawn(link.m_Player, 0));
        link.UIManager.DarkPanel(0, false);
        link.UIManager.DeadPanel(false);
        link.UIManager.Interface(true);
    }
}