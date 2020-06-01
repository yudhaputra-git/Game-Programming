using UnityEngine;

public class PlayerControllerTutorial : MonoBehaviour {

	// Use this for initialization
	private float bulletspeed = 100f, speed = 10f, sensitivity = 5f;

	[SerializeField]
	private Transform firepoint;

	[SerializeField]
	private Camera mycam;

	[SerializeField]
	private Rigidbody bullet;
	private Rigidbody rb;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float xMove = Input.GetAxisRaw("Horizontal");
		float yMove = Input.GetAxisRaw("Vertical");
		Vector3 movement = new Vector3(xMove, 0, yMove);
		rb.AddForce(movement * speed);

		if(Input.GetKeyDown("space")){
			Vector3 jump = new Vector3(0, 20, 0);
			rb.AddForce(jump * speed);
		}

		float yRotate = Input.GetAxisRaw("Mouse X");
		Vector3 rotation = new Vector3(0, yRotate, 0) * sensitivity;
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		
		float xRotate = Input.GetAxisRaw("Mouse Y");
		Vector3 camrotation = new Vector3(xRotate, 0, 0) * sensitivity;
		if(mycam !=null){
			mycam.transform.Rotate(-camrotation);
		}

		if(Input.GetMouseButtonDown(0)){
			Shoot();
		}

	}

	void Shoot(){
		Rigidbody shot = Instantiate(bullet, firepoint.position, firepoint.rotation);
		shot.AddForce(transform.forward * bulletspeed);

		Destroy(shot.gameObject, 2f);
		return;
	}
}
