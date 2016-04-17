using UnityEngine;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{

	private static GameLogic _instance;

	public static GameLogic Instance
	{
		get { return _instance; }
	}


	public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
	public static List<Build> Places = new List<Build>();


	public static void CreateRoom(RoomPosition pos)
	{
		Room room = Room.CreateRoom(pos, RoomType.SERVICE);

		int weight = 0;
		if (Rooms.ContainsKey(RoomPosition.Hash(pos.x - 1, pos.y)))
		{
			weight += 1;
			Rooms[RoomPosition.Hash(pos.x - 1, pos.y)].Weight += 2;
		}
		if (Rooms.ContainsKey(RoomPosition.Hash(pos.x + 1, pos.y)))
		{
			weight += 2;
			Rooms[RoomPosition.Hash(pos.x + 1, pos.y)].Weight += 1;
		}

		room.Weight = weight;

		Rooms.Add(room.Position.ToString(), room);
		
		ClearPlaces();
	}

	private static void ClearPlaces()
	{
		foreach (Build build in Places)
		{
			Destroy(build.gameObject);
		}

		Places.Clear();
	}

	private static Room _selectedRoom;
	public static Room SelectedRoom
	{
		get { return _selectedRoom; }
		set
		{
			if (_selectedRoom)
			{
				_selectedRoom.Selected = false;
				if (!value || _selectedRoom.Position.ToString().Equals(value.Position.ToString()))
				{
					_selectedRoom = null;
					return;
				}
			}

			_selectedRoom = value;
			if (_selectedRoom)
			{
				_selectedRoom.Selected = true;
			}
		}
	}

	public GameObject CaveObject;
	public GameObject RoomPrefab;
	public GameObject BuildPrefab;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start()
	{
		CreateRoom(new RoomPosition());
	}

	// Update is called once per frame
	void Update()
	{

	}

	

	public void StartBuild()
	{
		ClearPlaces();

		foreach (Room room in Rooms.Values)
		{
			switch (room.Weight)
			{
				case 0:
					Places.Add(Build.CreateBuild(room.Position.x - 1, room.Position.y));
					Places.Add(Build.CreateBuild(room.Position.x + 1, room.Position.y));
					break;
				case 1:
					Places.Add(Build.CreateBuild(room.Position.x + 1, room.Position.y));
					break;
				case 2:
					Places.Add(Build.CreateBuild(room.Position.x - 1, room.Position.y));
					break;
			}
		}

	}

}
