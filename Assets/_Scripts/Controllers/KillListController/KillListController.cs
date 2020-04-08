using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.KillListController
{
    public class KillListController : MonoBehaviour
    {
        [SerializeField] private GameObject m_EventMurder;
        [SerializeField] private Transform m_ParrentForEventMurder;

        public void AddKillToList(Unit.UnitController unit, Unit.UnitController damagedUnit, DataWeapons weapon)
        {
            EventMurderParameters parameters;
            GameObject createdMurederEvent = Instantiate(m_EventMurder, m_ParrentForEventMurder) as GameObject;
            
            parameters = createdMurederEvent.GetComponent<EventMurderParameters>();
            parameters.nameUnit1.text = unit.nickname;
            parameters.nameUnit2.text = damagedUnit.nickname;
            parameters.iconWeapon.sprite = weapon.Icon;
        }

        public IEnumerator DeleteKillFromList(DestoryEventMurder deleteEventMurder)
        {
            yield return new WaitForSeconds(deleteEventMurder.timeStartFadeOut);
            deleteEventMurder.isFadeOut = true;
        }
    }
}
