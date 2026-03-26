using UnityEngine;

public static class GeometryHelper
{
    public static (Vector3, Vector3) GetBox(Vector3 p1, Vector3 p2)
    {
        var minPoint = Vector3.Min(p1, p2);
        var maxPoint = Vector3.Max(p1, p2);

        var center = (minPoint + maxPoint) * 0.5f;
        var size = (maxPoint - minPoint) * 0.5f;

        return (center, size);
    }

    public static bool IsPointInsideRect(Vector2 p1, Vector2 p2, Vector2 targetPoint)
    {
        var minPoint = Vector2.Min(p1, p2);
        var maxPoint = Vector2.Max(p1, p2);

        var xInside = targetPoint.x >= minPoint.x && targetPoint.x <= maxPoint.x;
        var yInside = targetPoint.y >= minPoint.y && targetPoint.y <= maxPoint.y;

        return xInside && yInside;
    }

    public static Rect GetRect(Vector2 p1, Vector2 p2)
    {
        var minPoint = Vector2.Min(p1, p2);
        var maxPoint = Vector2.Max(p1, p2);

        var size = maxPoint - minPoint;
        
        return new Rect(minPoint, size);
    }

    public static (Vector3, float) GetSphere(Vector3 p1, Vector3 p2)
    {
        var center = (p1 + p2) * 0.5f;
        var radius = Vector3.Distance(p1, p2) / 2f;

        return (center, radius);
    }
}
