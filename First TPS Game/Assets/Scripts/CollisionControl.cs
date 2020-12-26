using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {

	[SerializeField]
	private GameObject finishpanel, crosshair;
	
	void Start () {
		finishpanel.SetActive(false);
	}
	
	void Update () {
		if (finishpanel.activeSelf)
		{
			crosshair.transform.position = Input.mousePosition;
		}
	}
	void OnCollisionEnter(Collision Collection){
		if(Collection.gameObject.name == "Player"){
			finishpanel.SetActive(true);
		}
	}
}
