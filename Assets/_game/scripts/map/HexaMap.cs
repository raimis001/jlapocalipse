using System.Collections.Generic;
using UnityEngine;

public enum TileKind
{
	Blank,
	Village,
	Church,
	School,
}

public struct TilePos
{
	public int x;
	public int y;

	public string HashString
	{
		get { return Hash(x, y); }
	}

	public static string Hash(int x, int y)
	{
		return x + ":" + y;
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
	#region tilesetup
	public static Dictionary<string, TileClass> Tiles = new Dictionary<string, TileClass>
	{
		{ TilePos.Hash(0, 0), new TileClass(0, 0)
			{
				Type = TileKind.Church
			}
		},
		{ TilePos.Hash(1, 0), new TileClass(1, 0)
			{
				Type = TileKind.Church
			}
		},
		{ TilePos.Hash(2, 0), new TileClass(2, 0)
			{
				Type = TileKind.Church
			}
		},
		{ TilePos.Hash(0, 1), new TileClass(0, 1)
			{
				Type = TileKind.Church
			}
		},
		{ TilePos.Hash(1, 1), new TileClass(1, 1)
			{
				Type = TileKind.Church
			}
		}
	};
	#endregion

	#region Unity defs
	public Transform Parent;
	public GameObject[] TilePrefabs;
	#endregion

	private void Start()
	{
		CreateMap();
	}

	private void CreateMap()
	{
		foreach (TileClass tile in Tiles.Values)
		{
			CreateTile(tile, Helper.GetRandomEnum<TileKind>());
		}
	}

	private void CreateTile(TileClass tile, TileKind tileType, string type = "h")
	{

		var obj = Instantiate(TilePrefabs[(int)tileType], HexOffset(tile.Position, type), Quaternion.identity) as GameObject;
		obj.transform.SetParent(transform);
		obj.transform.Translate(0, 0, Parent.position.z);
		obj.transform.localEulerAngles = new Vector3(-90, type.Equals("h") ? 0 : 90);
		obj.transform.localScale = Vector3.one;

		tile.Reference = obj.GetComponent<HexaTile>();
		tile.Reference.Setup = tile;
	}

	private Vector3 HexOffset(TilePos pos, string type = "h")
	{
		return HexOffset(pos.x, pos.y, type);
	}

	private Vector3 HexOffset(int x, int y, string type = "h")
	{
		float _offsetY = Mathf.Sqrt(3f) * 2f;
		float _offsetX = 1.5f * 2f;

		var result = Vector3.zero;

		if (type.Equals("v"))
		{
			result.x = x * _offsetX + (y % 2 == 0 ? 0 : _offsetX / 2f);
			result.z = -y * (_offsetY / 4f);
		}
		else
		{
			result.x = x * _offsetY * 0.5f + (y % 2 == 0 ? 0 : _offsetY / 4f);
			result.z = -y * _offsetX / 2f;
		}

		return result;
	}
}