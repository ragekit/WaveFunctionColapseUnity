using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


[ExecuteInEditMode]
[System.Serializable]
public class Grid : MonoBehaviour
{


	public Vector2 gridSize;


	[HideInInspector]
	public static GameObject root;


	public static Grid i;
	public GameObject module;

	[SerializeField]
	public Module[] content;

	private Vector3 moduleSize;

	private Dictionary<float,GameObject> angleToSelection;

	void Awake ()
	{
		i = this;
		Debug.Log ("Awake");
	}

	void Start ()
	{
	}

	// Use this for initialization
	public void Generate ()
	{

		Grid.root = gameObject;
		if (content != null && content.Length > 0) {
			Clear ();
		}
		
		content = new Module[(int)gridSize.x * (int)gridSize.y];
		Debug.Log (gridSize);
		Grid.root.name = "GridRoot";
		//clear ();

		moduleSize = new Vector3 (1, 1, 1);
	
		for (int i = 0; i < gridSize.x * gridSize.y; i++) {

			int x, y;
			x = (int)(i % gridSize.x);
			y = Mathf.FloorToInt (i / gridSize.x);

			GameObject go = Instantiate (module, 
				                new Vector3 (transform.position.x + (x + 0.5f - gridSize.x / 2f) * moduleSize.x, 0, transform.position.z + (y + 0.5f - gridSize.y / 2f) * moduleSize.y), 
				                Quaternion.identity) as GameObject;
			
			go.name = "module" + (x) + " " + (y);
			go.GetComponent<Module> ().gridPosition.x = x;
			go.GetComponent<Module> ().gridPosition.y = y;
			content [i] = go.GetComponent<Module> ();
			go.transform.parent = Grid.root.transform;
			go.layer = 10;

		}

		//content [0] = new GameObject[(int)gridSize.y + 2];
		//content [(int)gridSize.y + 1] = new GameObject[(int)gridSize.y + 2];
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
