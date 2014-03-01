using UnityEngine;
using System.Collections.Generic;

public class MountainManager : MonoBehaviour {
	
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
		nextPosition.z = 3;
		
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + Random.Range(-10,2));
			nextPosition.y = Random.Range(-40,-25);
			objectQueue.Enqueue(o);
		}
	}
	
	void Update () {
		if (objectQueue.Peek ().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			PopulateQueue();
		}
	}

	void PopulateQueue() {
		Transform o = objectQueue.Dequeue();
		o.localPosition = nextPosition;
		nextPosition.x += (o.localScale.x + 0);
		nextPosition.y = Random.Range(-40,-25);
		objectQueue.Enqueue(o);
	}
	
	void Reset () {
		nextPosition = new Vector3(0f,-30f,50);
		for (int i = 0; i < numberOfObjects; i++) {
			PopulateQueue();
		}
		
	}
	
	private void GameStart () {
		enabled = true;
		startPosition = new Vector3(0f,-200f,50);
		Reset ();
	}
	
	private void GameOver () {
		enabled = false;
	}

}
