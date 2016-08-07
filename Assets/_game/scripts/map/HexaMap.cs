using System;
using System.Collections.Generic;
using UnityEngine;

public enum TileKind
{
	Blank,
	Village,
	Church,
	School,
}

[Serializable]
public struct TilePos
{
	const float width = 7f;
	const float height = 3.5f;

	public int x;
	public int y;


	public Vector3 Position
	{
		get
		{
			return new Vector3(x * width + (y % 2 == 0 ? 0 : width / 2f),0, y * Mathf.Sqrt(3f) * height);
		}
	}

	public string HashString
	{
		get { return Hash(x, y); }
	}

	public static string Hash(int x, int y)
	{
		return x + ":" + y;
	}

	public override string ToString()
	{
		return HashString;
	}
}

public class TileClass
{
	public TilePos Position;
	public TileKind Type;

	public HexaTile Reference;

	public float Exploring;

	public TileClass(int x, int y)
	{
		Position.x = x;
		Position.y = y;
	}

	public override string ToString()
	{
		return "Tile @" + Position.HashString;
	}
}

public class HexaMap : MonoBehaviour
{

	public LineDraw Line;
	public Collider Terrain;
	public Camera Camera;

	public Transform Selection;

	private HexaTile _selectedTile;
	internal HexaTile SelectedTile {
		set {
			_selectedTile = value;
			Selection.gameObject.SetActive(true);
			Selection.position = _selectedTile.transform.position;
		}
	}

	public static HexaTile LastTile;

	public static HexaMap Instance;

	void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		
	}


	void Update()
	{
		if (!Line.draw) return;

		RaycastHit hit;
		Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

		if (Terrain.Raycast(ray, out hit, Mathf.Infinity))
		{
			Line.Target = hit.point;
		}

	}

}