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
            Helthbar = transform.GetChild(3).GetChild(0).GetComponent<Image>();
            Nickname = transform.GetChild(3).GetChild(1).GetComponent<Text>();
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