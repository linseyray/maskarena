using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskSpawner : MonoBehaviour {

	public GameObject[] objectPrefabs;
	public float spawnInterval;
	public float intervalRandomRange;
	float spawnTime;
	float timepassed;

	void Start () {
		spawnTime = Random.Range(spawnInterval-intervalRandomRange, spawnInterval+intervalRandomRange);
	}
		
	void Update () {
		if(timepassed > spawnTime) {
			SpawnObjectRandom();
			timepassed = 0;
			spawnTime = Random.Range(spawnInterval-intervalRandomRange, spawnInterval+intervalRandomRange);
		}
		timepassed += Time.deltaTime;
	}

	public GameObject SpawnObjectRandom() {
		int randomIndex = (int) Random.Range(0, objectPrefabs.Length);
		GameObject objectPrefab = objectPrefabs[randomIndex];
		GameObject gameObject = (GameObject) Instantiate(objectPrefab, NewRandomPosition(), Quaternion.identity);
		gameObject.transform.SetParent(this.transform);
		return gameObject;
	}

	private Vector2 NewRandomPosition() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		return new Vector2(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y));
	}
}
