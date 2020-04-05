using System;
using Managers;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeActivate : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LinkManager.instance.grenadeController.ExplodeGrenade(transform));
        }
    }
}
