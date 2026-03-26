using System.Collections.Generic;
using UnityEngine;

public static class GroupMovementHelper
{
    public static List<Vector3> GetLocations(Vector3 targetPosition, int amount, float spacing = 1f)
    {
        List<Vector3> positions = new List<Vector3>() { targetPosition };

        int count = 1;
        int ring = 1;

        while (count < amount)
        {
            float radius = ring * spacing;

            // Quantidade de pontos nesse anel (ajustável)
            int pointsInRing = Mathf.CeilToInt(2 * Mathf.PI * ring);

            for (int i = 0; i < pointsInRing; i++)
            {
                if (count >= amount)
                    break;

                float angle = (i / (float)pointsInRing) * Mathf.PI * 2;

                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                Vector3 pos = targetPosition + new Vector3(x, 0, z);
                positions.Add(pos);

                count++;
            }

            ring++;
        }

        return positions;
    }
}
