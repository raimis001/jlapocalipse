using UnityEngine;
using System.Collections;

public class WaypointPath : MonoBehaviour
{
	public Color Color = Color.black;

	[HideInInspector]
	public Waypoint[] WaypointList;

	public int WaypointsCount {
		get {
			if (WaypointList == null)
			{
				return 0;
			}
			return WaypointList.Length;
		}
	}
	// Use this for initialization
	void Start()
	{
		UpdateWaypoints();
  }

	// Update is called once per frame
	void Update()
	{

	}

	void UpdateWaypoints()
	{
		WaypointList = GetComponentsInChildren<Waypoint>();

		for (var i = 0; i < WaypointList.Length; i++)
		{
			WaypointList[i].Index = i;
		}
	}

	public void CreateWaypoint()
	{
		GameObject waypoint = new GameObject("Waypoint");
		waypoint.transform.SetParent(transform);
		waypoint.AddComponent<Waypoint>();

		if (WaypointList.Length == 0)
		{
			waypoint.transform.localPosition = Vector3.zero;
		}
		else
		{
			waypoint.transform.localPosition = WaypointList[WaypointList.Length - 1].transform.localPosition;
			waypoint.GetComponent<Waypoint>().SpeedModifier = WaypointList[WaypointList.Length - 1].SpeedModifier;
		}
		waypoint.GetComponent<Waypoint>().Color = Color;
		UpdateWaypoints();
	}
	public Waypoint GetFirst()
	{
		if (WaypointList.Length < 1 )
		{
			return null;
		}

		return WaypointList[0];
	}
	public Waypoint GetLast()
	{
		if (WaypointList.Length < 1)
		{
			return null;
		}

		return WaypointList[WaypointList.Length - 1];
	}
	public Waypoint GetNext(Waypoint waypoint, int direction = 1)
	{
		if (direction == 1 && waypoint.Index >= WaypointList.Length - 1)
		{
			return null;
		}
		if (direction == -1 && waypoint.Index == 0)
		{
			return null;
		}

		return WaypointList[waypoint.Index + direction];
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color;

		WaypointList = GetComponentsInChildren<Waypoint>();
		if (WaypointList.Length > 1)
		{
			for (var i = 0; i < (WaypointList.Length - 1); i++)
			{
				WaypointList[i].Color = Color;
				Gizmos.DrawLine(WaypointList[i].gameObject.transform.position, WaypointList[i + 1].gameObject.transform.position);
			}
		}
	}
}
