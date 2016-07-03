using UnityEngine;
using System.Collections;

public class WeaponControler : MonoBehaviour
{

	public Transform WeaponBody;
	public Transform BulletSpawn;

	private HealthControler Target;

	private float shot = 2;


	void Update()
	{
		if (Target)
		{
			Vector3 targetPoint = new Vector3(Target.position.x, Target.position.y, Target.position.z) - WeaponBody.position - new Vector3(0,0,0);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

			WeaponBody.rotation = targetRotation;
			targetPoint = WeaponBody.localEulerAngles;

			targetPoint.x = Mathf.Clamp(targetPoint.x + Random.Range(-2f,2f) , 0, 60) ;
			WeaponBody.localEulerAngles = targetPoint;

			shot -= Time.deltaTime; 
			if (shot <= 0) 
			{
				shot = 0.1f;
				Bullet.Shot(BulletSpawn.transform.position, BulletSpawn.transform.rotation);
			}

			return;
		}

		Target = Helper.FindClosestTarget<HealthControler>(transform.position);
		Debug.Log("Find target @" + Target.position);

	}
}
