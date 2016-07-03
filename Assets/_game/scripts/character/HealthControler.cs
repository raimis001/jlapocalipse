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
	public ProgressBar HealthProgress;

	public Transform HitTransform;

	public Vector3 position {
		get { return HitTransform.position; }
	}

	private float _currentHealth;


	void Start() 
	{
		_currentHealth = Health;
		if (HealthProgress)
		{
			HealthProgress.Value = _currentHealth / Health;
		}
	}

	public void DoHit(float damage) 
	{
		//Debug.Log("I'm hit!");
		_currentHealth -= damage;
		if (HealthProgress) 
		{
			HealthProgress.Value = _currentHealth / Health;
		}
	}
}
