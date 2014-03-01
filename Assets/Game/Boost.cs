using UnityEngine;
using System.Collections;

public class Scored : MonoBehaviour {

	KeepScore keepScore = new KeepScore();

	void OnTriggerEnter(Collider other) {
		keepScore.Score += 1;
		//Debug.Log ("score: " + keepScore.Score);
	}

}
