using UnityEngine;
using Engine.Pathfinder;
using System.Collections.Generic;

public class CaveNode : GridNode
{
	public Object Position;

	public CaveNode(int indexX, int indexY, int idValue, bool walk, GridNode parent, GameObject reference) : base(indexX, indexY, idValue, walk, parent, reference)
	{
	}
}

public class CavePathfinder
{

	public IntVector TopLeft;
	public IntVector BottomRight;

	public CaveNode[,] Map;

	private int GenId = 0;

	public int width;
	public int height;

	private Room GetRoom(int x, int y) 
	{
		int rx = x + TopLeft.x;
		int ry = y / 2;

		foreach (Room room in GameLogic.Rooms) 
		{
			if (room.Position.x == rx && room.Position.y == ry) {
				//Debug.Log("check at:" + rx + ":" + ry + " true");
				return room;
			}
		}
		//Debug.Log("check at:" + rx + ":" + ry + " false");
		return null;
	}

	public CaveNode GetNode(int x, int y) 
	{
		return Map[x - TopLeft.x, y * 2];
	}

	public void Prepare() 
	{
		GenId = 0;

		TopLeft = new IntVector(int.MaxValue, int.MaxValue);
		BottomRight = new IntVector(-int.MaxValue, -int.MaxValue);

		foreach (Room room in GameLogic.Rooms)
		{
			if (room.Position.x < TopLeft.x) TopLeft.x = room.Position.x;
			if (room.Position.y < TopLeft.y) TopLeft.y = room.Position.y;

			if (room.Position.x > BottomRight.x) BottomRight.x = room.Position.x;
			if (room.Position.y > BottomRight.y) BottomRight.y = room.Position.y;
		}

		width = BottomRight.x - TopLeft.x + 1;
		height = (BottomRight.y - TopLeft.y) * 2 + 2;

		Map = new CaveNode[width, height];

		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
			{
				Map[x, y] = new CaveNode(x, y, GenId++, false, null, null);
				Room room;

				if (y % 2 != 0)
				{
					room = GetRoom(x, y - 1);
					Map[x, y].walkable = room && room.RoomType == RoomType.ELEVATOR && GetRoom(x, y + 1);
					continue;
				}

				room = GetRoom(x, y);
				Map[x, y].walkable = room != null;
				Map[x, y].reference = room ? room.gameObject : null;
				//Debug.Log(Map[x, y]);

			}

	}

	public List<GridNode> FindPath(RoomPosition start, RoomPosition end)
	{
		int startX = start.x - TopLeft.x;
		int startY = start.y * 2;

		int endX = end.x - TopLeft.x;
		int endY = end.y * 2;

		PathfinderClass pathfinder = new PathfinderClass(width, height);

		pathfinder.Map = Map;
		return pathfinder.FindPath(startX, startY, endX, endY, false);

	}
}
