using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Helper
{
	public static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T) A.GetValue(UnityEngine.Random.Range(0, A.Length));
		return V;
	}

	public static T FindClosestTarget<T>(Vector3 position, float distance = Mathf.Infinity) where T : MonoBehaviour
	{
		T[] objs = GameObject.FindObjectsOfType<T>();
		T closest = null;
		float dist = Mathf.Infinity;

		foreach (var obj in objs)
		{
			float delta = Vector3.Distance(position, obj.transform.position);

			if (delta <= distance && delta < dist)
			{
				closest = obj;
				dist = delta;
			}
		}

		return closest;
	}

	public static float AngleInRad(Vector3 vec1, Vector3 vec2)
	{
		return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z);
	}

	public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
	{
		return AngleInRad(vec1, vec2) * 180f / Mathf.PI;
	}

}

public class WeightClass<T>
{
	public float Weight { get; set; }
	public T Value { get; set; }
}

public static class Weighted
{
	public static float TotalWeight = 0;

	public static WeightClass<T> Create<T>(float weight, T value)
	{
		TotalWeight += weight;
		return new WeightClass<T> { Weight = weight, Value = value };
	}

	//private static readonly System.Random random = new System.Random();

	public static T GetWeighted<T>(this IEnumerable<WeightClass<T>> collection)
	{
		//var rnd = random.NextDouble();
		//Debug.Log(rnd);

		float total = 0;

		float random = UnityEngine.Random.Range(0, TotalWeight + 0.00f);
		foreach (var item in collection)
		{
			total += item.Weight;
			if (total >= random)
			{
				return item.Value;
			}

			/*
				if (rnd < item.Weight)
				{
					Debug.Log("weight:" + item.Weight + " value:" + item.Value);
					return item.Value;
				}
				rnd -= item.Weight;
			*/
		}
		throw new InvalidOperationException("The proportions in the collection do not add up to 1.");
	}
}

