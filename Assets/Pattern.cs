using System;

public class Pattern : Fake2DArray<int>
{
	public int index;
	int sampleX;
	int sampleY;

	public int weight;

	public Pattern (int n) : base (n * n, n)
	{
		
	}

	void setIndex ()
	{
		int numberofSamples = Model.numberOfSamples;
		int result = 0, power = 1;
		for (int i = 0; i < Length; i++) {
			result += this [Length - 1 - i] * power;
			power *= numberofSamples;
		}
		this.index = result;
	}

	public static Pattern FromSample (int x, int y)
	{
		Pattern result = new Pattern (Model.N);
		for (int i = 0; i < Model.N; i++)
			for (int j = 0; j < Model.N; j++) {
				result [j + i * Model.N] = Model.sample.get (x + j, y + i);
			}

		result.setIndex ();
		return result;
	}

}


