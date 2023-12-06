using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PC : MonoBehaviour
{
	public Transform PCSpawnLocation;
	private int lifeCounter = 3;
	private static GameObject txtLifeCounter;

	private Rigidbody rbody;
	public float speed = 5;

	public float rotationSpeed = 80;
	private Quaternion PCOriginalRotation;
	private float counterXRotation = 0;

	public LayerMask targetLayer;

	private AudioSource _audio;
	public AudioClip sfx_death;

	// Start is called before the first frame update
	void Start()
	{
		rbody = GetComponent<Rigidbody>();

		_audio = GetComponent<AudioSource>();

		// get the PC rotation values in the beginning of the game
		PCOriginalRotation = transform.localRotation;
		Cursor.lockState = CursorLockMode.Locked;

		// find game object for life counter text
		txtLifeCounter = GameObject.Find("Life Counter");
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Detect when NPC Enemy touches PC
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Kill PC if it still has lives left, otherwise the game is over
			if (lifeCounter > 0)
			{
				_audio.PlayOneShot(sfx_death);
				lifeCounter--;
				Debug.Log("Enemy killed PC. Going back to respawn place...");
				// update life counter text on canvas
				txtLifeCounter.GetComponent<TMP_Text>().text = "Vidas (" + lifeCounter.ToString() + ")";
				// go back to respawn location
				transform.position = PCSpawnLocation.position;
			}
			else
			{
				Debug.Log("Game Over! Loading game over scene...");
				SceneManager.LoadSceneAsync(2);
			}
		}
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
