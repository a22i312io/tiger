using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    [SerializeField] private int segments = 50; // •ªŠ„”
    [SerializeField] private float radius = 5f; // ”¼Œa
    [SerializeField] private float lineWidth = 0.1f; // ü‚Ì•

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // ’¸“_‚Ì”
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        DrawCircle();
    }

    void DrawCircle()
    {
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }
}