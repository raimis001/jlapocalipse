using UnityEngine;
using System.Collections;

public class CameraManagerMap : MonoBehaviour
{

	public float horizontalScrollSpeed = 1;
	public float verticalScrollSpeed = 1;
	public float zoomSpeed = 1;

	public float depthMin = -5;
	public float depthMax = -150;

	public float fieldMin = -100;
	public float fieldMax = 100;

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
			MoveMe(delta.x, delta.y);
			_dragg = Input.mousePosition;
			return;
		}

		float zMove = Input.GetAxis("Mouse ScrollWheel");
		if (zMove != 0)
		{
			LiftMe(zMove);
			return;
		}

		float xAxisValue = Input.GetAxis("Horizontal");
		float yAxisValue = Input.GetAxis("Vertical");
		if (xAxisValue == 0 && yAxisValue == 0)
		{
			return;
		}

		MoveMe(xAxisValue, yAxisValue);
	}

	private void MoveMe(float x, float y)
	{
		Vector3 _moveVector = new Vector3(x*horizontalScrollSpeed, 0, y*verticalScrollSpeed) * Time.deltaTime;
		Debug.Log(_moveVector);
		//transform.Translate(_moveVector);

		transform.localPosition = transform.localPosition + _moveVector;

		transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x,fieldMin, fieldMax), transform.localPosition.y,transform.localPosition.z);
	}

	private void LiftMe(float z)
	{
		Vector3 _moveVector = new Vector3(0, -z * zoomSpeed, z * zoomSpeed) * Time.deltaTime;
		transform.Translate(_moveVector);
		transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, depthMin, depthMax ), transform.localPosition.z);
	}

}
