using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class HelthbarUnit : MonoBehaviour
    {
        public Image Helthbar;
        public Text Nickname;

        private Unit Unit;
        private LinkManager LinkManager;
    
        private void Start()
        {
            Unit = GetComponent<Unit>();
            LinkManager = LinkManager.Instance;
        
            SetColorBar();
            SetNickname();
        }

        private void SetColorBar()
        {
            LinkManager.HelthController.SetColorBar(Unit);
        }

        private void SetNickname()
        {
            Nickname.text = Unit.Nickname;
        }
    }
}