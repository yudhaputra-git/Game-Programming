using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject enemy;
	
	[Header("Attributes")]

	public float range = 15f;
	public float fireCountdown = 0f;
	public float fireSpeed = 1f;
	private float bulletspeed = 1000f;
	public Transform rotatable;
	private float turnSpeed = 10f;
	public Rigidbody bullet;
	public Transform firePoint;

	// Use this for initialization
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);
		
		if(distanceToPlayer <= range){
			Vector3 smoother = new Vector3 (0,1,0);
			Vector3 dir = enemy.transform.position - transform.position - smoother;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(rotatable.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // x, y , z
			rotatable.rotation = Quaternion.Euler(rotation);
		}
		else{
			return;
		}

		if (fireCountdown <= 0){
			Shoot();
			fireCountdown = 1f / fireSpeed;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot(){
		Rigidbody shot = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
		shot.AddForce(firePoint.forward * bulletspeed);
		Destroy(shot.gameObject, 1f);
	}	

}
