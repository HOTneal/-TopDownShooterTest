using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomWeaponBot : MonoBehaviour
{
    public int RandomIdWeapon()
    {
        Random random = new Random();
        return random.Next(0, 3);
    }
}