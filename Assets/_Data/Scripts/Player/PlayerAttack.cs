using UnityEngine;
using static GameString;

public class PlayerAttack : ObjAttack
{
    protected override void Start()
    {
        LoadSelectedWeapon();
        base.Start();
    }

    public void LoadSelectedWeapon()
    {
        WeaponName weaponName = GameDataS0.Instance.playerData.selectedWeapon;
        nameWeapon = weaponName;
    }

    protected override void SetNameWeapon()
    {
        weaponMovementComponent = weaponMovements.Find(w => w.name == nameWeapon.ToString());
    }
}
