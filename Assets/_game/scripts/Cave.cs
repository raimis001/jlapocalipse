using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum RoomType
{
	SERVICE = 0,
	LIVING = 1,
	ENERGY = 2,
	OXIGEN = 3,
}

public struct P
{
	public float value;
	public float Value(int level)
	{
		return value * (float)level;
	}

	public P(float value)
	{
		this.value = value;
	}

	public static implicit operator P(float v)
	{
		return new P(v);
	}
}

public struct PropertySetup
{
	public float Energy;
	public float Oxigen;
	public float Temperature;
	public float Water;
	public float Meal;
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
	public float Energy { get { return level * setup.Energy; } }
	public float Oxigen { get { return level * setup.Oxigen; } }
}

public class RoomProperty
{
	public RoomType RoomType;
	public float Live;
	public int Level = 1;

	public PropertySetup Storage = new PropertySetup()
	{
		Energy = 1f,
		Oxigen = 1f,
		Temperature = 1f,
		Water = 0,
		Meal = 0
	};

	public Property Use
	{
		get {
			Property p = new Property()
			{
				setup = Setup.Use,
				level = Level
			};
			return p;
		}
	}

	public Property Gen {
		get {
			Property p = new Property()
			{
				setup = Setup.Gen,
				level = Level
			};
			return p;
		}
	}
	public RoomSetup Setup {
		get {
			return Cave.RoomSetup[RoomType];
		}
	}
}


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
		return new Vector3(pos.x * 10, pos.y * 8, 0);
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
					Energy = 1f,
					Oxigen = -1,
					Temperature = -2,
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
					Energy = -1,
					Oxigen = -1,
					Temperature = -2,
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
		{RoomType.OXIGEN, new RoomSetup()
			{
				Type = RoomType.OXIGEN,
				Name = "Gaisa filtrs",
				Description = "Filtrs attīra gaisu un padara to derīgu elpošanai.",
				Use = {
					Energy = -1,
					Oxigen = -1,
					Temperature = -2,
					Water = 0,
					Meal = 0
				},
				Gen = {
					Energy = 0,
					Oxigen = 5,
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
					Energy = -1,
					Oxigen = -1,
					Temperature = -2,
					Water = 0,
					Meal = 0
				},
				Gen =  {
					Energy = 0,
					Oxigen = 0,
					Temperature = 0,
					Water = 0,
					Meal = 0
				}
			}
		}
	};
	#endregion

	public static float Energy;
	public static float Oxigen;
	public static float Temperature;
	public static float Water;
	public static float Meal;

	public static void Update()
	{
		float oxigen = 0;
		foreach (RoomProperty p in GameLogic.Properties())
		{
			//RoomProperty p = room.Position.Property;
			p.Storage.Oxigen += p.Use.Oxigen * Time.deltaTime * 0.01f;
			p.Storage.Oxigen = Mathf.Clamp(p.Storage.Oxigen, 0, 1);

			oxigen += p.Gen.Oxigen * Time.deltaTime;
		}
		foreach (RoomProperty p in GameLogic.Properties())
		{
			if (oxigen <= 0) break;
			oxigen -= 0.02f;
			p.Storage.Oxigen += Time.deltaTime * 0.02f;
			p.Storage.Oxigen = Mathf.Clamp(p.Storage.Oxigen, 0, 1);

		}
	}
}
