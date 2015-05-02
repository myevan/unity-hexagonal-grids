using UnityEngine;
using UnityEditor;

public static class DynamicPrimitiveMenus
{
    [MenuItem("GameObject/3D Object/Dynamic Quad")]
    public static void CreateDynamicQuad()
    {
        var newObject = new GameObject();
        newObject.name = "Dynamic Quad";

        var newMeshFitler = newObject.AddComponent<MeshFilter>();
        newMeshFitler.mesh = GetSharedQuadMesh();
        
        var newMeshRenderer = newObject.AddComponent<MeshRenderer>();
        newMeshRenderer.sharedMaterials = GetSharedMaterials();
        newMeshRenderer.receiveShadows = false;
        newMeshRenderer.useLightProbes = false;
        newMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public static Mesh GetSharedQuadMesh()
    {
        if (_sharedMesh == null)
        {
            _sharedMesh = DynamicPrimitiveFactory.CreateQuadMesh("DynamicSharedQuadMesh");
        }

        return _sharedMesh;
    }

    public static Material[] GetSharedMaterials()
    {
        if (_sharedMaterials == null) 
        {
            Debug.Log("create_dynamic_shared_materials");

            var defaultShader = Shader.Find("Unlit/Color");
            var defaultMaterial = new Material(defaultShader);
            defaultMaterial.name = "DynamicSharedMaterial";
            _sharedMaterials = new Material[] {
                defaultMaterial,
            };
        }
        return _sharedMaterials;
    }

    private static Mesh _sharedMesh;
    private static Material[] _sharedMaterials;
}
