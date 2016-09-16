using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MapGeneratorScript))]
public class MouseScript : MonoBehaviour
{
    MapGeneratorScript _Map;

    Vector3 CurrentTileCoord;

    public Transform SelectionCube;

	void Start ()
    {
        _Map = GetComponent<MapGeneratorScript>();
	}
	
	

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

      /*  if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
        {

        }*/
	}
}
