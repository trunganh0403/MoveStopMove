using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHome : BaseButton
{
    protected override void OnClick()
    {
        GameManager.Instance.ReturnToFirstScene();
    }
}
