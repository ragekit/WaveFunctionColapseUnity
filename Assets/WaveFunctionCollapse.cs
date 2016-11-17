using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Wave
{
	public Module2DArray[] patterns;
	public bool[] coefficient;

	public Wave (Module2DArray[] basePatterns)
	{
		patterns = basePatterns;
		coefficient = new bool[patterns.Length];
		for (int i = 0; i < coefficient.Length; i++) {
			coefficient [i] = true;
		}
	}
}

public class WaveFunctionCollapse : MonoBehaviour
{
	public Grid baseGrid;
	public Grid targetGrid;

	public int n;


	public Module2DArray[] patterns;

	public void Build ()
	{
		int gridW = (int)baseGrid.gridSize.x;
		int gridH = (int)baseGrid.gridSize.y;

		patterns = new Module2DArray[(gridW - n + 1) * (gridH - n + 1)];
		Debug.Log (baseGrid.content);

		for (int i = 0, l = 0; i < (gridW - n + 1); i++) {
			for (int j = 0; j < gridH - n + 1; j++,l++) {
				patterns [l] = baseGrid.content.cut (i, j, i + (n - 1), j + (n - 1));
			}

		}

		int outW = (int)targetGrid.gridSize.x;
		int outH = (int)targetGrid.gridSize.y;

		Wave[] ws = new Wave[outW * outH];

		for (int i = 0; i < outW * outH; i++) {
			ws [i] = new Wave (patterns);
		}


		//STARTING THE ALGO;

//		for (int i = 0; i < patterns.Length; i++) {
//			
//		
//			string ret = "";
//			for (int j = 0; j < patterns [i].Length; j++) {
//				if (patterns [i] [j].content == null) {
//					ret += "0";
//				} else {
//					ret += "1";
//				}
//			}
//
//			Debug.Log (ret);
//		}
//

	}



}
