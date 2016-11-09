using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(WaveFunctionCollapse))]
public class WaveEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		if (GUILayout.Button ("Build")) {
			(target as WaveFunctionCollapse).Build ();
		
		}

		DrawDefaultInspector ();


	}

}
