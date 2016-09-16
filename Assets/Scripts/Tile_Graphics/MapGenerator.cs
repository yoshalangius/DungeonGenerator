using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class MapGenerator : MonoBehaviour
{

    public int Size_X = 100;
    public int Size_Z = 50;
    public float TileSize = 1.0f;

    public Texture2D TerrainTiles;
    public int TileReselution;


    void Start()
    {
        GenerateMesh();
    }

    Color[][] ChopUpTiles()
    {
        int NumTilesPerRow = TerrainTiles.width / TileReselution;
        int NumRows = TerrainTiles.height / TileReselution;

        Color[][] Tiles = new Color[NumTilesPerRow * NumRows][];

        for (int y = 0; y < NumRows; y++)
        {
            for (int x = 0; x < NumTilesPerRow; x++)
            {
                Tiles[y * NumTilesPerRow + x] = TerrainTiles.GetPixels(x * TileReselution, y * TileReselution, TileReselution, TileReselution);
            }
        }

        return Tiles;
    }


    //Applay the Texture to the Generate Map
    void BuildTexture()
    {
        int TextWidth = Size_X * TileReselution;
        int TextHeigt = Size_Z * TileReselution;
        Texture2D texture = new Texture2D(TextWidth, TextHeigt);

        Color[][] Tiles = ChopUpTiles();

        for (int y = 0; y < Size_Z; y++)
        {
            for (int x = 0; x < Size_X; x++)
            {
                Color[] P = Tiles[Random.Range(0, 4)];
                texture.SetPixels(x * TileReselution, y * TileReselution, TileReselution, TileReselution, P);
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer Mesh_Renderer = GetComponent<MeshRenderer>();
        Mesh_Renderer.sharedMaterials[0].mainTexture = texture;

    }

    //Generate a Mesh
    public void GenerateMesh()
    {
        TDMap Map = new TDMap(Size_X, Size_Z);

        int NumTiles = Size_X * Size_Z;
        int NumTris = NumTiles * 2;

        int VSize_X = Size_X + 1;
        int VSize_Z = Size_Z + 1;
        int NumVerts = VSize_X * VSize_Z;

        //Generate Mesh Data
        Vector3[] Vertices = new Vector3[NumVerts];
        Vector3[] Normals = new Vector3[NumVerts];
        Vector2[] UV = new Vector2[NumVerts];

        int[] Triangels = new int[NumTris * 3];

        int x, z;

        for (x = 0; x < VSize_X; x++)
        {
            for (z = 0; z < VSize_Z; z++)
            {
                Vertices[z * VSize_X + x] = new Vector3(x * TileSize, 0, z * TileSize);
                Normals[z * VSize_X + x] = Vector3.up;
                UV[z * VSize_X + x] = new Vector2((float)x / Size_X, (float)z / VSize_Z);
            }
        }


        for (x = 0; x < Size_X; x++)
        {
            for (z = 0; z < Size_Z; z++)
            {
                int SquareIndex = z * Size_X + x;
                int TriOffset = SquareIndex * 6;
                Triangels[TriOffset + 2] = z * VSize_X + x + 0;
                Triangels[TriOffset + 1] = z * VSize_X + x + VSize_X + 1;
                Triangels[TriOffset + 0] = z * VSize_X + x + VSize_X + 0;

                Triangels[TriOffset + 5] = z * VSize_X + x + 0;
                Triangels[TriOffset + 4] = z * VSize_X + x + 1;
                Triangels[TriOffset + 3] = z * VSize_X + x + VSize_X + 1;
            }
        }


        // Create a new Mesh and populate with data
        Mesh mesh = new Mesh();
        mesh.vertices = Vertices;
        mesh.triangles = Triangels;
        mesh.normals = Normals;
        mesh.uv = UV;

        // Assign our Mesh to our Filter/Renederer/Collider
        MeshFilter Mesh_Filter = GetComponent<MeshFilter>();
        MeshCollider Mesh_Collider = GetComponent<MeshCollider>();
        MeshRenderer Mesh_Renderer = GetComponent<MeshRenderer>();

        Mesh_Filter.mesh = mesh;
        BuildTexture();

    }
}