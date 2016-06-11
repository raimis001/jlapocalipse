using UnityEngine;

public class Hazard : MonoBehaviour
{
	TextMesh valueText;

	void Start()
	{
		valueText = GetComponentInChildren<TextMesh>();
		valueText.transform.localScale = transform.localScale.Inverse();
	}

	void Update()
	{
		valueText.transform.rotation = Quaternion.Euler(90, 0, 0);
	}
}

public static class Vector3Extensions
{
	public static Vector3 Inverse(this Vector3 v)
	{
		return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
	}
}