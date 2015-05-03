using UnityEngine;

public enum HexagonOrientation
{
    PointyTopped,
    FlatTopped,
}

public class HexagonConfig
{
    public HexagonOrientation CurrentOrientation
    {
        get
        {
            return _orientation;
        }
    }

    public void SetEdgeLen(float edgeLen)
    {
        _edgeLen = edgeLen;
    }

    public void SetOrientation(HexagonOrientation orientation)
    {
        _orientation = orientation;
        
        if (orientation == HexagonOrientation.PointyTopped)
            _baseAngleDeg = 30;
        else
            _baseAngleDeg = 0;
    }

    public float GetCornerAngleDeg(int cornerIndex)
    {
        return _baseAngleDeg + _wedgeAngleDeg * cornerIndex;
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
        var cornerPositions = new Vector3[_cornerCount];
        for (int cornerIndex = 0; cornerIndex != _cornerCount; ++cornerIndex)
        {
            cornerPositions[cornerIndex] = GetCornerPosition(cornerIndex, center);
        }
        return cornerPositions;
    }

    public Vector3[] GetVertexPositions(Vector3 center)
    {
        var vertexPositions = new Vector3[_vertexCount + 1];
        vertexPositions[0] = center;
        for (int vertexIndex = 1; vertexIndex != _vertexCount; ++vertexIndex)
        {
            vertexPositions[vertexIndex] = GetCornerPosition(vertexIndex, center);
        }
        return vertexPositions;
    }

    private HexagonOrientation _orientation = HexagonOrientation.PointyTopped;

    private float _edgeLen = 1;
    private float _baseAngleDeg = 30;

    private const float _wedgeAngleDeg = 60;
    private const int _cornerCount = 6;
    private const int _vertexCount = 6 + 1;
}
