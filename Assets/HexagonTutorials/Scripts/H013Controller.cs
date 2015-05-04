using UnityEngine;

public class H013Controller : MonoBehaviour
{
    void Awake()
    {
        _hexagonConfig = new HexagonConfig(HexagonOrientation.PointyTopped);
        _primitiveManager = gameObject.AddComponent<PrimitiveManager>();
    }

    void Start()
    {
        MakeView();
    }

    void MakeView()
    {
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
