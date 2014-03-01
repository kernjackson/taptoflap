using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;
	public AudioClip flapSound;

	private static int boosts;
	private bool gameOver = false;
	private Vector3 startPosition = new Vector3(0,0,0); // this is just to get rid of an annoying compiler warning
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}

	void Update () {

		if (!gameOver) {
			transform.Translate (3f * Time.deltaTime, 0f, 0f);
			if ( ( (Input.GetKeyDown ("space")) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ) && (transform.position.y < 5) ) {
				rigidbody.velocity = new Vector3 (0, 4, 0);
				audio.PlayOneShot(flapSound, 0.7F);
			}
			if (transform.localPosition.y < -2) {
				gameOver = true;
				Reset ();
				GameOver();
				GameEventManager.TriggerGameOver();
			}
		}

		distanceTraveled = transform.localPosition.x;
	}
			
	void OnCollisionEnter(Collision collision) {
		gameOver = true;
		GameOver();
		GameEventManager.TriggerGameOver();
	}

	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

	private void Reset() {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		transform.localEulerAngles = new Vector3(0,0,0);
		transform.localPosition = startPosition;
		transform.localPosition = new Vector3(-7f,5f,-1.2f);
		
		gameOver = false;
	}
	
	private void GameStart () {
		Reset ();
	}
	
	void GameOver() {
		rigidbody.velocity = Vector3.zero;
	}

}