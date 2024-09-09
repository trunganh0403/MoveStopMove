using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameString;

public class WeaponSelector : SelectorBase
{
    [Header("WeaponSelector")]
    [SerializeField] ObjAttack objAttack;
    [SerializeField] WeaponName currentWeapon;
    [SerializeField] WeaponDataSO currentWeaponDataSO;

    [SerializeField] WeaponName[] weapons;
    [SerializeField] List<WeaponDataSO> weaponDataSOs;


    protected override void Awake()
    {
        weapons = new WeaponName[]
        {
            WeaponName.Hammer,
            WeaponName.Candy,
            WeaponName.Axe
        };
    }

    private void Update()
    {
        if(IsWeaponPurchased())
        {
            ToggleDisplay(buyButton.gameObject, false);
            return;
        }
        ToggleDisplay(buyButton.gameObject, true);
    }

    protected override void LoadPreviousSelection()
    {
        if (objAttack == null) return;
        objAttack.ChangeWeapon(GameDataS0.Instance.playerData.selectedWeapon);
    }

    protected override void UpdateSelection()
    {
        if (isColorMode) return;
        currentWeapon = weapons[currentIndex];
        currentWeaponDataSO = GetCurrentWeaponData();

        if (objAttack == null) return;
        objAttack.ChangeWeapon(currentWeapon);
        cost.text = "Cost: " + currentWeaponDataSO.costs.ToString() + ", Atk Range: " + currentWeaponDataSO.atkRange + ", Atk Speed: " +currentWeaponDataSO.atkRange;

        if (!IsWeaponPurchased()) return;
        SaveWeapon();

    }

    protected override int GetSelectionCount()
    {
        return weapons.Length;
    }

    protected override void PurchaseItem()
    {
        if (IsWeaponPurchased()) return;

        if (GameDataS0.Instance.coinSO.currentCoin >= currentWeaponDataSO.costs)
        {
            SaveWeapon();
            AddPurchasedWeapons();
            DeducCoin();
        }
    }

    protected virtual void SaveWeapon()
    {
        GameDataS0.Instance.playerData.selectedWeapon = currentWeapon;
    }

    protected virtual void AddPurchasedWeapons()
    {
        GameDataS0.Instance.playerData.purchasedWeapons.Add(currentWeapon);
    }

    protected virtual void DeducCoin()
    {
        CoinManager.Instance.DeductScore(currentWeaponDataSO.costs);
    }
    protected virtual WeaponDataSO GetCurrentWeaponData()
    {
        foreach (var weapon in weaponDataSOs)
        {
            if (currentWeapon == weapon.weaponName)
            {
                return weapon;
            }
        }
        return null;
    }

    protected virtual bool IsWeaponPurchased()
    {
        foreach (var weapon in GameDataS0.Instance.playerData.purchasedWeapons)
        {
            if (currentWeapon == weapon)
            {
                return true;
            }
        }
        return false;
    }
}
