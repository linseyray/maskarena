using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int nrPlayers;
	public int maxNrLives;

	private List<GameObject> players;

	private ObjectSpawner objectSpawner;
	private LivesController livesController;

	public Gamestates gamestate;
	public int WINNING_SCORE = 3;

	public enum Gamestates {
		LOBBY,
		ROUND,
		WINNINGROUND,
		WINNNGMATCH
	};

	void Awake() {
		objectSpawner = (ObjectSpawner) gameObject.GetComponent<ObjectSpawner>();
		livesController = gameObject.GetComponentInChildren<LivesController>();
	}

	void Start () {
		players = new List<GameObject>();
		SpawnPlayers();
		livesController.SetMaxNrLives(maxNrLives); // Spawns the UI elements
	}

	void Update () {
		switch(gamestate) {
		case Gamestates.ROUND: {
				int numberOfPlayersAlive = 0;
				Player lastStanding = null;
				foreach (var p in players) {
					if(p.GetComponent<Player>().GetCurrentState()!=Player.PlayerState.DEAD) {
						numberOfPlayersAlive++;
						lastStanding = p.GetComponent<Player>();
					}
				}
				if(numberOfPlayersAlive < 2) {
					lastStanding.increaseScore();
					if(lastStanding.score >= WINNING_SCORE) {
						gamestate = Gamestates.WINNNGMATCH;
					} else {
					gamestate = Gamestates.WINNINGROUND;
					}
				}
				break;
			}
		case Gamestates.LOBBY: {
				// TODO: implement lobby logic (adding players, waiting for all to wear a mask)
				Invoke("StartRound", 2f);
				break;
			}
		case Gamestates.WINNINGROUND: {
				// TODO: Show winning text/sprite, countdown from 3 to 1
				Invoke("StartRound", 3f);
				break;
			}
		case Gamestates.WINNNGMATCH: {
				// TODO: show winning player for 10 seconds
				Invoke("StartLobby", 10f);
				break;
			}
		}

	}

	private void SpawnPlayers() {
		for (int i = 0; i < nrPlayers; i++) {
			GameObject player = objectSpawner.SpawnObjectFixed (i);
			player.GetComponent<Player>().SetNumber(i+1);
			player.GetComponent<Player>().SetMaxNrLives(maxNrLives);
			players.Add(player);
		}
	}

	private void StartRound() {
		gamestate = Gamestates.ROUND;
	}
	private void StartLobby() {
		gamestate = Gamestates.LOBBY;
	}
}
