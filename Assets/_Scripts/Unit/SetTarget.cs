using Managers;
using UnityEngine;

namespace Unit
{
    public class SetTarget : MonoBehaviour
    {
        public void SetTargetForBotAttack()
        {
            foreach (var value in LinkManager.instance.unitsHolder.Units)
            {
                if (value.isBot)
                    value.bot.target = transform;
            }
        }
    }
}
