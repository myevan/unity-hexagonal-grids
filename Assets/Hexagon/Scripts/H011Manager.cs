using UnityEngine;

public class H011Manager : MonoBehaviour
{
    void Start()
    {
        var hexConfig = new HexConfig();
        var cornerPositions = hexConfig.GetCornerPositions(Vector3.zero);

        var primitiveManager = gameObject.AddComponent<PrimitiveManager>();
        primitiveManager.MakePoints(0.1f, cornerPositions, "corner[{0}]");
        primitiveManager.MakePoint(0.1f, Vector3.zero, "center");
    }
}
