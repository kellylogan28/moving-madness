using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagingScene : MonoBehaviour {

	public List<GameObject> furniture;
	public GameObject currentObject;
	public GameObject nextObject;
	public Vector3 nextPos;
	public Vector3 currentPos;
	public bool sleeping;
	public Text itemsLeft;
	public float itemsRemaining;
	public float time;
	public Text timeText;
	public static float finalScore = 0;

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (currentPos, 1);
		Gizmos.DrawWireSphere (nextPos, 1);
	}

	void Start () {
		StartTime ();
	}

	void Update () {
		if (furniture.Count > 0) {
			CheckNext ();
		}
		CheckCurrent ();
		Sleeping ();
		if (currentObject == null) {
			LoseCondition ();
		}
		WinCondition ();
		StartTime ();
		time = time - Time.deltaTime;
		if (time < 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		int count = furniture.Count;
		if (nextObject != null) {
			count++;
		}
		itemsLeft.text = count + " Items Left";
		itemsRemaining = count;
	}

	void CheckNext () {
		if (nextObject == null) {
			nextObject = Instantiate (furniture [0], nextPos, new Quaternion ()) as GameObject;
			furniture.RemoveAt (0);
		}
	}

	void CheckCurrent () {
		if (currentObject == null || currentObject.GetComponent<DragDrop1>() == null) {
			currentObject = null;
			if (nextObject != null) {
				currentObject = nextObject;
				currentObject.transform.position = currentPos;
				currentObject.GetComponent <DragDrop1> ().enabled = true;
				currentObject.GetComponent <Collider2D> ().isTrigger = false;
				nextObject = null;
			}
		}
	}

	void Sleeping () {
		Rigidbody2D[] rbsSleep = GameObject.FindObjectsOfType<Rigidbody2D> ();
		foreach (Rigidbody2D rbSleep in rbsSleep) {
			if (!rbSleep.IsSleeping ()) {
				sleeping = false;
				print (rbSleep.name + " is not sleeping");
				return;
			} else {
				sleeping = true;
				print (rbSleep.name + " is sleeping");
			}
		}
	}

	void WinCondition () {
		Rigidbody2D[] rbsWin = GameObject.FindObjectsOfType<Rigidbody2D> ();
		foreach (Rigidbody2D rbWin in rbsWin) {
			bool crossedLine = rbWin.GetComponent<FinishLine> ().crossedLine;
			if (sleeping && crossedLine) {
				finalScore = Mathf.Round (time * itemsRemaining);
				SceneManager.LoadScene ("You Win");
			}
		}
	}

	void LoseCondition () {
		if (sleeping) {
				SceneManager.LoadScene ("You Lose");
		}
	}

	void StartTime () {
		timeText.text = "Time remaining: " + Mathf.Round (time).ToString () + " seconds";
	}		
}