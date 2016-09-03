using UnityEngine;
using System.Collections;

public class DroneForce : MonoBehaviour
{
	[Range(0, 1000)]
	public float VectorY = 100;

	[Range(0, 10)]
	public float Hight = 6;


	public Transform Attachment;
	public Transform[] Rotors;

	Rigidbody _rigidbody;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		_rigidbody.AddForce(new Vector3(0, Mathf.Lerp(50, VectorY, 1f - transform.localPosition.y / Hight), 0));

		foreach (Transform rotor in Rotors)
		{
			rotor.Rotate(0, VectorY * Time.deltaTime * 10f, 0);
		}

		if (Attachment) Attachment.position = transform.position;
	}
}
