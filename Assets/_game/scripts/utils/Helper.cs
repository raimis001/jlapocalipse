using UnityEngine;
using System.Collections;

public static class Helper 
{
	public static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
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

}
