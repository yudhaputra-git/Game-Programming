using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	[Header("Attributes")]
	public float health = 100f;
	public float armor = 5f;
    public float speed;
	private float bulletspeed = 100000f;
	public float weaponRange = 10f;
	private int jumpcount = 0;
	[SerializeField]
	private Camera mycam;
	private float sensitivity = 10f;

	[SerializeField]
	private LayerMask mask;
	public GameObject target;
	public Transform firePoint;

    private Rigidbody rb;

	public Rigidbody bullet;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		Physics.gravity = new Vector3(0, -9, 0);
    }

    void Update ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
		
		if(Input.GetKeyDown("space")){
			if(jumpcount < 2){
				Vector3 jump = new Vector3(0,20,0);
				rb.AddForce(jump * speed);
				jumpcount += 1;
			}
			else
			{
				if(transform.position.y <= 0.5f){
					jumpcount = 0;
				}
			}
		}
		if(Input.GetMouseButtonDown(0)){
			Rigidbody shot = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
			shot.AddForce(firePoint.forward * bulletspeed);
			Destroy(shot.gameObject, 1f);
			
		}

		if(health <= 0){
			Debug.Log("U DED");
		}
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);
		
		float yRotation = Input.GetAxisRaw("Mouse X");
		//Vector3 rotation = new Vector3(0f, yRotation, 0f) * sensitivity;
		//rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

		float xRotation = Input.GetAxisRaw("Mouse Y");
		//Vector3 camRotation = new Vector3(xRotation, 0f, 0f) * sensitivity;
		// if(mycam != null){
		// 	mycam.transform.Rotate(-camRotation);
		// }
		Vector3 myrotation = new Vector3(-xRotation, yRotation, 0f) * sensitivity;
		rb.MoveRotation(rb.rotation * Quaternion.Euler(myrotation));


		Cursor.visible = false;
		target.GetComponent<RectTransform>().position = Input.mousePosition;
    }

	public void getHit(float damage){
		float finaldamage = damage - armor;
		health -= finaldamage;
		Debug.Log(health.ToString());
	}
}