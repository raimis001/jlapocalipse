using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour
{

	public GameObject SelectedObject;
	public GameObject[] Walls;

	private int _weight;
	[Range(0, 3)]
	public int Weight;

	public GameObject Lights;

	[Header("Indicators")]
	public ProgressBar DamageProgress;
	public ProgressBar EnergyStorage;
	public ProgressBar OxigenStorage;
	public ProgressBar TemperatureStorage;

	[HideInInspector]
	public RoomDevice Device;

	private RoomType _type;
	public RoomType Type {
		get { return _type; }
		set {
			_type = value;

			Position.Property = new RoomProperty() { RoomType = _type };

			if (Device)
			{
				Destroy(Device.gameObject);
			}
		
			Device = RoomDevice.Create(_type, transform, Position);
			Device.LightsOn = _lightsOn;
		}
	}

	private bool _lightsOn = true;
	public bool LightsOn
	{
		get { return _lightsOn; }
		set
		{
			//if (_lightsOn == value) return;

			_lightsOn = value;
			if (Lights)
			{
				Lights.SetActive(_lightsOn);
			}
			if (Device)
			{
				Device.LightsOn = _lightsOn;
			}

		}
	}

	private bool _selected;
	public bool Selected
	{
		get { return _selected; }
		set
		{
			_selected = value;
			SelectedObject.SetActive(_selected);
		}
	}

	public RoomPosition Position = new RoomPosition();

	public static Room CreateRoom(RoomPosition pos, RoomType type)
	{
		return CreateRoom(pos.x, pos.y, type);
	}
	public static Room CreateRoom(int x, int y, RoomType type)
	{
		GameObject obj = Instantiate(GameLogic.Instance.RoomPrefab);
		obj.transform.SetParent(GameLogic.Instance.CaveObject.transform);

		Room result = obj.GetComponent<Room>();

		result.Position.x = x;
		result.Position.y = y;
		result.Type = type;


		return result;
	}

	// Use this for initialization
	void Start()
	{
		transform.position = RoomPosition.Vector3(Position);
	}

	// Update is called once per frame
	void Update()
	{
		if (Weight != _weight)
		{
			_weight = Weight;
			SetWalls();
		}
		if (Position == null) return;

		RoomProperty p = Position.Property;
		if (p == null) return; 

		if (DamageProgress) DamageProgress.Value = p.Damage;
		if (EnergyStorage) EnergyStorage.Value = Cave.Storage.Energy;
		if (OxigenStorage) OxigenStorage.Value = Cave.Storage.Oxigen;

		LightsOn = p.Use.Energy <= 1 ? Cave.Storage.Energy > 0.9f : Cave.Storage.Energy >= 0.5f;

	}

	void SetWalls()
	{
		//0 - apkārt nav istabu
		//1 - pa kreisi ir istaba
		//2 - pa labi istaba
		//3 - abās pusēs istaba

		foreach (GameObject wall in Walls)
		{
			wall.SetActive(false);
		}

		switch (_weight)
		{
			case 0:
				Walls[0].SetActive(true);
				Walls[2].SetActive(true);
			break;
			case 1:
				Walls[1].SetActive(true);
				Walls[2].SetActive(true);
				break;
			case 2:
				Walls[0].SetActive(true);
				break;
			case 3:
				Walls[1].SetActive(true);
			break;
		}

	}

	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;

		GameLogic.SelectedRoom = this;
	}

}
