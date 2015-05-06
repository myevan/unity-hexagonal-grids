using UnityEngine;

public enum HexagonOrientation
{
    PointyTopped,
    FlatTopped,
}

public class HexagonConfig
{
    public HexagonConfig(HexagonOrientation orientation, int remain=1, float edgeLen=0.5f)
    {
        _edgeLen = edgeLen;
        _longLen = edgeLen * 2f;
        _shortLen = edgeLen / 2f * Mathf.Sqrt(3f) * 2f;

        SetOrientation(orientation, remain);
    }

    public void SetOrientation(HexagonOrientation orientation, int remain=1)
    {
        _orientation = orientation;
        
        if (orientation == HexagonOrientation.PointyTopped)
        {
            _baseAngleDeg = 30;
            _step = new Vector2(_shortLen, _longLen / 4f * 3f);
            _size = new Vector2(_shortLen, _longLen);
            _bases[(0 + remain) % 2] = _size * 0.5f;
            _bases[(1 + remain) % 2] = _size * 0.5f;
            _bases[(0 + remain) % 2].x += _step.x / 2f;
        }
        else
        {
            _baseAngleDeg = 0;
            _step = new Vector2(_longLen / 4f * 3f, _shortLen);
            _size = new Vector2(_longLen, _shortLen);
            _bases[(0 + remain) % 2] = _size * 0.5f;
            _bases[(1 + remain) % 2] = _size * 0.5f;
            _bases[(0 + remain) % 2].y += _step.y / 2f;
        }
    }

    public void SetGrowY(float y)
    {
        _grow.y = y;
    }

    public Vector3 GetStep3(float z)
    {
        return new Vector3(_step.x, _step.y, z);
    }

    public Vector3 GetSize3(float z)
    {
        return new Vector3(_size.x, _size.y, z);
    }

    public Vector3 GetOffsetPosition3(Vector3 origin, int q, int r)
    {
        var ret = origin;
        ret.x += (_bases[r & 1].x + q * _step.x) * _grow.x;
        ret.y += (_bases[q & 1].y + r * _step.y) * _grow.y; 
        return ret;
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

    public char Prefix
    {
        get
        {
            return _orientation.ToString()[0];
        }
    }

    public Vector2 Size
    {
        get
        {
            return _size;
        }
    }

    public Vector2 Step
    {
        get
        {
            return _step;
        }
    }

    private HexagonOrientation _orientation = HexagonOrientation.PointyTopped;

    private float _edgeLen = 0.5f;
    private float _baseAngleDeg = 30;

    private float _longLen = 0f;
    private float _shortLen = 0f;

    private Vector2 _grow = Vector2.one;
    private Vector2 _size = Vector2.zero;
    private Vector2 _step = Vector2.zero;
    private Vector2[] _bases = new Vector2[] 
    { 
        Vector2.zero, 
        Vector2.zero 
    };

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
