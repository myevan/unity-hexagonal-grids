using UnityEngine;

public static class DynamicPrimitiveFactory
{
    public static Mesh CreateQuadMesh(string meshName)
    {
        var positions = new Vector3[4];
        positions[0] = new Vector3(-0.5f, -0.5f, 0f);
        positions[1] = new Vector3(+0.5f, -0.5f, 0f);
        positions[2] = new Vector3(-0.5f, +0.5f, 0f);
        positions[3] = new Vector3(+0.5f, +0.5f, 0f);
        
        var texCoords = new Vector2[4];
        texCoords[0] = new Vector2(0f, 0f);
        texCoords[1] = new Vector2(0f, 1f);
        texCoords[2] = new Vector2(1f, 0f);
        texCoords[3] = new Vector2(1f, 1f);
        
        var indices = new int[6];
        indices[0] = 0;
        indices[1] = 2;
        indices[2] = 1;

        indices[3] = 1;
        indices[4] = 2;
        indices[5] = 3;

        var newMesh = new Mesh();
        newMesh.name = meshName;
        newMesh.vertices = positions;
        newMesh.triangles = indices;
        newMesh.uv = texCoords;
        return newMesh;
    }
}