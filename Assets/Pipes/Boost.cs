using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class Boost : MonoBehaviour {
	
	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;

	public Transform prefab;
	public int numberOfObjects;
	public Vector3 startPosition;
	public AudioClip coin;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;


	void Start () {
		objectQueue = new Queue<Transform> (numberOfObjects);
		nextPosition = startPosition;
		
		nextPosition.z = 4;
		nextPosition.y = -1;
		
		for (int i = 0; i < numberOfObjects; i++) {
			
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 3);
			objectQueue.Enqueue(o);
			
		}
	}
	
	void Update () {
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = nextPosition;
			nextPosition.x += (o.localScale.x + 3);
			objectQueue.Enqueue(o);
		}
	}
	
	void OnTriggerEnter () {
		audio.PlayOneShot(coin, 0.7F);
		Debug.Log ("play sound");
		Runner.AddBoost();
	}
	
	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(1f, 3f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

}