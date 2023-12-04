using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPC : MonoBehaviour
{
	private Quaternion initialRotation;
	public float rotationSpeed = 80;
	private float counterYRotation = 0;
	
	// Start is called before the first frame update
	void Start()
	{
		initialRotation = transform.localRotation;
	}

	// Update is called once per frame
	void Update()
	{
		counterYRotation += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * rotationSpeed;
		counterYRotation = Mathf.Clamp(counterYRotation, -60, 60);
		Quaternion rotX = Quaternion.AngleAxis(counterYRotation, Vector3.left);

		transform.localRotation = initialRotation * rotX;
	}
}
