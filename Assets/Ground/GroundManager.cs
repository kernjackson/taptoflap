using UnityEngine;
using System.Collections.Generic;

public class GroundManager : MonoBehaviour {
	
	public Transform prefab, pipe;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;

	private Vector3 nextPosition;

	private bool oneShot = false;
	
	private Queue<Transform> objectQueue;
	
	void Start () {

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;

		if (!oneShot) {
			SetUp ();
			oneShot = true;
		}
	}

	void SetUp () {
		objectQueue = new Queue<Transform> (numberOfObjects);

		nextPosition = startPosition;
		
		nextPosition.z = 4;
		nextPosition.y = -1;
		
		for (int i = 0; i < numberOfObjects; i++) {
			
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 1);
			objectQueue.Enqueue(o);
		}
	}

	void Update () {
		if (objectQueue.Peek ().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			Transform o = objectQueue.Dequeue ();
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 1);
			objectQueue.Enqueue (o);
		}
	}

	void Reset () {
		nextPosition = new Vector3(20f,-1f,3f);
	
		for (int i = 0; i < numberOfObjects; i++) {			
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 1);
			objectQueue.Enqueue(o);
		}
	}
	

	private void GameStart () {
		enabled = true;
		startPosition =  new Vector3(20f,-4f,0f);
		Reset ();
	}
	
	private void GameOver () {
		enabled = false;
	}
}