using UnityEngine;

public class PrimitiveManager : MonoBehaviour
{
    private void Awake()
    {
        _cachedTransform = this.transform;
        _curShader = Shader.Find("UI/Default");
    }

    public void Clear()
    {
        _curSortingOrder = 0;

        foreach (Transform childTransform in _cachedTransform)
        {
            childTransform.gameObject.name = "";
            Destroy(childTransform.gameObject);
        }
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

    public void SetLineWidth(float width)
    {
        _curLineWidth = width;
    }

    public void MakePoints(Vector3[] positions)
    {
        var localScale = Vector3.one * _curPointRadius;

        for (int i = 0; i != positions.Length; ++i)
        {
            var eachPos = positions[i];
            var newObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
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

        var newObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
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
        newLineRenderer.useWorldSpace = false;

        newLineRenderer.sortingOrder = AllocSortingOrder();
    }

    public void MakeClosedLines(Vector3[] positions)
    {
        var newObject = new GameObject();
        newObject.name = _curObjectName; 

        var newObjectTransform = newObject.GetComponent<Transform>();
        newObjectTransform.parent = _cachedTransform; 
        newObjectTransform.localPosition = Vector3.zero;
        newObjectTransform.localScale = Vector3.one;

        var newLineRenderer = newObject.AddComponent<LineRenderer>();
        newLineRenderer.SetWidth(_curLineWidth, _curLineWidth);
        newLineRenderer.SetVertexCount(positions.Length + 1);
        for (int i = 0; i != positions.Length; ++i)
            newLineRenderer.SetPosition(i, positions[i]);
        newLineRenderer.SetPosition(positions.Length, positions[0]);
        newLineRenderer.sharedMaterial = _curMaterial;
        newLineRenderer.useWorldSpace = false;

        newLineRenderer.sortingOrder = AllocSortingOrder();
    }

    public void MakePolygon(Vector3[] positions, int[] indices)
    {
        var newMesh = new Mesh();
        newMesh.name = "DynamicMesh";
        newMesh.vertices = positions;
        newMesh.triangles = indices;

        var newObject = new GameObject();
        newObject.name = _curObjectName;

        var newObjectTransform = newObject.GetComponent<Transform>();
        newObjectTransform.parent = _cachedTransform; 
        newObjectTransform.localPosition = Vector3.zero;
        newObjectTransform.localScale = Vector3.one; 

        var newMeshFitler = newObject.AddComponent<MeshFilter>();
        newMeshFitler.mesh = newMesh;
        
        var newMeshRenderer = newObject.AddComponent<MeshRenderer>();
        newMeshRenderer.sharedMaterial = _curMaterial;
        newMeshRenderer.receiveShadows = false;
        newMeshRenderer.useLightProbes = false;
        newMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;        
        newMeshRenderer.sortingOrder = AllocSortingOrder();
    }

    private int AllocSortingOrder()
    {
        return _curSortingOrder++;
    }

    public Transform CachedTransform
    {
        get
        {
            return _cachedTransform;
        }
    }

    private Transform _cachedTransform;
    private Shader _curShader;
    private Material _curMaterial;
    private int _curSortingOrder = 0;
    private float _curLineWidth = 0.1f;
    private float _curPointRadius = 0.1f;
    private string _curObjectName = "";

}