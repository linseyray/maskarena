using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LivesController : MonoBehaviour {

	private int maxNrLives;
	public GameObject fullHeartPrefab;
	public GameObject emptyHeartPrefab;

	private GameObject[][] fullHearts; // For each player, their X amount of lives
	private GameObject[][] emptyHearts;

	public Transform[] positions;
	public float heartOffset = 40;

	void Awake() {
	}

	void Start () {
		fullHearts = new GameObject[4][];
		emptyHearts = new GameObject[4][];
		for (int i = 0; i < fullHearts.Length; i++) {
			fullHearts[i] = new GameObject[maxNrLives];
			emptyHearts[i] = new GameObject[maxNrLives];
		}

		// Instantiate the hearts
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < maxNrLives; j++) {
				Vector2 position = positions[i].position;
				position.x += heartOffset * j;
				GameObject fullHeart = GameObject.Instantiate(fullHeartPrefab);
				GameObject emptyHeart = GameObject.Instantiate(emptyHeartPrefab);
				fullHeart.transform.position = position;
				emptyHeart.transform.position = position;
				fullHearts[i][j] = fullHeart;
				emptyHearts[i][j] = emptyHeart;
			}
		}
	}
	
	void Update () {
	}

	public void SetLives(int playerNumber, int nrLives) {
		Debug.Log(fullHearts);

		for (int i = 0; i < fullHearts.Length; i++) {
			if (i+1 <= nrLives) {
				fullHearts[playerNumber-1][i].SetActive(true);
				emptyHearts[playerNumber-1][i].SetActive(false);
			}
			else 
			if (i+1 <= maxNrLives) {
				fullHearts[playerNumber-1][i].SetActive(false);
				emptyHearts[playerNumber-1][i].SetActive(true);
			}
		}
	}

	public void SetMaxNrLives(int lives) {
		maxNrLives = lives;
	}
}
