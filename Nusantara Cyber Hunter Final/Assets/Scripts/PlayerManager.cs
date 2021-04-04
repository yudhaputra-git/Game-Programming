using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
	private float health;
	public Image healthbar;
	// Use this for initialization
	void Start () {
		health = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = health / 100f;
		if(health <= 0){
			Debug.Log("Game Over");
		}
	}

	public void GetHit(float damage){
		health -= damage;
		Debug.Log("Player Health : " + health);
	}
}
