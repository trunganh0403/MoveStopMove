using UnityEngine;

public class BuildingTransparency : MonoBehaviour
{
    public MeshRenderer buildingRenderer;

    void Start()
    {
        buildingRenderer = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Thay đổi màu sắc để tòa nhà trở nên bán trong suốt
            Color color = buildingRenderer.material.color;
            color.a = 0.5f; // Đặt độ trong suốt (0.5f là giá trị bán trong suốt)
            buildingRenderer.material.color = color;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Khôi phục lại màu sắc gốc (hoàn toàn đục)
            Color color = buildingRenderer.material.color;
            color.a = 1f;
            buildingRenderer.material.color = color;
        }
    }
}
