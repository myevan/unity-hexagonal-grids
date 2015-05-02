using UnityEngine;

public class H011Controller : MonoBehaviour
{
    void Start()
    {
        var hexagonConfig = new HexagonConfig();
        var cornerPositions = hexagonConfig.GetCornerPositions(Vector3.zero);

        var primitiveManager = gameObject.AddComponent<PrimitiveManager>();
        primitiveManager.SetColor(new Color(0.3f, 0.3f, 0.3f));
        primitiveManager.SetObjectName("edge");
        primitiveManager.MakeLine(cornerPositions[0], cornerPositions[1]);

        primitiveManager.SetColor(new Color(0.5f, 0.1f, 0.1f));
        primitiveManager.SetObjectName("center");
        primitiveManager.MakePoint(Vector3.zero);
        primitiveManager.SetObjectName("corner[{0}]");
        primitiveManager.MakePoints(cornerPositions);

    }
}
