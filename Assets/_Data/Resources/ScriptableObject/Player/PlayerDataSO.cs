using System;
using System.Collections.Generic;
using UnityEngine;
using static GameString;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "SO/PlayerDataSO")]

public class PlayerDataSO : ScriptableObject
{
    public List<WeaponName> purchasedWeapons;
    public Color selectedColor = Color.white;
    public WeaponName selectedWeapon = WeaponName.Hammer;
}
