using UnityEngine;

public class H011Controller : MonoBehaviour
{
    public ObjectAnchor centerAnchor;
    public ObjectAnchor cornerAnchor;
    public ObjectAnchor edgeAnchor;

    void Awake()
    {
        _hexagonConfig = new HexagonConfig();
        _hexagonConfig.SetOrientation(HexagonOrientation.PointyTopped);
        _primitiveManager = gameObject.AddComponent<PrimitiveManager>();
    }

    void Start()
    {
        MakeView();
    }

    void MakeView()
    {
        var vertexPositions = _hexagonConfig.GetVertexPositions(Vector3.zero);
        var cornerPositions = _hexagonConfig.GetCornerPositions(Vector3.zero);

        _primitiveManager.Clear();

        _primitiveManager.SetColor(new Color(0.5f, 0.5f, 0.5f));
        _primitiveManager.SetObjectName("hexagon");
        _primitiveManager.MakeHexagon(vertexPositions);

        _primitiveManager.SetColor(new Color(0.3f, 0.3f, 0.3f));
        _primitiveManager.SetObjectName("outline");
        _primitiveManager.SetLineWidth(0.05f);
        _primitiveManager.MakeClosedLines(cornerPositions);

        _primitiveManager.SetColor(new Color(0.5f, 0.1f, 0.1f));
        _primitiveManager.SetObjectName("edge");
        _primitiveManager.MakeLine(cornerPositions[4], cornerPositions[5]);

        _primitiveManager.SetObjectName("edge_center");
        _primitiveManager.MakePoint((cornerPositions[4] + cornerPositions[5]) / 2 );
        _primitiveManager.SetObjectName("center");
        _primitiveManager.MakePoint(Vector3.zero);
        _primitiveManager.SetObjectName("corner[{0}]");
        _primitiveManager.MakePoints(cornerPositions);

        centerAnchor.SetTargetTransform(
            _primitiveManager.CachedTransform.Find("center"));

        cornerAnchor.SetTargetTransform(
            _primitiveManager.CachedTransform.Find("corner[0]"));

        edgeAnchor.SetTargetTransform(
            _primitiveManager.CachedTransform.Find("edge_center"));
    }

    public void OnClickPointyTopped(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("pointy topped");

        _hexagonConfig.SetOrientation(HexagonOrientation.PointyTopped);

        MakeView();
    }

    public void OnClickFlatTopped(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("flat topped");

        _hexagonConfig.SetOrientation(HexagonOrientation.FlatTopped);

        MakeView();
    }

    private HexagonConfig _hexagonConfig;
    private PrimitiveManager _primitiveManager;
}
