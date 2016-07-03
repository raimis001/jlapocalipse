using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{

	[Range(0, 1000)]
	public float vectorY = 100;

	[Range(0, 10)]
	public float Hight = 6;

	public Transform Attachment;
	public Inventory Inventory;

	public Transform[] Rotors;

	Rigidbody Rigidbody;
	// Use this for initialization
	void Start()
	{
		Rigidbody = GetComponent<Rigidbody>();
		Inventory.OnChange = OnInventoryChange;
	}

	// Update is called once per frame
	void Update()
	{
		//if (transform.localPosition.y < 5)
		Rigidbody.AddForce(new Vector3(0, Mathf.Lerp(50, vectorY, 1f - transform.localPosition.y / Hight), 0));

		foreach (Transform rotor in Rotors)
		{
			rotor.Rotate(0, vectorY * Time.deltaTime * 10f, 0);
		}

		if (Attachment) Attachment.position = transform.position;
	}

	void OnInventoryChange()
	{
		Rigidbody.mass = 10 + Inventory.Count * 3;
	}

	public void OnMouseDown()
	{
		//Debug.Log("Drone mouse down");
	}

	public void OnMouseUp()
	{
		//Debug.Log("Drone mouse up");
	}

	public void OnMouseUpAsButton()
	{
		Inventory.Visible = !Inventory.Visible;
	}
}
