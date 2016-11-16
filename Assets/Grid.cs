using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
[Serializable]
public class Grid : MonoBehaviour
{


	public Vector2 gridSize;


	[HideInInspector]
	public static GameObject root;


	public static Grid i;
	public GameObject module;

	[SerializeField]
	public Module2DArray content;

	private Vector3 moduleSize;

	//private Dictionary<float,GameObject> angleToSelection;

	void Awake ()
	{
		i = this;
		//	Debug.Log ("Awake");
	}

	void Start ()
	{
	}

	// Use this for initialization
	public void Generate ()
	{

		Grid.root = gameObject;
		if (content != null) {
			Clear ();
		}
		content = new Module2DArray ((int)gridSize.x * (int)gridSize.y, (int)gridSize.x);
		Debug.Log (gridSize);
		Grid.root.name = "GridRoot";


		moduleSize = new Vector3 (1, 1, 1);
	
		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				GameObject go = Instantiate (module, 
					                new Vector3 (transform.position.x + (x + 0.5f - gridSize.x / 2f) * moduleSize.x, 0, transform.position.z + (y + 0.5f - gridSize.y / 2f) * moduleSize.y), 
					                Quaternion.identity) as GameObject;

				go.name = "module" + (x) + " " + (y);
				go.GetComponent<Module> ().gridPosition.x = x;
				go.GetComponent<Module> ().gridPosition.y = y;
				content.set (x, y, go.GetComponent<Module> ());
				go.transform.parent = Grid.root.transform;
				go.layer = 10;
			}
		}
	}

	public void Clear ()
	{
		if (content == null) {

			var children = new List<GameObject> ();
			foreach (Transform child in transform)
				children.Add (child.gameObject);
			children.ForEach (child => DestroyImmediate (child));

		} else {
			for (int i = 0; i < content.Length; i++) {

				if (content [i] == null)
					continue;


				var el = content [i];

				DestroyImmediate (el.gameObject);
			}
			content = null;
		}

	}
}
