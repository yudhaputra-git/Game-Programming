using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera mycam;

	[SerializeField]
	private Rigidbody bullet;
	[SerializeField]
	private Transform firePoint;
	[SerializeField]
	private float bulletspeed = 10000f;
	private Vector3 velocity = Vector3.zero, 
	rotation = Vector3.zero, 
	camrotation = Vector3.zero;

	private float firespeed = 1f;
	private float firecountdown = 0f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();		
	}
	
	public void Move(Vector3 _velocity){
		velocity = _velocity;
	}
	// Update is called once per frame
	void FixedUpdate () {
		PerformMovement();
		PerformRotation();
		if(Input.GetMouseButtonDown(0)){
			if (firecountdown <= 0){
				Shoot();
				firecountdown = 1f / firespeed;
			}
		}
		firecountdown -= Time.deltaTime;
	}

	void PerformMovement(){
		if(velocity != Vector3.zero){
			rb.AddForce(velocity);
		}
	}

	public void Rotate(Vector3 _rotation){
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _camrotation){
		camrotation = _camrotation;
	}

	void PerformRotation(){
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if(mycam != null){
			mycam.transform.Rotate(-camrotation);
		}
	}

	void Shoot(){
		Rigidbody shot = Instantiate(bullet, firePoint.position, firePoint.rotation) as Rigidbody;
		shot.AddForce(firePoint.forward * bulletspeed);
		Destroy(shot.gameObject, 2f);
		return;
	}
}
