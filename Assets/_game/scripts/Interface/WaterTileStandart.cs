using UnityEngine;
using System.Collections;

public class WaterTileStandart : MonoBehaviour
{

	public Renderer WaterMaterial;

	public Vector2 Speed;
	public Vector2 Tile;

	// Update is called once per frame
	void Update()
	{
		if (WaterMaterial)
		{

			float offsetX = Mathf.PingPong(Time.time * Speed.x, Tile.x);
			float offsetY = Mathf.PingPong(Time.time * Speed.y, Tile.y);

			WaterMaterial.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
		}

	}
}
