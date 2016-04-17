using UnityEngine;


public class Waypoint : MonoBehaviour {

	public float SpeedModifier = 1;

	[HideInInspector]
	public int Index;

	[HideInInspector]
	public Color Color = Color.black;

	void  OnDrawGizmos()
	{
		Gizmos.color = Color;
		Gizmos.DrawSphere(transform.position, 1);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateWaypoint()
	{

		if (!transform.parent)
		{
			return;
		}
		WaypointPath path = transform.parent.gameObject.GetComponent<WaypointPath>();
		if (path)
		{
			path.CreateWaypoint();
		}

	}
}

