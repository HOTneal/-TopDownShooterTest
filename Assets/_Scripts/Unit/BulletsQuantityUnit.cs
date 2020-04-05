using ScriptableObjects;
using UnityEngine;

namespace Unit
{
    public class BulletsQuantityUnit : MonoBehaviour
    {
        [HideInInspector] public DataWeapons currentWeapon;
        public GameObject[] allWeaponUnit;
        [HideInInspector] public int quantityBulletsInClip;
        [HideInInspector] public int allBulletsWeapon;
        [HideInInspector] public int nextWeapon;
        [HideInInspector] public int lastWeapon;
        [HideInInspector] public int defaultBulletsInClip;
    }
}