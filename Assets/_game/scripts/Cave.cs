using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum RoomType
{
	NONE,
	SERVICE,
	LIVING,
	ENERGY,
	OXIGEN,
	MEAL,
	ELEVATOR,
}

public struct P
{
	private float value;

	private P(float val)
	{
		this.value = val;// Mathf.Clamp(val,0f,1f);
	}

	public void Clamp(float val)
	{
		this.value = Mathf.Clamp(val,0f,1f);
	}
	public void ClampA(float val)
	{
		this.value = Mathf.Clamp(this.value + val, 0f, 1f);
	}

	public float Level(int level)
	{
		return this.value*(float) level;
	}

	public override string ToString()
	{
		return value.ToString("0.00");
	}

	public static implicit operator P(float v)
	{
		return new P(v);
	}
	public static implicit operator float(P record)
	{
		return record.value;
	}

}

public struct PropertySetup
{
	public P Energy;
	public P Oxigen;
	public P Temperature;
	public P Water;
	public P Meal;
}

public struct RoomSetup
{
	public RoomType Type;
	public string Name;
	public string Description;

	public int Price;

	public PropertySetup Use;
	public PropertySetup Gen;

}

public struct Property
{
	public int level;
	public PropertySetup setup;

	public float Energy { get { return setup.Energy.Level(level); } }
	public float Oxigen { get { return setup.Oxigen.Level(level); } }
	public float Temperature { get { return setup.Temperature.Level(level); } }
	public float Water { get { return setup.Water.Level(level); } }
	public float Meal { get { return setup.Meal.Level(level); } }
}

public class RoomProperty
{
	public RoomType RoomType;
	public float Live;
	public int Level = 1;
	public float Damage = 0;
	private Property _use;
	private Property _gen;

	public PropertySetup Storage = new PropertySetup()
	{
		Energy = 0f,
		Oxigen = 1f,
		Temperature = 1f,
		Water = 0,
		Meal = 0,
	};

	public Property Use
	{
		get
		{
			_use.setup = Setup.Use;
			_use.level = Level;
			return _use;
		}
	}

	public Property Gen {
		get {
			_gen.setup = Setup.Gen;
			_gen.level = Level;
			return _gen;
		}
	}
	public RoomSetup Setup {
		get {
			return Cave.RoomSetup[RoomType];
		}
	}
}

[Serializable]
public class RoomPosition
{
	public int x;
	public int y;

	public RoomProperty Property;

	public RoomPosition Left
	{
		get { return new RoomPosition() { x = x - 1, y = y }; }
	}
	public RoomPosition Right
	{
		get { return new RoomPosition() { x = x + 1, y = y }; }
	}

	public Room LeftRoom 
	{
		get { return GameLogic.Rooms[Left.hash]; }
	}
	public Room RightRoom {
		get { return GameLogic.Rooms[Right.hash]; }
	}

	public bool HasLeft()
	{
		return GameLogic.Rooms.ContainsKey(Left.hash);
	}
	public bool HasRight()
	{
		return GameLogic.Rooms.ContainsKey(Right.hash);
	}

	public string hash
	{
		get { return Hash(x, y); }
	}

	public override string ToString()
	{
		return Hash(x, y);
	}

	#region STATIC
	public static Vector3 Vector3(RoomPosition pos)
	{
		return new Vector3(pos.x * 10, pos.y * -8, 0);
	}

	public static string Hash(int x, int y)
	{
		return string.Format("{0}:{1}", x, y);
	}
	public static string Hash(RoomPosition pos)
	{
		return Hash(pos.x, pos.y);
	}
	#endregion
}

public static class  Cave
{
	#region Room Setup
	public static Dictionary<RoomType, RoomSetup> RoomSetup = new Dictionary<RoomType, global::RoomSetup>()
	{
		{RoomType.SERVICE, new RoomSetup()
			{
				Type = RoomType.SERVICE,
				Name = "Telpa",
				Description = "Šo isatbu var pārbūvēt par kādu nepieciešamu istabu",
				Use =  {					
					Energy = 1,
					Oxigen = 1,
					Temperature = 2,
					Water = 0,
					Meal = 0
				},
				Gen = {
					Energy = 0,
					Oxigen = 0,
					Temperature = 0,
					Water = 0,
					Meal = 0
				}
			}
		},
		{RoomType.ENERGY, new RoomSetup()
			{
				Type = RoomType.ENERGY,
				Name = "Ģenerātors",
				Description = "Ģenerators ražo elektrību, kas nepieciešama citām iekārtām",
				Use = {
					Energy = 1,
					Oxigen = 5,
					Temperature = 2,
					Water = 0,
					Meal = 0
				},
				Gen = {
					Energy = 10,
					Oxigen = 0,
					Temperature = 0,
					Water = 0,
					Meal = 0
				}
			}
		},
		{RoomType.OXIGEN, new RoomSetup()
			{
				Type = RoomType.OXIGEN,
				Name = "Gaisa filtrs",
				Description = "Filtrs attīra gaisu un padara to derīgu elpošanai.",
				Use = {
					Energy = 5,
					Oxigen = 1,
					Temperature = 2,
					Water = 0,
					Meal = 0
				},
				Gen = {
					Energy = 0,
					Oxigen = 10,
					Temperature = 0,
					Water = 0,
					Meal = 0
				}
			}
		},
		{RoomType.LIVING, new RoomSetup()
			{
				Type = RoomType.LIVING,
				Name = "Dzīvojamā istaba",
				Description = "Te var atpūsties un sakrāt spēkus nākamajai dienai.",
				Use = {
					Energy = 3,
					Oxigen = 3,
					Temperature = 2,
					Water = 0,
					Meal = 0,
				},
				Gen =  {
					Energy = 0,
					Oxigen = 0,
					Temperature = 0,
					Water = 0,
					Meal = 0
				}
			}
		},
		{RoomType.MEAL, new RoomSetup()
			{
				Type = RoomType.MEAL,
				Name = "Ēdiens",
				Description = "Ēdiens ir svarīga lieta enerģijas iegūšanai.",
				Use = {
					Energy = 4,
					Oxigen = 2,
					Temperature = 2,
					Water = 0,
					Meal = 0,
				},
				Gen =  {
					Energy = 0,
					Oxigen = 0,
					Temperature = 0,
					Water = 0,
					Meal = 10
				}
			}
		},
	};
	#endregion

	public static PropertySetup Storage = new PropertySetup()
	{
		Energy = 0,
		Oxigen = 1,
		Temperature = 1,
		Water = 0,
		Meal = 0
	};

	public static void Update()
	{

		float oxigenUse = 0;
		float oxigenGen = 0;

		float energyUse = 0;
		float energyGen = 0;

		foreach (RoomProperty p in GameLogic.Properties())
		{
			Property use = p.Use;
			Property gen = p.Gen;

			oxigenUse += use.Oxigen;
			oxigenGen += gen.Oxigen;

			energyUse += use.Energy;
			energyGen += gen.Energy;
	
		}

		Storage.Energy.Clamp(energyUse > 0 ? energyGen / energyUse : 1f);
		Storage.Oxigen.ClampA((oxigenGen * Storage.Energy - oxigenUse) * Time.deltaTime * 0.01f);

		//Debug.Log("gen:" + energyGen + " use:" + energyUse + " energy:" + Storage.Energy);
	}
}
