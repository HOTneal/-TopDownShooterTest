using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class HelthbarUnit : MonoBehaviour
    {
        public Image helthbar;
        public Text nickname;

        private UnitController Unit;
        private LinkManager LinkManager;
    
        private void Start()
        {
            Unit = GetComponent<UnitController>();
            LinkManager = LinkManager.instance;
        
            SetColorBar();
            SetNickname();
        }

        private void SetColorBar()
        {
            LinkManager.helthController.SetColorBar(Unit);
        }

        private void SetNickname()
        {
            nickname.text = Unit.nickname;
        }
    }
}