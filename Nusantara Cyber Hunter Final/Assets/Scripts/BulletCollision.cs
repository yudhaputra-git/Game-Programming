using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
	private float enemydamage;
	public GameObject bulletImpact;
	// Use this for initialization
	void Start () {
		enemydamage = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision Collection){
		if(Collection.gameObject.tag == "Player"){ // ini untuk peluru / bullet dari enemy / turret
			PlayerManager playerhit = Collection.gameObject.GetComponent<PlayerManager>();
			playerhit.GetHit(enemydamage);
		}
		// GameObject impactInst = (GameObject)Instantiate(bulletImpact, transform.position, transform.rotation);
		// Destroy(impactInst,2f);
		Destroy(gameObject);
	}
}
