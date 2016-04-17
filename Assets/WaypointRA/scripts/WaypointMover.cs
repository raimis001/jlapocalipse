using UnityEngine;
using UnityEngine.Events;

public enum WaypointPathKind
{
	SIMPLE,
	LOOP,
	PINGPONG
}

public class WaypointMover : MonoBehaviour
{

	public WaypointPath Path;
	public WaypointPathKind PathKind;
	public bool LookAt = true;
	
	[Range(0,100)]
	public float Speed = 20;

	public UnityEvent OnWaypointEnd;
	public UnityEvent OnWaypointReach;

	Waypoint _destination;
	int _direction = 1;

	// Use this for initialization
	void Start()
	{
		_destination = Path.GetFirst();
	}

	// Update is called once per frame
	void Update()
	{
		if (_destination)
		{
			if (LookAt) transform.LookAt(_destination.transform.position );
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0, transform.localEulerAngles.z); 
      transform.position = Vector3.MoveTowards(transform.position, _destination.transform.position, Time.deltaTime * Speed * _destination.SpeedModifier);
			if (Vector3.Distance(transform.position,_destination.transform.position) < 0.1f)
			{
				transform.position = _destination.transform.position;
				OnWaypointReach.Invoke();
				_destination = Path.GetNext(_destination, _direction);
				if (!_destination)
				{
					OnWaypointEnd.Invoke();
					switch (PathKind)
					{
						case WaypointPathKind.LOOP:
							_direction = 1;
							_destination = Path.GetFirst();
							break;
						case WaypointPathKind.SIMPLE:
							_direction = 1;
							break;
						case WaypointPathKind.PINGPONG:
							_direction *= -1;
							_destination = _direction == 1 ? Path.GetFirst() : Path.GetLast();
							break;
					}
				}
			}
		}
	}

}
