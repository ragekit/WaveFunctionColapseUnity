using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor
{

	void OnSceneGUI ()
	{
		HandleUtility.AddDefaultControl (GUIUtility.GetControlID (FocusType.Passive));
		if (Event.current.type == EventType.MouseDown) {
			RaycastHit hit; 
			Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 10))) {

				Module m = hit.transform.gameObject.GetComponent<Module> ();

				if (m) {
					
					if (m.content == null) {
						Debug.Log ("add");
						GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Cube) as GameObject;
						obj.transform.position = m.transform.position;
						obj.transform.parent = hit.transform;
						m.content = obj;
					} else {
						Debug.Log ("remove");
						DestroyImmediate (m.content);
						m.content = null;
					}

				}
			
				//Event.current.Use();

			}
		}

	}

	public override void OnInspectorGUI ()
	{
		if (GUILayout.Button ("Generate")) {
			
			(target as Grid).Generate ();
		}
		if (GUILayout.Button ("Clear")) {

			(target as Grid).Clear ();
		}

		DrawDefaultInspector ();


	}
}
