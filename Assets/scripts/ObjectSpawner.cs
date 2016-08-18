using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public GameObject objectPrefab;
	public List<Vector2> playerStartingPositions = new List<Vector2> ();
	public Vector3 topLeft;
	public Vector3 bottomRight;

	void Start () {
		initialiseFixedPlayersPositions();
	}

	private void initialiseFixedPlayersPositions() {
		playerStartingPositions = new List<Vector2> ();

		topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
		bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

		Vector2 playerPos1 = new Vector2((bottomRight.x - topLeft.x)*0.25f, (bottomRight.y - topLeft.y)*0.25f);
		Vector2 playerPos2 = new Vector2(-(bottomRight.x - topLeft.x)*0.25f, -(bottomRight.y - topLeft.y)*0.25f);
		Vector2 playerPos3 = new Vector2((bottomRight.x - topLeft.x)*0.25f, -(bottomRight.y - topLeft.y)*0.25f);
		Vector2 playerPos4 = new Vector2(-(bottomRight.x - topLeft.x)*0.25f, (bottomRight.y - topLeft.y)*0.25f);
		playerStartingPositions.Add(playerPos1);
		playerStartingPositions.Add(playerPos2);
		playerStartingPositions.Add(playerPos3);
		playerStartingPositions.Add(playerPos4);
	}

	void Update () {
	}

	public GameObject SpawnObjectRandom() {
		//int randomIndex = (int) Random.Range(0, prefabs.Length);
		GameObject gameObject = (GameObject) Instantiate(objectPrefab, NewRandomPosition(), Quaternion.identity);
		gameObject.transform.SetParent(this.transform);
		return gameObject;
	}

	private Vector2 NewRandomPosition() {
		Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
		Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		return new Vector2(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y));
	}

	public GameObject SpawnObjectFixed(int index) {
		GameObject gameObject = (GameObject) Instantiate(objectPrefab, FixedPosition(index), Quaternion.identity);
		gameObject.transform.SetParent(this.transform);
		return gameObject;
	}

	private Vector2 FixedPosition(int index) {
		return playerStartingPositions[index];
	}
}
