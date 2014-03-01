using UnityEngine;
using System.Collections.Generic;

public class PipeManager : MonoBehaviour {

	public Transform prefab;
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

	void SetUp() {
		objectQueue = new Queue<Transform> (numberOfObjects);
		nextPosition = startPosition;
		
		nextPosition.y = 3; // this is a hack to make the first pipe not be stuck in the ground
		nextPosition.z = 3;
		
		for (int i = 0; i < numberOfObjects; i++) {
			
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 3);
			nextPosition.y = Random.Range(1,5);
			objectQueue.Enqueue(o);
		}
	}
	
	void Update () {
		if (objectQueue.Peek ().localPosition.x + recycleOffset < Runner.distanceTraveled) {

			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 3);
			nextPosition.y = Random.Range(1f,4.5f);
			objectQueue.Enqueue(o);
		}
	}

	void Reset () {
		nextPosition = new Vector3(0f,3f,1.7f);
	
		for (int i = 0; i < numberOfObjects; i++) {
			
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 3);
			nextPosition.y = Random.Range(1f,4.5f);
			objectQueue.Enqueue(o);
		}
	}

	private void GameStart () {
		enabled = true;
		startPosition =  new Vector3(0f,0f,1.7f);
		Reset ();
	}

	private void GameOver () {
		enabled = false;
	}

}
