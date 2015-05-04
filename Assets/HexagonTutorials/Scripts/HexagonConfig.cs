using UnityEngine;

public enum HexagonOrientation
{
    PointyTopped,
    FlatTopped,
}

public class HexagonConfig
{
    public HexagonConfig(HexagonOrientation orientation)
    {
        SetOrientation(orientation);
    }

    public void SetOrientation(HexagonOrientation orientation)
    {
        _orientation = orientation;
        
        if (orientation == HexagonOrientation.PointyTopped)
            _baseAngleDeg = 30;
        else
            _baseAngleDeg = 0;
    }

    public void SetEdgeLen(float edgeLen)
    {
        _edgeLen = edgeLen;
    }

    public float GetCornerAngleDeg(int cornerIndex)
    {
        return _baseAngleDeg + WedgeAngleDeg * cornerIndex;
    }

    public float GetCornerAngleRad(int cornerIndex)
    {
        return Mathf.Deg2Rad * GetCornerAngleDeg(cornerIndex);
    }

    public void GetCornerPosition(
        int cornerIndex,
        float centerX, float centerY,
        out float outCornerX, out float outCornerY)
    {
        var cornerAngleRad = GetCornerAngleRad(cornerIndex);
        outCornerX = centerX + _edgeLen * Mathf.Cos(cornerAngleRad);
        outCornerY = centerY + _edgeLen * Mathf.Sin(cornerAngleRad);
    }

    public Vector2 GetCornerPosition(int cornerIndex, Vector2 center)
    {
        float cornerX, cornerY;
        GetCornerPosition(
            cornerIndex, center.x, center.y,
            out cornerX, out cornerY);

        return new Vector2(cornerX, cornerY);
    }

    public Vector3 GetCornerPosition(int cornerIndex, Vector3 center)
    {
        float cornerX, cornerY;
        GetCornerPosition(
            cornerIndex, center.x, center.y,
            out cornerX, out cornerY);

        return new Vector3(cornerX, cornerY, center.z);
    }

    public Vector3[] GetCornerPositions(Vector3 center)
    {
        var cornerPositions = new Vector3[CornerCount];
        for (int cornerIndex = 0; cornerIndex != CornerCount; ++cornerIndex)
        {
            cornerPositions[cornerIndex] = GetCornerPosition(
                cornerIndex, center);
        }
        return cornerPositions;
    }

    public Vector3[] GetVertexPositions(Vector3 center)
    {
        var vertexPositions = new Vector3[VertexCount];
        vertexPositions[0] = center;
        for (int vertexIndex = 1; vertexIndex != VertexCount; ++vertexIndex)
        {
            vertexPositions[vertexIndex] = GetCornerPosition(
                vertexIndex, center);
        }
        return vertexPositions;
    }

    public Vector3[] GetVertexNormals()
    {
        var normal = new Vector3(0f, 0f, -1f);
        var vertexNormals = new Vector3[VertexCount];
        for (int vertexIndex = 0; vertexIndex != VertexCount; ++vertexIndex)
        {
            vertexNormals[vertexIndex] = normal; 
        }
        return vertexNormals;
    }

    public Vector2[] GetVertexUVs(Vector2 center)
    {
        var vertexUVs = new Vector2[VertexCount];
        vertexUVs[0] = center;
        for (int vertexIndex = 1; vertexIndex != VertexCount; ++vertexIndex)
        {
            vertexUVs[vertexIndex] = GetCornerPosition(
                vertexIndex, center);
        }
        return vertexUVs;
    }

    public Color[] GetVertexColors(Color color)
    {
        var vertexColors = new Color[VertexCount];
        for (int vertexIndex = 0; vertexIndex != VertexCount; ++vertexIndex)
        {
            vertexColors[vertexIndex] = color;
        }
        return vertexColors;
    }

    public HexagonOrientation Orientation
    {
        get
        {
            return _orientation;
        }
    }

    private HexagonOrientation _orientation = HexagonOrientation.PointyTopped;

    private float _edgeLen = 0.5f;
    private float _baseAngleDeg = 30;

    public static readonly float WedgeAngleDeg = 60;
    public static readonly int CornerCount = 6;
    public static readonly int VertexCount = 6 + 1;

    public static readonly int[] FanIndices = new int[18]
    {
        2, 1, 0,
        3, 2, 0,
        4, 3, 0,
        5, 4, 0,
        6, 5, 0,
        1, 6, 0,
    };
}
