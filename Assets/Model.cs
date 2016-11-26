using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Model : MonoBehaviour
{

	public Grid baseGrid;
	public Grid targetGrid;

	public static int N;
	public static Array2D<int> sample;
	public static int SMX, SMY, FMX, FMY;
	public static int numberOfSamples;

	int numberOfPatterns;

	public Dictionary<float,int> weights;
	public Pattern[] patterns;

	public Array2D<bool[]> wave;
	public System.Random random;
	double logT;

	public void Build ()
	{

		FMX = (int)targetGrid.gridSize.x;
		FMY = (int)targetGrid.gridSize.y;
		SMX = (int)baseGrid.gridSize.x;
		SMY = (int)baseGrid.gridSize.y;

		//size of pattern lookup
		N = 2;
		sample = new Array2D<int> (SMX * SMY, SMX);

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


		//kinda dict.toArray
		int counter = 0;
		foreach (int index in patternUnFiltered.Keys) {
			patterns [counter] = patternUnFiltered [index];
			patterns [counter].weight = weights [index];
			counter++;
		}

		wave = new Array2D<bool[]> (FMX * FMY, FMX, FMY);

		for (int x = 0; x < 2 * N - 1; x++) {
			for (int y = 0; y < 2 * N - 1; y++) {
				for (int patternIndex1 = 0; patternIndex1 < numberOfPatterns; patternIndex1++) {

					List<int> list = new List<int> ();

					for (int patternIndex2 = 0; patternIndex2 < numberOfPatterns; patternIndex2++) {
						if (patternOverlap (patterns [patternIndex1], patterns [patternIndex2], x - N + 1, y - N + 1)) {
							list.Add (patternIndex2);
						}
					}


					patterns [patternIndex1].neighbors.set (x, y, list.ToArray ());
				}
			}
		}
			
		//DebugPatterns ();
	}

	public bool Run (int seed, int limit)
	{
		logT = Math.Log (numberOfPatterns);
		logProb = new Double[numberOfPatterns];
		for (int t = 0; t < numberOfPatterns; t++) {
			logProb [t] = Math.Log (patterns [t].weight);
		}

		return false;
	}

	bool OnBoundary (int x, int y)
	{
		//TODO periodic
		return (x + N > FMX || y + N > FMY);
	}

	Array2D<int> Observed;
	double[] logProb;

	bool? Observe ()
	{
		bool[] currentWave;
		int amount;
		float sum;
		double min = 1E+3;
		int argminx = -1, argminy = -1;
		double noise, mainSum, logSum;
		double entropy;

		for (int x = 0; x < FMX; x++) {
			for (int y = 0; y < FMY; y++) {



				if (OnBoundary (x, y))
					continue;
					
				currentWave = wave.get (x, y);
				amount = 0;
				sum = 0;
				for (int t = 0; t < numberOfPatterns; t++) {
					if (currentWave [t]) {
						amount += 1;
						sum += patterns [t].weight;
					}
				}

				if (sum == 0)
					return false;

				noise = 1E-6 * random.NextDouble ();


				//entropy calculation
				if (amount == 1) {
					entropy = 0;
				} else if (amount == numberOfPatterns) {
					entropy = logT;
				} else {
					mainSum = 0;
					logSum = Math.Log (sum);
					for (int t = 0; t < numberOfPatterns; t++) {
						if (currentWave [t]) {
							mainSum += patterns [t].weight * logProb [t];
						}
					}

					entropy = logSum - mainSum / sum;
				}

				if (entropy > 0 && entropy + noise < min) {
					min = entropy + noise;
					argminx = x;
					argminy = y;
				}
			}

			if (argminx == -1 && argminy == -1) {
				
			}

		}


		return false;
	}

	bool patternOverlap (Pattern first, Pattern second, int dx, int dy)
	{
		//reduce the test square to overlap
		int xmin = dx < 0 ? 0 : dx;
		int xmax = dx < 0 ? dx + N : N;
		int ymin = dy < 0 ? 0 : dy;
		int ymax = dy < 0 ? dy + N : N;

		for (int y = ymin; y < ymax; y++) {
			for (int x = xmin; x < xmax; x++) {

				//comparision of  overlap
				if (first.get (x, y) != second.get (x - dx, y - dy)) {
					return false;
				}
			}
		}

		return true;
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

			ret += " neigh : ";

			for (int k = 0; k < p.neighbors.Length; k++) {
				ret += " i : " + k + " //";
				for (int l = 0; l < p.neighbors [k].Length; l++) {
					ret += p.neighbors [k] [l] + " ";
				}

			}

			Debug.Log (ret);
		}
	}
}
