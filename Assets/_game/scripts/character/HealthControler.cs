using UnityEngine;
using System.Collections;

public enum HealthKind {
	Player,
	Enemy
}

public class HealthControler : MonoBehaviour
{

	public float Health;
	public HealthKind Kind;

	public Transform HitTransform;

	public Vector3 position {
	get { return HitTransform.position; }
	}

	public void DoHit(float damage) 
	{

	}
}
