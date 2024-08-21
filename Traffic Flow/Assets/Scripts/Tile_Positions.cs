using UnityEngine;

public static class GridData
{
    public static readonly Vector3[] validPositions = new Vector3[]
    {
        new Vector3(-2.2f, 1.1f, 0f),
        new Vector3(-1.1f, 1.1f, 0f),
        new Vector3(0f, 1.1f, 0f),
        new Vector3(1.1f, 1.1f, 0f),
        new Vector3(2.2f, 1.1f, 0f),

        new Vector3(-2.2f, 0f, 0f),
        new Vector3(-1.1f, 0f, 0f),
        new Vector3(0f, 0f, 0f),
        new Vector3(1.1f, 0f, 0f),
        new Vector3(2.2f, 0f, 0f),

        // Added 3 extra rows
        new Vector3(-2.2f, -1.1f, 0f),
        new Vector3(-1.1f, -1.1f, 0f),
        new Vector3(0f, -1.1f, 0f),
        new Vector3(1.1f, -1.1f, 0f),
        new Vector3(2.2f, -1.1f, 0f),

        new Vector3(-2.2f, -2.2f, 0f),
        new Vector3(-1.1f, -2.2f, 0f),
        new Vector3(0f, -2.2f, 0f),
        new Vector3(1.1f, -2.2f, 0f),
        new Vector3(2.2f, -2.2f, 0f),

        new Vector3(-2.2f, -3.3f, 0f),
        new Vector3(-1.1f, -3.3f, 0f),
        new Vector3(0f, -3.3f, 0f),
        new Vector3(1.1f, -3.3f, 0f),
        new Vector3(2.2f, -3.3f, 0f),
    };
}
