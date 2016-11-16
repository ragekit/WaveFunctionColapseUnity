using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Module : MonoBehaviour
{

	// Use this for initialization
	public Vector2 gridPosition;
	public GameObject content;

	public bool toggle = true;

	void OnDrawGizmos ()
	{
		if (toggle) {
			Gizmos.color = Color.grey;

		} else {
			Gizmos.color = Color.red;
		}

		Gizmos.DrawWireCube (transform.position, transform.lossyScale);
	}

}
