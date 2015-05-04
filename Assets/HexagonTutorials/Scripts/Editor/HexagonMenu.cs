using UnityEngine;
using UnityEditor;
using System.IO;

public class HexagonMenu : MonoBehaviour
{
    [MenuItem("GameObject/3D Object/PHexagon")]
    public static void ExportPointyToppedHexagon()
    {
        ExportHexagon(PointyToppedHexagonConfig);
    }

    [MenuItem("GameObject/3D Object/FHexagon")]
    public static void ExportFlatToppedHexagon()
    {
        ExportHexagon(FlatToppedHexagonConfig);
    }

    public static void ExportHexagon(HexagonConfig hexagonConfig)
    {
        var hexagonName = hexagonConfig.Orientation.ToString()[0] + "Hexagon";

        var newObject = new GameObject();
        newObject.name = hexagonName;

        var newObjectTransform = newObject.GetComponent<Transform>();
        newObjectTransform.localPosition = Vector3.zero;
        newObjectTransform.localScale = Vector3.one;

        var newMeshFitler = newObject.AddComponent<MeshFilter>();
        newMeshFitler.mesh = PrepareHexagonMesh(hexagonConfig, hexagonName);

        var newMeshRenderer = newObject.AddComponent<MeshRenderer>();
        newMeshRenderer.sharedMaterial =
            AssetDatabase.GetBuiltinExtraResource<Material>(
                "Default-Material.mat");
    }

    public static Mesh PrepareHexagonMesh(
        HexagonConfig hexagonConfig, string hexagonName)
    {
        var meshPath = Path.Combine(HexagonModelDirPath, hexagonName + ".obj");

        var oldMesh = AssetDatabase.LoadMainAssetAtPath(meshPath) as Mesh;
        if (oldMesh != null)
            return oldMesh;

        var meshFolderPath = System.IO.Path.GetDirectoryName(meshPath);
        PrepareFolder(meshFolderPath);

        var newMesh = new Mesh();
        newMesh.name = hexagonName;
        newMesh.vertices = hexagonConfig.GetVertexPositions(Vector3.zero);
        newMesh.uv = hexagonConfig.GetVertexUVs(Vector2.zero);
        newMesh.colors = hexagonConfig.GetVertexColors(Color.white);
        newMesh.normals = hexagonConfig.GetVertexNormals();
        newMesh.triangles = HexagonConfig.FanIndices;

        ExportMesh(newMesh, meshPath);
        AssetDatabase.Refresh();

        return newMesh;
    }

    public static void PrepareFolder(string folderPath)
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            var branchPath = System.IO.Path.GetDirectoryName(folderPath);
            PrepareFolder(branchPath);

            var leafName = folderPath.Substring(branchPath.Length + 1);
            AssetDatabase.CreateFolder(branchPath, leafName);
        }
    }

    public static void ExportMesh(Mesh mesh, string meshPath)
    {
        var sb = new System.Text.StringBuilder();

        sb.Append("g ").Append(mesh.name).Append("\n");

        foreach (Vector3 v in mesh.vertices)
        {
            sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");

        foreach (Vector3 v in mesh.normals)
        {
            sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");

        foreach (Vector2 v in mesh.uv)
        {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }
        sb.Append("\n");

        foreach (Color c in mesh.colors)
        {
            sb.Append(string.Format("vc {0} {1} {2} {3}\n", c.r, c.g, c.b, c.a));
        }
        sb.Append("\n");

        for (int i = 0; i != 18; i += 3)
        {
            int a = mesh.triangles[i + 0] + 1;
            int b = mesh.triangles[i + 1] + 1;
            int c = mesh.triangles[i + 2] + 1;
            sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", a, b, c));
        }
        sb.Append("\n");

        System.IO.File.WriteAllText(meshPath, sb.ToString());
    }

    private static readonly string HexagonModelDirPath = "Assets/HexagonTutorials/Models";

    private static HexagonConfig PointyToppedHexagonConfig =
        new HexagonConfig(HexagonOrientation.PointyTopped);

    private static HexagonConfig FlatToppedHexagonConfig =
        new HexagonConfig(HexagonOrientation.FlatTopped);

}