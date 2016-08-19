using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LivesController : MonoBehaviour {

	public bool isInitialised = false;

	public int maxNrLives;
	public GameObject fullHeartPrefab;
	public GameObject emptyHeartPrefab;

	private List<GameObject>[] fullHearts; // For each player, their X amount of lives
	private List<GameObject>[] emptyHearts;

	public Transform[] positions;
	public float heartOffset = 40;
	public GameObject[] portraits;

	void Awake() {
		fullHearts = new List<GameObject>[4];
		emptyHearts = new List<GameObject>[4];
		for (int i = 0; i < fullHearts.Length; i++) {
			fullHearts[i] = new List<GameObject>();
			emptyHearts[i] = new List<GameObject>();
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isInitialised) {
			// Instantiate the hearts
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < this.maxNrLives; j++) {
					Vector2 position = positions[i].position;
					position.x += heartOffset * j;
					GameObject fullHeart = GameObject.Instantiate(fullHeartPrefab);
					GameObject emptyHeart = GameObject.Instantiate(emptyHeartPrefab);
					fullHeart.transform.position = position;
					emptyHeart.transform.position = position;
					fullHearts[i].Add(fullHeart);
					emptyHearts[i].Add(emptyHeart);
				}
			}
			Debug.Log("Initialised");
			isInitialised = true;
		}
	}

	public void SetLives(int playerNumber, int nrLives) {
		if (!isInitialised) {
			Debug.Log("returning");
			return;
		}
		
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

	// Set from Player.cs
	public void SetMaxNrLives(int lives) {
		this.maxNrLives = lives;
	}
}
