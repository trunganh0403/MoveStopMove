using UnityEngine;

public class ColorSelector : SelectorBase
{
    [Header("ColorSelector")]
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Color[] colors;
    public Color curentColor;

    protected override void Awake()
    {
        base.Awake();
        colors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan
        };
    }


    protected override void LoadPreviousSelection()
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material.color = GameDataS0.Instance.playerData.selectedColor;
        }
    }

    protected override void UpdateSelection()
    {
        if (isColorMode)
        {
            curentColor = colors[currentIndex];
            if (skinnedMeshRenderer != null)
            {
                skinnedMeshRenderer.material.color = curentColor;
                GameDataS0.Instance.playerData.selectedColor = curentColor;

            }
        }
    }

    protected override int GetSelectionCount()
    {
        return colors.Length;
    }

    protected override void PurchaseItem()
    {
       
    }
}
