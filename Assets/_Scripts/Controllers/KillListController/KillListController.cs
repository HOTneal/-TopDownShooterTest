using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Controllers.KillListController
{
    public class KillListController : MonoBehaviour
    {
        [SerializeField] private GameObject m_EventMurder;
        [SerializeField] private Transform m_ParrentForEventMurder;

        public void AddKillToList(Unit.Unit unit, Unit.Unit damagedUnit, DataWeapons weapon)
        {
            EventMurderParameters parameters;
            GameObject createdMurederEvent = Instantiate(m_EventMurder, m_ParrentForEventMurder) as GameObject;
            
            parameters = createdMurederEvent.GetComponent<EventMurderParameters>();
            parameters.NameUnit1.text = unit.Nickname;
            parameters.NameUnit2.text = damagedUnit.Nickname;
            parameters.IconWeapon.sprite = weapon.Icon;
        }

        public IEnumerator DeleteKillFromList(DestoryEventMurder deleteEventMurder)
        {
            yield return new WaitForSeconds(deleteEventMurder.TimeStartFadeOut);
            deleteEventMurder.isFadeOut = true;
        }
    }
}
