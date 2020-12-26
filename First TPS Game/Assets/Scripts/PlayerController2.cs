using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController2 : MonoBehaviour {

	private int jumpcount = 0;
	[SerializeField]
	private float speed = 5f;
	private Vector3 jump = Vector3.zero;
	private float sensitivity = 3f;
	private Rigidbody rb;
	private PlayerMotor motor;

	void Start(){
		motor = GetComponent<PlayerMotor>();
		rb = GetComponent<Rigidbody>();	
	}

	void Update(){
		//Movement
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

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movement * speed);
		//end of movement

		//aiming
		float yRotation = Input.GetAxisRaw("Mouse X");
		Vector3 rotation = new Vector3(0f, yRotation, 0f) * sensitivity;
		motor.Rotate(rotation);

		float xRotation = Input.GetAxisRaw("Mouse Y");
		Vector3 camRotation = new Vector3(xRotation, 0f, 0f) * sensitivity;
		motor.RotateCamera(camRotation);

		Cursor.visible = false;
	}
}
