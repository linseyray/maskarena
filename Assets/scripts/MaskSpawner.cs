using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaskSpawner : MonoBehaviour {

	public GameObject[] objectPrefabs;
	public float spawnInterval;
	public float intervalRandomRange;
	public float stageWidth;
	public float stageHeight;

	float spawnTime;
	float timepassed;

	void Start () {
		spawnTime = Random.Range(spawnInterval-intervalRandomRange, spawnInterval+intervalRandomRange);
	}
		
	void Update () {
		if(timepassed > spawnTime) {
			SpawnObjectRandomInEllipse();
			timepassed = 0;
			spawnTime = Random.Range(spawnInterval-intervalRandomRange, spawnInterval+intervalRandomRange);
		}
		timepassed += Time.deltaTime;
	}

	public GameObject SpawnObjectRandomInEllipse() {
		int randomIndex = (int) Random.Range(0, objectPrefabs.Length);
		GameObject objectPrefab = objectPrefabs[randomIndex];
		GameObject gameObject = (GameObject) Instantiate(objectPrefab, SpawnInEllipse(), Quaternion.identity);
		gameObject.transform.SetParent(this.transform);
		return gameObject;
	}

	private Vector2 SpawnInEllipse() {
		Vector2 unitCircle = Random.insideUnitCircle;
		float x = (float)unitCircle.x * stageWidth / 2.0f;
		float y = (float)unitCircle.y * stageHeight / 2.0f;
		return new Vector2 (x, y);
	}
}
