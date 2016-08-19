using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LivesController : MonoBehaviour {

	private bool initialised = false;

	public int maxNrLives;
	private int nrLives;
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
		if (!initialised) {
			// Instantiate the hearts
			for (int i = 0; i < 4; i++) {
				Debug.Log("lives in loop " + maxNrLives);
				for (int j = 0; j < this.maxNrLives; j++) {
					Vector2 position = positions[i].position;
					position.x += heartOffset * j;
					GameObject fullHeart = GameObject.Instantiate(fullHeartPrefab);
					GameObject emptyHeart = GameObject.Instantiate(emptyHeartPrefab);
					fullHeart.transform.position = position;
					emptyHeart.transform.position = position;
					Debug.Log(fullHearts);
					fullHearts[i].Add(fullHeart);
					emptyHearts[i].Add(emptyHeart);
				}
			}
			initialised = true;
		}
	}

	public void TakeLife(int playerNumber) {
		nrLives--;
	}

	// Set from Player.cs
	public void SetNrLives(int lives) {
		this.maxNrLives = lives;
		Debug.Log("Max lives " + this.maxNrLives);
	}
}
