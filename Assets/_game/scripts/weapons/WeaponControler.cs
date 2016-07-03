using UnityEngine;
using System.Collections;

public class WeaponControler : MonoBehaviour
{

	public int Ammo;
	public int AmmoCount = 100;

	public Transform WeaponBody;
	public Transform BulletSpawn;

	public ProgressBar ProgressAmmo;

	private HealthControler Target;

	[Range(0, 2f)]
	public float ShotSpeed = 0.1f;

	private float shot = 2;

	void Start() 
	{
		Ammo = AmmoCount;
	}

	void Update()
	{
		if (Target)
		{
			Vector3 targetPoint = new Vector3(Target.position.x, Target.position.y, Target.position.z) - WeaponBody.position - new Vector3(0,0,0);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

			WeaponBody.rotation = targetRotation;
			targetPoint = WeaponBody.localEulerAngles;


			if (Ammo < 1) 
			{
				shot = ShotSpeed;
			} 
			else 
			{
				targetPoint.x = Mathf.Clamp(targetPoint.x + Random.Range(-2f, 2f), 0, 60);
			}

			WeaponBody.localEulerAngles = targetPoint;

			shot -= Time.deltaTime;
			if (shot <= 0)
			{
				shot = ShotSpeed;
				Bullet.Shot(BulletSpawn.transform.position, BulletSpawn.transform.rotation, 1f);
				Ammo--;
				if (ProgressAmmo) 
				{
					ProgressAmmo.Value = (float)Ammo / AmmoCount;
				}
			}

			return;
		}

		Target = Helper.FindClosestTarget<HealthControler>(transform.position);
		Debug.Log("Find target @" + Target.position);

	}



}
