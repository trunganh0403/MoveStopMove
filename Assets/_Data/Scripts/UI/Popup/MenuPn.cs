using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPn : BasePanel
{
    private static MenuPn instance;
    public static MenuPn Instance { get => instance; }

    protected override void Awake()
    {
        if (MenuPn.instance != null) Debug.LogError("Only 1 MenuPn allow to exist");
        MenuPn.instance = this;
    }
}
