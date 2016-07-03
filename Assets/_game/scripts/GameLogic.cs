using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{

	private static GameLogic _instance;
	public static GameLogic Instance { get { return _instance; } }

	public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();
	public static List<Build> Places = new List<Build>();

	public static GameObject Device(RoomType type)
	{
		return Instance.DevicesPrefab[(int)type];
	}
	public static void CreateRoom(RoomPosition pos)
	{
		Room room = Room.CreateRoom(pos, RoomType.SERVICE);

		int weight = 0;
		if (pos.HasLeft())
		{
			weight += 1;
			pos.LeftRoom.Weight += 2;
		}
		if (pos.HasRight())
		{
			weight += 2;
			pos.RightRoom.Weight += 1;
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
					Instance.BuildMenu.SetActive(true);
					Instance.RoomMenu.SetActive(false);
					return;
				}
			}

			_selectedRoom = value;
			if (_selectedRoom)
			{
				_selectedRoom.Selected = true;
			}
			Instance.BuildMenu.SetActive(!_selectedRoom);
			Instance.RoomMenu.SetActive(_selectedRoom);
		}
	}

	public static ItemMain DragItem;

	[Header("Prefabs")]
	public GameObject CaveObject;
	public GameObject RoomPrefab;
	public GameObject BuildPrefab;
	public GameObject BulletPrefab;

	public GameObject[] DevicesPrefab;

	public GameObject[] Items;

	[Header("Interface")]
	public GameObject BuildMenu;
	public GameObject RoomMenu;

	[Header("Test")]
	public Text OxigeGen;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		CreateRoom(new RoomPosition());
		BuildMenu.SetActive(true);
		RoomMenu.SetActive(false);
	}

	void Update()
	{
		Cave.Update();
		OxigeGen.text = Cave.Storage.Oxigen.ToString();
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

	public void MakeDevice(int type)
	{
		if (!_selectedRoom) return;

		_selectedRoom.Type = (RoomType)type;

	}

	public static IEnumerable<RoomProperty> Properties()
	{
		foreach (Room room in Rooms.Values)
		{
			yield return room.Position.Property;
		}
	}

	public static ItemMain CreateItem(ItemKind kind)
	{
	
		GameObject obj = Instantiate(Instance.Items[(int) kind]);
		return obj.GetComponent<ItemMain>();
	}
}
