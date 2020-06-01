using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTutorial : MonoBehaviour {

	[SerializeField]
	private GameObject target;
	private float range = 5f;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(transform.position, target.transform.position);

		if(dist <= range){
			transform.LookAt(target.transform);
		}
		else{
			return;
		}
		
	}
}
