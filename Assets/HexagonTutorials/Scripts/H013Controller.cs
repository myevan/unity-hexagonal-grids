﻿using UnityEngine;

public class H013Controller : MonoBehaviour
{
    public GameObject PHexagon;
    public GameObject FHexagon;

    void Awake()
    {
        _hexagonObject = PHexagon;
        _hexagonConfig = new HexagonConfig(HexagonOrientation.PointyTopped, 0);
        _primitiveManager = gameObject.AddComponent<PrimitiveManager>();
    }

    void Start()
    {
        MakeView();
    }

    void MakeView()
    {
        var originPos = new Vector3(-3, -1);
        var xAxisEndPos = originPos;
        var yAxisEndPos = originPos;
        xAxisEndPos.x += 6f;
        yAxisEndPos.y += 3f;

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

    public void OnClickPointyTopped(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("pointy topped");

        _hexagonConfig.SetOrientation(HexagonOrientation.PointyTopped);
        _hexagonObject = PHexagon;

        MakeView();
    }

    public void OnClickFlatTopped(bool isOn)
    {
        if (!isOn)
            return;

        Debug.Log("flat topped");

        _hexagonConfig.SetOrientation(HexagonOrientation.FlatTopped);
        _hexagonObject = FHexagon;

        MakeView();
    }

    private HexagonConfig _hexagonConfig;
    private GameObject _hexagonObject;
    private PrimitiveManager _primitiveManager;
}
