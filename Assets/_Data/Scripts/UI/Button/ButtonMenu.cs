using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenu : BaseButton
{
    protected override void OnClick()
    {
        MenuPn.Instance.Toggle();
    }
}
