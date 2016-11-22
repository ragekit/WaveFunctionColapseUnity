using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Model : MonoBehaviour
{

	public Grid baseGrid;


	public static int N;
	public static Fake2DArray<int> sample;
	public static int SMX, SMY;
	public static int numberOfSamples;

	int numberOfPatterns;

	public Dictionary<float,int> weights;
	public Pattern[] patterns;

	public void Build ()
	{

		SMX = (int)baseGrid.gridSize.x;
		SMY = (int)baseGrid.gridSize.y;

		//size of pattern lookup
		N = 2;
		sample = new Fake2DArray<int> (SMX * SMY, SMX);

		for (int y = 0; y < SMY; y++)
			for (int x = 0; x < SMX; x++) {
				int moduleType = baseGrid.content.get (y, x).GetComponent<Module> ().content == null ? 0 : 1;
				sample.set (x, y, moduleType);
			}
		

		//this is C
		numberOfSamples = 2;
		//this is W
		float patternBiteSize = Mathf.Pow (numberOfSamples, N * N);

		weights = new Dictionary<float, int> ();

		//building pattern weight & pattern list


		Dictionary<int, Pattern> patternUnFiltered = new Dictionary<int, Pattern> ();

		for (int y = 0; y < SMY - N + 1; y++)
			for (int x = 0; x < SMX - N + 1; x++) {


				Pattern p = Pattern.FromSample (x, y);
				patternUnFiltered [p.index] = p;

				//TODO SYMMETRY

				int index = p.index;
				if (weights.ContainsKey (index)) {
					weights [index]++;
				} else {
					weights.Add (index, 1);
				}
			}
		//this is T;
		numberOfPatterns = patternUnFiltered.Count;
		patterns = new Pattern[numberOfPatterns];


		//kinda toArray
		int counter = 0;
		foreach (int index in patternUnFiltered.Keys) {
			patterns [counter] = patternUnFiltered [index];
			patterns [counter].weight = weights [index];
			counter++;
		}

		DebugPatterns ();
	}


	public void DebugPatterns ()
	{
		for (int i = 0; i < patterns.Length; i++) {
			Pattern p = patterns [i];
			var ret = "pattern : ";

			for (int j = 0; j < p.Length; j++) {
				ret += p [j];

			}
			ret += " weight : " + p.weight;
			Debug.Log (ret);
		}
	}
}
