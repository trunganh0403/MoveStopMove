using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeapon : Spawner
{
    [Header("SpawnerWeapon")]
    private static SpawnerWeapon instance;
    public static SpawnerWeapon Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (SpawnerWeapon.instance != null) Debug.LogError("Only 1 SpawnerWeapon allow to exist");
        SpawnerWeapon.instance = this;
    }
}
