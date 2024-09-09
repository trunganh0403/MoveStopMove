using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameString;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "SO/WeaponDataSO")]

public class WeaponDataSO : ScriptableObject
{
    public WeaponName weaponName;
    public int costs;
    public float atkRange;
    public float atkSpeed;
}
