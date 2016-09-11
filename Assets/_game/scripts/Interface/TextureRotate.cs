using UnityEngine;
using System.Collections;

public class TextureRotate : MonoBehaviour
{

	public bool Working = false;

	public Renderer Material;

	public Vector2 Speed;

	// Update is called once per frame
	void Update()
	{
		if (!Working) return;

		if (Material)
		{

			float offsetX = Time.time*Speed.x;
			float offsetY = Time.time*Speed.y;

			Material.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
			Material.material.SetTextureOffset("_BumpMap", new Vector2(offsetX, offsetY));
		}
	}
}
