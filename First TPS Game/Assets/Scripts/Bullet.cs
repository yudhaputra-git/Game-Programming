using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject bulletImpact, enemyImpact;
	private int playerlife, damage = 1, score;
	// Update is called once per frame
	void Start(){
		playerlife = PlayerPrefs.GetInt("MyLife");
	}
	void Update () {
		score = PlayerPrefs.GetInt("Score");
	}

	void OnCollisionEnter(Collision Collection){
		if(Collection.gameObject.tag == "Enemy"){ //ini untuk peluru / bullet dari player
			score += 100;
			PlayerPrefs.SetInt("Score", score);
			GameObject enemyimpactInst = (GameObject)Instantiate(enemyImpact, Collection.gameObject.transform.position, Collection.gameObject.transform.rotation);
			Destroy(enemyimpactInst,1.5f);
			Destroy(Collection.gameObject, 0.5f); //destroy gameobject enemy
		}
		if(Collection.gameObject.name == "Player"){ // ini untuk peluru / bullet dari enemy / turret
			playerlife -= damage * 5; //mengurangi nyawa berdasarkan damage
			PlayerPrefs.SetInt("MyLife", playerlife);
			//Debug.Log(playerlife);
		}
		GameObject impactInst = (GameObject)Instantiate(bulletImpact, transform.position, transform.rotation);
		Destroy(impactInst,2f);
		Destroy(gameObject);
	}
}
