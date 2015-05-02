using UnityEngine;

public class HexagonConfig
{
    public enum Orientation
    {
        PointyTopped,
        FlatTopped,
    }

    public Orientation CurrentOrientation
    {
        get
        {
            return _curOrientation;
        }
    }

    public void SetEdgeLen(float edgeLen)
    {
        _edgeLen = edgeLen;
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

    private Orientation _curOrientation = Orientation.PointyTopped;

    private float _edgeLen = 1;
    private float _baseAngleDeg = 30;

    private const float _wedgeAngleDeg = 60;
    private const int _cornerCount = 6;
}
