﻿using System;
using UnityEngine;

[Serializable]
public class Array2D<T>
{
	[SerializeField]
	private T[] content;

	public int width;
	public int Length;

	public Array2D (int size, int width, int height = -1)
	{
		Length = size;
		content = new T[size];
		this.width = width;
	}

	public T get (int x, int y)
	{
		return content [y * width + x];
	}

	public void set (int x, int y, T value)
	{
		content [y * width + x] = value;
	}

	public Vector2 indexToVector2 (int index)
	{
		return new Vector2 ((int)(index % width), Mathf.FloorToInt (index / width));
	}


	public T this [int i] {
		get { return content [i]; }
		set { content [i] = value; }
	}

}

[Serializable]
public class Array3D<T>
{
	[SerializeField]
	private T[] content;

	public int width;
	public int height;
	public int Length;

	public Array3D (int size, int width, int height, int depth = -1)
	{
		Length = size;
		this.width = width;
		this.height = height;

		content = new T[size];
	}

	public T get (int x, int y, int z)
	{
		return content [x + width * (y + height * z)];
	}

	public void set (int x, int y, int z, T value)
	{
		content [x + width * (y + height * z)] = value;
	}

	public T this [int i] {
		get { return content [i]; }
		set { content [i] = value; }
	}
}

[Serializable]
public class Module2DArray : Array2D<Module>
{
	public Module2DArray (int size, int width, int height = 0) : base (size, width, height)
	{
		
	}

	public Module2DArray cut (int top, int left, int bottom, int right)
	{
		Module2DArray ret = new Module2DArray ((right + 1 - left) * (bottom + 1 - top), (right + 1 - left));

		for (int i = 0; i < ret.Length; i++) {
			ret [i] = get (left + i % ret.width, top + (int)(i / ret.width));
		}

		return ret;
	}


}



