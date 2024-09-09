using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected ObjAttack obj;
    [SerializeField] protected int segments = 55;
    [SerializeField] protected float lineWidth = 1f;

    [SerializeField] protected float radius;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = false;

        UpdateRadiusFromObjAttack();
    }

    void Update()
    {
        if (obj == null) return;
        UpdateRadiusFromObjAttack();
    }

    public void UpdateRadiusFromObjAttack()
    {   
        if (obj == null) return;
        obj.SetAtkSpeed();
        radius = obj.AtkRange;
        DrawCircleOnScreen();
    }

    void DrawCircleOnScreen()
    {
        float angle = 360f / segments;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle * i) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle * i) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0.4f, z));
        }
    }

    
}
