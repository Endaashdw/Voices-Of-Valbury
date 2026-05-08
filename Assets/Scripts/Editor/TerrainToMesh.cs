using UnityEngine;
using UnityEditor;

public class TerrainToMesh : MonoBehaviour
{
    [MenuItem("Terrain/Convert to Mesh")]
    static void Convert()
    {
        Terrain terrain = Selection.activeGameObject.GetComponent<Terrain>();
        if (terrain == null) { Debug.LogError("Select a Terrain object first."); return; }

        TerrainData data = terrain.terrainData;
        int w = data.heightmapResolution;
        int h = data.heightmapResolution;
        float[,] heights = data.GetHeights(0, 0, w, h);

        Vector3[] verts = new Vector3[w * h];
        int[] tris = new int[(w - 1) * (h - 1) * 6];
        Vector2[] uvs = new Vector2[w * h];

        for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                int i = y * w + x;
                verts[i] = new Vector3(
                    (float)x / (w - 1) * data.size.x,
                    heights[y, x] * data.size.y,
                    (float)y / (h - 1) * data.size.z);
                uvs[i] = new Vector2((float)x / (w - 1), (float)y / (h - 1));
            }

        int t = 0;
        for (int y = 0; y < h - 1; y++)
            for (int x = 0; x < w - 1; x++)
            {
                tris[t++] = y * w + x;
                tris[t++] = (y + 1) * w + x;
                tris[t++] = y * w + x + 1;
                tris[t++] = (y + 1) * w + x;
                tris[t++] = (y + 1) * w + x + 1;
                tris[t++] = y * w + x + 1;
            }

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, "Assets/TerrainMesh.asset");

        GameObject go = new GameObject("TerrainMesh");
        go.AddComponent<MeshFilter>().sharedMesh = mesh;
        go.AddComponent<MeshRenderer>();
        go.transform.position = terrain.transform.position;

        Debug.Log("Done! TerrainMesh created in scene.");
    }
}