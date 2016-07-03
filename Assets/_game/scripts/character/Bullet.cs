using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public Rigidbody Body;
	public float damage = 0.5f;

	public GameObject Blood;
	public GameObject Sphere;

	public static void Shot(Vector3 position, Quaternion rotation, float damage)
	{
		GameObject obj = (GameObject)Instantiate(GameLogic.Instance.BulletPrefab, position, rotation);

		obj.GetComponent<Bullet>().DoShot(damage);
	}


	public void DoShot(float damage)
	{
		//Body.AddForce(Vector3.left * 30);
		this.damage = damage;
		Body.velocity = transform.forward * 70;
		Destroy(gameObject, 1);
	}

	public void OnCollisionEnter(Collision collision)
	{
		HealthControler health = collision.gameObject.GetComponent<HealthControler>();
		Body.velocity = Vector3.zero;
		Body.angularVelocity = Vector3.zero;

		if (health)
		{
			health.DoHit(damage);
			Blood.SetActive(true);
			Sphere.SetActive(false);

			Destroy(gameObject, 0.3f);
			return;
		}

		Destroy(gameObject);
	}
}
