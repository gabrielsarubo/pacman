using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public GameObject PC;
	// the destination where the PC should teleport to
	public GameObject teleportTo;

	private void OnCollisionEnter(Collision collision) {
		Debug.Log("Collided with a portal");
		PC.transform.position = teleportTo.transform.position;
	}
}
