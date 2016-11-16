using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveFunctionCollapse : MonoBehaviour
{
	public Grid baseGrid;
	public Grid targetGrid;

	public int n;


	public Dictionary<Vector2,Module2DArray> patterns;

	public void Build ()
	{
		int gridW = (int)baseGrid.gridSize.x;
		int gridH = (int)baseGrid.gridSize.y;

		patterns = new Dictionary<Vector2, Module2DArray> ();
		Debug.Log (baseGrid.content);

		for (int i = 0; i < (gridW - n + 1); i++) {
			for (int j = 0; j < gridH - n + 1; j++) {
				for (int k = 0; k < n; k++) {

					for (int l = 0; l < n; l++) {


						Module2DArray lol = baseGrid.content.cut (i, j, i + (n - 1), j + (n - 1));

						patterns [new Vector2 (i, j)] = lol;


					}

				}
			}

		}

		foreach (KeyValuePair<Vector2, Module2DArray> entry in patterns) {
			string ret = "";
			for (int i = 0; i < entry.Value.Length; i++) {
				if (entry.Value [i].content == null) {
					ret += "0";
				} else {
					ret += "1";
				}
			}

			Debug.Log (ret);
		}
	}



}
