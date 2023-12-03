using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
	private Rigidbody rbody;
	public float speed = 5;

	public float rotationSpeed = 80;
	private Quaternion PCOriginalRotation;
	private float counterXRotation = 0;

	// Start is called before the first frame update
	void Start()
	{
		rbody = GetComponent<Rigidbody>();
		// get the PC rotation values in the beginning of the game
		PCOriginalRotation = transform.localRotation;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{
		// Movement
		float moveForward = Input.GetAxis("Vertical");
		float moveSideways = Input.GetAxis("Horizontal");
		rbody.velocity = transform.TransformDirection(
			new Vector3(moveSideways * speed, rbody.velocity.y, moveForward * speed)
		);

		// Rotation with mouse
		counterXRotation += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
		// get angle of the axis for the number (counterXRotation) in the X axis
		Quaternion yRotation = Quaternion.AngleAxis(counterXRotation, Vector3.up);
		transform.localRotation = PCOriginalRotation * yRotation;
	}
}
