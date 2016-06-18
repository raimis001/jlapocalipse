using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

	public float horizontalScrollSpeed = 1;
	public float verticalScrollSpeed = 1;
	public float zoomSpeed = 1;

	public float depthMin = -5;

	Vector3 _dragg;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetMouseButtonDown(2))
		{
			_dragg = Input.mousePosition;
			return;
		}

		if (Input.GetMouseButton(2))
		{
			Vector3 delta = (_dragg - Input.mousePosition) * 0.3f;
			MoveMe(delta.x, delta.y, 0);
			_dragg = Input.mousePosition;
			return;
		}

		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
		float zMove = Input.GetAxis("Mouse ScrollWheel");

		if (xAxisValue == 0 && yAxisValue == 0 && zMove == 0)
		{
			return;
		}

		MoveMe(xAxisValue, yAxisValue, zMove);

	}

	private void MoveMe(float x, float y, float z)
	{
		Vector3 _moveVector = new Vector3(x * horizontalScrollSpeed, y * verticalScrollSpeed, z * zoomSpeed) * Time.deltaTime;
		transform.Translate(_moveVector);
		transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -150, depthMin));
	}
}
