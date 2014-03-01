using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText scoreText, instructionsText;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}

	void Update () {
		if ( (Input.GetKeyDown ("space")) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ) {
 			GameEventManager.TriggerGameStart();
		}
	}
	private void GameStart () {
		instructionsText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		instructionsText.enabled = true;
		enabled = true;
	}
	
	public static void SetBoosts(int boosts){
		instance.scoreText.text = boosts.ToString();
	}

}