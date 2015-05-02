using UnityEngine;

public class PrimitiveManager : MonoBehaviour
{
    private void Awake()
    {
        _cachedTransform = this.transform;
        _curShader = Shader.Find("UI/Default");
    }

    public void SetObjectName(string objName)
    {
        _curObjectName = objName;
    }

    public void SetColor(Color color)
    {
        _curMaterial = new Material(_curShader);
        _curMaterial.color = color;
    }

    public void MakePoints(Vector3[] positions)
    {
        var localScale = Vector3.one * _curPointRadius;

        for (int i = 0; i != positions.Length; ++i)
        {
            var eachPos = positions[i];
            var newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObject.name = string.Format(_curObjectName, i);

            var newObjectTransform = newObject.GetComponent<Transform>();
            newObjectTransform.parent = _cachedTransform; 
            newObjectTransform.localPosition = eachPos;
            newObjectTransform.localScale = localScale;

            var newMeshRenderer = newObject.GetComponent<MeshRenderer>();
            newMeshRenderer.sharedMaterial = _curMaterial;
            newMeshRenderer.sortingOrder = AllocSortingOrder();
        }
    }

    public void MakePoint(Vector3 position)
    {
        var localScale = Vector3.one * _curPointRadius;

        var newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newObject.name = _curObjectName;

        var newObjectTransform = newObject.GetComponent<Transform>();
        newObjectTransform.parent = _cachedTransform; 
        newObjectTransform.localPosition = position;
        newObjectTransform.localScale = localScale; 

        var newMeshRenderer = newObject.GetComponent<MeshRenderer>();
        newMeshRenderer.sharedMaterial = _curMaterial;
        newMeshRenderer.sortingOrder = AllocSortingOrder();
    }

    public void MakeLine(Vector3 sPosition, Vector3 ePosition)
    {
        var newObject = new GameObject();
        newObject.name = _curObjectName; 

        var newObjectTransform = newObject.GetComponent<Transform>();
        newObjectTransform.parent = _cachedTransform; 
        newObjectTransform.localPosition = Vector3.zero;
        newObjectTransform.localScale = Vector3.one;

        var newLineRenderer = newObject.AddComponent<LineRenderer>();
        newLineRenderer.SetWidth(_curLineWidth, _curLineWidth);
        newLineRenderer.SetVertexCount(2);
        newLineRenderer.SetPosition(0, sPosition);
        newLineRenderer.SetPosition(1, ePosition);
        newLineRenderer.sharedMaterial = _curMaterial;
        newLineRenderer.sortingOrder = AllocSortingOrder();
    }

    private int AllocSortingOrder()
    {
        return _curSortingOrder++;
    }

    private Transform _cachedTransform;
    private Shader _curShader;
    private Material _curMaterial;
    private int _curSortingOrder = 0;
    private float _curLineWidth = 0.1f;
    private float _curPointRadius = 0.1f;
    private string _curObjectName = "";
}