using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int nrPlayers;

	private List<GameObject> players;
	private ObjectSpawner objectSpawner;

	void Awake() {
		objectSpawner = (ObjectSpawner) gameObject.GetComponent<ObjectSpawner>();
	}

	void Start () {
		players = new List<GameObject>();
		SpawnPlayers();
	}

	void Update () {
	}

	private void SpawnPlayers() {
		for (int i = 0; i < nrPlayers; i++) {
			GameObject player = objectSpawner.SpawnObjectRandom();
			player.GetComponent<Player>().SetNumber(i+1);
			players.Add(player);
		}
	}
}
