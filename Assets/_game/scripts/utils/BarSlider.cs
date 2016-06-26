using UnityEngine;

public class BarSlider : MonoBehaviour
{

	public float Value 
	{
		get { return transform.localPosition.x; }
	}
	private Vector3 _mouse;

	public void OnMouseDrag()
	{

		float distance = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		Vector3 move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
		transform.position = new Vector3(move.x, transform.position.y, transform.position.z);
		transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, 0f, 1f), transform.localPosition.y, transform.localPosition.z);

	}
}
