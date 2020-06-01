using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {

	[SerializeField]
	private GameObject finishpanel;
	// Use this for initialization
	void Start () {
		finishpanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision Collection){
		if(Collection.gameObject.name == "Player"){ //jika player menyentuh objek yang memiliki collider (berimbuhan script) ini
			finishpanel.SetActive(true);
			Cursor.visible = true;
		}
	}
}
