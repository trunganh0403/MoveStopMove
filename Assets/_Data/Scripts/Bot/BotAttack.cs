using UnityEngine;
using static GameString;

public class BotAttack : ObjAttack
{
    protected override void Start()
    {
        SetRandomWeapon();
        base.Start();
    }

    protected void SetRandomWeapon()
    {
        if (weaponMovements.Count > 0)
        {
            int randomIndex = Random.Range(0, weaponMovements.Count);
            nameWeapon = (WeaponName)System.Enum.Parse(typeof(WeaponName), weaponMovements[randomIndex].name);
        }
    }

    protected override void SetNameWeapon()
    {
        weaponMovementComponent = weaponMovements.Find(w => w.name == nameWeapon.ToString());
    }
}
