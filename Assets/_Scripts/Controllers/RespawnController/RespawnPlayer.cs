using Managers;
using UnityEngine;

namespace Controllers.RespawnController
{
    public class RespawnPlayer : MonoBehaviour
    {
        public void Respawn()
        {
            LinkManager link = LinkManager.instance;
            
            link.respawnController.StartCoroutine(link.respawnController.Respawn(link.player, 0));
            link.uiManager.DarkPanel(0, false);
            link.uiManager.DeadPanel(false);
            link.uiManager.Interface(true);
        }
    }
}