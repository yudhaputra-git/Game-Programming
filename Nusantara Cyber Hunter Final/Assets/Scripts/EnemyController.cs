using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyController : MonoBehaviour {
    private Animator animasi;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject player;
    public Transform rotatable;
    public Transform firePoint;
    private float turnSpeed = 20f, bulletspeed = 10000f, fireCountdown = 1.6f;
    public Rigidbody bullet;
    private float health;

    void Start () {
        health = 100f;
        animasi = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("PlayerTarget");
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update () {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Choose the next destination point when the agent gets
        //close to the current one.
        if(health <= 0){
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            agent.isStopped = true;
            animasi.SetBool("isDied",true);
			Destroy(gameObject,1.5f);
		}
        if(distanceToPlayer > 20f && health > 0){
            agent.speed = 3f;
            animasi.SetBool("isIdle",false);
            animasi.SetBool("isWalking",true);
            animasi.SetBool("isShooting", false);
            animasi.SetBool("isShootingForward",false);
            if (!agent.pathPending && agent.remainingDistance < 1f)
                GotoNextPoint();
        }
        else if(distanceToPlayer > 15f && health > 0){
            agent.speed = 1f;
            agent.SetDestination(player.transform.position);
            animasi.SetBool("isIdle",false);
            animasi.SetBool("isWalking",false);
            animasi.SetBool("isShooting", false);
            animasi.SetBool("isShootingForward",true);
            Vector3 dir = (player.transform.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
            Quaternion characterRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            Vector3 rotation = Quaternion.Lerp(rotatable.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            Vector3 bodyrotation = Quaternion.Lerp(rotatable.rotation, characterRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(bodyrotation);
            rotatable.rotation = Quaternion.Euler(rotation);
            if (fireCountdown <= 0){
                Shoot();
                fireCountdown = 0.6f;
            }
            fireCountdown -= Time.deltaTime;
        }
        else if(distanceToPlayer <= 10f && health > 0){
            agent.speed = 0;
            animasi.SetBool("isIdle",false);
            animasi.SetBool("isWalking",false);
            animasi.SetBool("isShooting", true);
            animasi.SetBool("isShootingForward",false);
            Vector3 dir = (player.transform.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
            Quaternion characterRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            Vector3 rotation = Quaternion.Lerp(rotatable.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            Vector3 bodyrotation = Quaternion.Lerp(rotatable.rotation, characterRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(bodyrotation);
            rotatable.rotation = Quaternion.Euler(rotation);
            if (fireCountdown <= 0){
                Shoot();
                fireCountdown = 0.6f;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    public void GetHit(float damage){
		health -= damage;
        FindObjectOfType<AudioManager>().Play("EnemyHit");
	}

    void Shoot(){
        Rigidbody shot = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
        shot.AddForce(firePoint.forward * bulletspeed);
        Destroy(shot.gameObject, 10f);
    }
}