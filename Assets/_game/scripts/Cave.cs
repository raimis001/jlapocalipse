using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RoomType
{
	SERVICE,
	LIVING
}

public class RoomPosition
{
	public static Vector3 Vector3(RoomPosition pos)
	{
		return new Vector3(pos.x * 10, pos.y * 8, 0);
	}

	public static string Hash(int x, int y)
	{
		return string.Format("{0}:{1}", x, y);
	}

	public int x;
	public int y;

	public override string ToString()
	{
		return Hash(x, y);
	}

}

public class Cave : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
