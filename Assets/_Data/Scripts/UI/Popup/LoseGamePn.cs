using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGamePn : BasePanel
{
    private static LoseGamePn instance;
    public static LoseGamePn Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (LoseGamePn.instance != null) Debug.LogError("Only 1 LoseGamePn allow to exist");
        LoseGamePn.instance = this;
    }
}