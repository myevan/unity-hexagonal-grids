using UnityEngine;

public class H011Controller : MonoBehaviour
{
    public ObjectAnchor centerAnchor;
    public ObjectAnchor cornerAnchor;
    public ObjectAnchor edgeAnchor;

    void Start()
    {
        var hexagonConfig = new HexagonConfig();
        var vertexPositions = hexagonConfig.GetVertexPositions(Vector3.zero);
        var cornerPositions = hexagonConfig.GetCornerPositions(Vector3.zero);

        var primitiveManager = gameObject.AddComponent<PrimitiveManager>();
        primitiveManager.SetColor(new Color(0.5f, 0.5f, 0.5f));
        primitiveManager.SetObjectName("hexagon");
        primitiveManager.MakeHexagon(vertexPositions);

        primitiveManager.SetColor(new Color(0.3f, 0.3f, 0.3f));
        primitiveManager.SetObjectName("outline");
        primitiveManager.SetLineWidth(0.05f);
        primitiveManager.MakeClosedLines(cornerPositions);

        primitiveManager.SetColor(new Color(0.5f, 0.1f, 0.1f));
        primitiveManager.SetObjectName("edge");
        primitiveManager.MakeLine(cornerPositions[4], cornerPositions[5]);

        primitiveManager.SetObjectName("edge_center");
        primitiveManager.MakePoint((cornerPositions[4] + cornerPositions[5]) / 2 );
        primitiveManager.SetObjectName("center");
        primitiveManager.MakePoint(Vector3.zero);
        primitiveManager.SetObjectName("corner[{0}]");
        primitiveManager.MakePoints(cornerPositions);

        centerAnchor.SetTargetTransform(
            primitiveManager.CachedTransform.Find("center"));

        cornerAnchor.SetTargetTransform(
            primitiveManager.CachedTransform.Find("corner[0]"));

        edgeAnchor.SetTargetTransform(
            primitiveManager.CachedTransform.Find("edge_center"));
    }
}
