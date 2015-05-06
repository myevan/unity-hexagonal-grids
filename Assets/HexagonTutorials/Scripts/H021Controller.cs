using UnityEngine;

public class H021Controller : MonoBehaviour
{
    public GameObject PHexagon;
    public GameObject FHexagon;

    void Awake()
    {
        _hexagonObject = PHexagon;
        _hexagonConfig = new HexagonConfig(HexagonOrientation.PointyTopped, 1);
        _hexagonConfig.SetGrowY(-1f);
        _primitiveManager = gameObject.AddComponent<PrimitiveManager>();
    }

    void Start()
    {
        MakeView();
    }

    void MakeView()
    {
        var originPos = new Vector3(-3, +2);
        var xAxisEndPos = originPos;
        var yAxisEndPos = originPos;
        xAxisEndPos.x += 6f;
        yAxisEndPos.y -= 3f;

        _primitiveManager.Clear();
        _primitiveManager.SetColor(new Color(0.1f, 0.1f, 0.1f));
        _primitiveManager.SetLineWidth(0.02f);
        _primitiveManager.SetObjectName("axis:X");
        _primitiveManager.MakeLine(originPos, xAxisEndPos);
        _primitiveManager.SetObjectName("axis:y");
        _primitiveManager.MakeLine(originPos, yAxisEndPos);

        for (int r = 0; r != 3; ++r)
        {
            for (int q = 0; q != 6; ++q)
            {
                _primitiveManager.SetObjectName(
                    string.Format("hexagon:{0}x{1}", q, r));

                var eachPos = _hexagonConfig.GetOffsetPosition3(originPos, q, r);
                _primitiveManager.MakeClone(_hexagonObject, eachPos);
            }
        }
    }

    public void OnClickOddR(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("odd-r");

        _hexagonConfig.SetOrientation(HexagonOrientation.PointyTopped, 1);
        _hexagonObject = PHexagon;

        MakeView();
    }

    public void OnClickEvenR(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("even-r");

        _hexagonConfig.SetOrientation(HexagonOrientation.PointyTopped, 2);
        _hexagonObject = PHexagon;

        MakeView();
    }

    public void OnClickOddQ(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("odd-q");

        _hexagonConfig.SetOrientation(HexagonOrientation.FlatTopped, 1);
        _hexagonObject = FHexagon;

        MakeView();
    }

    public void OnClickEvenQ(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("even-q");

        _hexagonConfig.SetOrientation(HexagonOrientation.FlatTopped, 2);
        _hexagonObject = FHexagon;

        MakeView();
    }

    private HexagonConfig _hexagonConfig;
    private GameObject _hexagonObject;
    private PrimitiveManager _primitiveManager;
}
