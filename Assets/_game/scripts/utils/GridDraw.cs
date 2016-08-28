using UnityEngine;
using System.Collections;
using Engine.Pathfinder;
using System.Collections.Generic;

public class GridDraw : MonoBehaviour
{

	public GameObject GridPrefab;

	private Vector3 _boound;

	private GameObject[,] Cells = null;

	private Room startRoom;
	private Room endRoom;

	void Start()
	{
		//Debug.Log(GridPrefab.GetComponent<SpriteRenderer>().bounds.size);
		_boound = GridPrefab.GetComponent<SpriteRenderer>().bounds.size + new Vector3(0.1f, 0.1f);
	}

	public void OnDisable()
	{
		//GameLogic.OnRoomSelect -= OnRoomSelect;
	}

	public void OnEnable()
	{
		//GameLogic.OnRoomSelect += OnRoomSelect;
	}

	void OnRoomSelect(Room room)
	{
		RedrawGrid();


		if (room == null)
		{
			Debug.Log("clear");
			startRoom = null;
			endRoom = null;
			return;
		}

		CavePathfinder path = GameLogic.Pathfinder;

		if (!startRoom) 
		{
			startRoom = room;
		} 
		else 
		{
			endRoom = room;
		}


		if (startRoom && endRoom) 
		{
			List<GridNode> p = path.FindPath(startRoom.Position, endRoom.Position);

			foreach (GridNode g in p) 
			{
				Cells[g.x, g.y].GetComponent<SpriteRenderer>().color = Color.green;
			}

		}

	}

	public void FillPath(List<GridNode> path) 
	{
		foreach (GridNode g in path)
		{
			Cells[g.x, g.y].GetComponent<SpriteRenderer>().color = Color.green;
			break;
		}

	}
	void RedrawGrid() 
	{
		CavePathfinder path = GameLogic.Pathfinder;

		for (int x = 0; x < path.width; x++)
		{
			for (int y = path.height - 1; y >= 0; y--)
			{
				if (path.Map[x, y] == null) continue;
				Cells[x, y].GetComponent<SpriteRenderer>().color = path.Map[x, y].walkable ? Color.white : Color.gray;
			}
		}
	}

	public void DrawGrid()
	{
		if (Cells != null)
		{
			foreach (GameObject obj in Cells)
			{
				Destroy(obj);
			}
		}

		CavePathfinder path = GameLogic.Pathfinder;

		Cells = new GameObject[path.width, path.height];

		for (int x = 0; x < path.width; x++)
		{
			for (int y = path.height - 1; y >= 0; y--)
			{
				if (path.Map[x, y] == null) continue;

				Cells[x, y] = Instantiate(GridPrefab);
				Cells[x, y].transform.SetParent(transform);
				Cells[x, y].transform.localPosition = new Vector3(x * _boound.x, path.height * _boound.y - y * _boound.y);

				Cells[x, y].GetComponent<SpriteRenderer>().color = path.Map[x, y].walkable ? Color.white : Color.gray;
			}
		}
	}

}
