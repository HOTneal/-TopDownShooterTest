using UnityEngine;

namespace Unit
{
    public class SetTarget : MonoBehaviour
    {
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.Instance;
        }

        public void SetTargetForBotAttack()
        {
            foreach (var value in LinkManager.Instance.UnitsHolder.Units)
            {
                if (value.isBot)
                    value.BotController.m_Target = transform;
            }
        }
    }
}
