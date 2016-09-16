using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MapGenerator))]
public class DungeonGeneratorInspector :Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            MapGenerator Generator = (MapGenerator)target;
            Generator.GenerateMesh();
        }
    }
    

	
}
