using UnityEngine;
using System.Collections;

public class WaveFunctionCollapse : MonoBehaviour
{
	public Grid baseGrid;
	public Grid targetGrid;

	public int n;


	public Module[,] patterns;

	public void Build ()
	{
		int gridW = (int)baseGrid.gridSize.x;
		int gridH = (int)baseGrid.gridSize.y;

		patterns = new Module[(gridW - n + 1) * (gridH - n + 1), n * n];

		for (int l = 0, j = 0; j < (gridH - n + 1); j++) {
			for (int i = 0; i < (gridW - n + 1); i++,l++) {
			
				for (int k = 0; k < n * n; k++) {

					patterns [l, k] = baseGrid.content [(int)(j * gridW + i + (Mathf.FloorToInt (k / n) * gridW + k % n))];
					
				}

			}
		}

		Debug.Log (patterns.GetLength (0));
		for (int i = 0; i < patterns.GetLength (0); i++) {

			string ret = "";

			for (int j = 0; j < n * n; j++) {
				
				if (patterns [i, j].content != null) {
					ret += "1";
				} else {
					ret += "0";
				}



			}
			Debug.Log (ret);
		}

	}



}
