using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        Color selectedColor = GameDataS0.Instance.playerData.selectedColor;

        if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null)
        {
            skinnedMeshRenderer.material.color = selectedColor;
        }
    }
}
