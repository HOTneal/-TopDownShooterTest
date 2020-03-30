using Managers;
using UnityEngine;

namespace Controllers.KillListController
{
    public class DestoryEventMurder : MonoBehaviour
    {
        public float TimeStartFadeOut = 3.0f;
        public float SpeedFadeOut = 1.5f;
        [HideInInspector] public bool isFadeOut = false;
        
        private CanvasGroup m_CanvasGroup;

        private void Start()
        {
            StartCoroutine(LinkManager.Instance.KillListController.DeleteKillFromList(this));
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (!isFadeOut)
                return;
            
            FadeOut();
            if (m_CanvasGroup.alpha == 0)
                    Destroy(gameObject);
        }

        private void FadeOut()
        {
            m_CanvasGroup.alpha -= SpeedFadeOut * Time.deltaTime;
        }
    }
}