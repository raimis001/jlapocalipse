using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{

	public Animator Animator;

	bool walking;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(transform.position.x);
		if (transform.position.x > 8 && walking)
		{
			Animator.SetTrigger("stopWalk");
			walking = false;
		}
	}

	public void OnMouseUp()
	{
		Animator.SetTrigger("startWalk");
		walking = true;
	}

	public void FixedUpdate()
	{
		Vector3 pos = transform.position;
		pos.z = Mathf.Clamp(pos.z, 3.85f, 3.95f);
		transform.position = pos;
	}

	public void AfterTurn()
	{
		Vector3 rot = transform.localEulerAngles;
		if (rot.y > 45 && rot.y < 225)
			rot.y = 90;
			else rot.y = -90;

		transform.localEulerAngles = rot;

	}

}
