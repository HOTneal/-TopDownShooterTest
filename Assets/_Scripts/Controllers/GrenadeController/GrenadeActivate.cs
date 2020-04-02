using System;
using Managers;
using UnityEngine;

namespace Controllers.GrenadeController
{
    public class GrenadeActivate : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LinkManager.Instance.GrenadeController.ExplodeGrenade(transform));
        }
    }
}
