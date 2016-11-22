using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Model))]
public class ModelEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		if (GUILayout.Button ("Build")) {
			(target as Model).Build ();

		}

		DrawDefaultInspector ();


	}

}
