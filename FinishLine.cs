using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	public bool crossedLine = false;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.CompareTag ("Finish")) {
			crossedLine = true;
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.CompareTag ("Finish")) {
			crossedLine = false;
		}
	}

	void Update () {
	}
}