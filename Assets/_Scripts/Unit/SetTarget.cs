using Managers;
using UnityEngine;

namespace Unit
{
    public class SetTarget : MonoBehaviour
    {
        private LinkManager m_LinkManager;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
        }

        public void SetTargetForBotAttack()
        {
            foreach (var value in LinkManager.instance.unitsHolder.Units)
            {
                if (value.isBot)
                    value.botController.target = transform;
            }
        }
    }
}
