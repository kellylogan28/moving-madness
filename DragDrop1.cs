using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DragDrop1 : MonoBehaviour {

	float distance = 10;
	public Rigidbody2D myRigidbody;
	public float speed = 0.5f;

	void OnMouseDrag () {
		if (enabled == false) {
			return;
		}
		myRigidbody.isKinematic = true;
		transform.Rotate (0,0,speed);
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
		GetComponent <UpperScreenClamp> ().enabled = true;
	}

	void OnMouseUp () {
		GetComponent <UpperScreenClamp> ().enabled = false;
		if (enabled == false) {
			return;
		}
		myRigidbody.isKinematic = false;
//		GetComponent <LowerScreenClamp> ().enabled = true;
		Destroy (this);
	}

	void Update () {
	}
}
