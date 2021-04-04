using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivillianController : MonoBehaviour {
	private Animator animasi;
	private GameObject[] enemy;
	private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		animasi = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.isStopped = true;
		enemy = GameObject.FindGameObjectsWithTag("Dummie");
		if(enemy.Length != 0){
			float distance = Vector3.Distance(transform.position, GetClosestEnemy(enemy).position);
			if(distance < 30f){
				animasi.SetBool("isPraying", true);
				animasi.SetBool("isCheering", false);
			}
			else {
				animasi.SetBool("isCheering",true);
				animasi.SetBool("isPraying",false);
			}
		}
		else{
			animasi.SetBool("isCheering",true);
			animasi.SetBool("isPraying",false);
		}
		
	}

	Transform GetClosestEnemy(GameObject[] enemies){
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

}
