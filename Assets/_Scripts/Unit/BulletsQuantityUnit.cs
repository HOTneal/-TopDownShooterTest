using UnityEngine;

namespace Unit
{
    public class BulletsQuantityUnit : MonoBehaviour
    {
        [HideInInspector] public DataWeapons CurrentWeapon;
        public GameObject[] AllWeaponUnit;
        [HideInInspector] public int QuantityBulletsInClip;
        [HideInInspector] public int AllBulletsWeapon;
        [HideInInspector] public int NextWeapon;
        [HideInInspector] public int LastWeapon;
        [HideInInspector] public int DefaultBulletsInClip;
    }
}