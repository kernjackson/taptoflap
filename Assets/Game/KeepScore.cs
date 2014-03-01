using UnityEngine;
using System.Collections;

public class KeepScore : MonoBehaviour {

	public int value;
	private int score = 0;
	
	public int Score {
		get { return score; }
		set { score += 1; }
	}

}
