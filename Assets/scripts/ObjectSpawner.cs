using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject objectPrefab;

	void Start () {

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
}
