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

	public GameObject[] portraits;

	void Awake() {
	}

	void Start () {
		fullHearts = new GameObject[4][];
		emptyHearts = new GameObject[4][];

		// Instantiate the hearts
		for (int i = 0; i < 4; i++) {
			fullHearts[i] = new GameObject[maxNrLives];
			emptyHearts[i] = new GameObject[maxNrLives];				
			int orderInLayer = maxNrLives;
			for (int j = 0; j < maxNrLives; j++) {
				Vector2 position = positions[i].position;
				position.x += heartOffset * j;
				GameObject fullHeart = GameObject.Instantiate(fullHeartPrefab);
				GameObject emptyHeart = GameObject.Instantiate(emptyHeartPrefab);
				fullHeart.transform.position = position;
				emptyHeart.transform.position = position;
				fullHeart.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
				emptyHeart.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
				fullHearts[i][j] = fullHeart;
				emptyHearts[i][j] = emptyHeart;
				orderInLayer--;
			}
		}
	}
	
	void Update () {
	}

	public void SetLives(int playerNumber, int nrLives) {
		for (int i = 0; i < fullHearts[playerNumber-1].Length; i++) {
			fullHearts[playerNumber-1][i].SetActive(false);
		}
			
		for (int i = 0; i < fullHearts[playerNumber-1].Length; i++) {
			if (i+1 <= nrLives) {
				fullHearts[playerNumber-1][i].SetActive(true);
			}
		}

		// Grey out if dead
		Debug.Log(portraits.Length);
		SpriteRenderer spriteRenderer = portraits[playerNumber-1].gameObject.GetComponent<SpriteRenderer>();
		Color color = spriteRenderer.color;
		if (nrLives == 0) {
			// Grey out portrait
			color.a = 0.5f;
		}
		else 
			color.a = 1.0F;
		spriteRenderer.color = color;
	}

	public void SetMaxNrLives(int lives) {
		maxNrLives = lives;
	}
}
