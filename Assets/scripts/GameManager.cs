using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int nrPlayers;
	public int maxNrLives;

	private List<GameObject> players;
	private int[] playerScores;

	private ObjectSpawner objectSpawner;

	public GameObject uiHealthPrefab;
	private LivesController livesController;

	public Gamestates gamestate;
	public int WINNING_SCORE = 3;

	public enum Gamestates {
		LOBBY,
		ROUND,
		WINNINGROUND,
		WINNNGMATCH,
		IDLE
	};

	void Awake() {
		objectSpawner = (ObjectSpawner) gameObject.GetComponent<ObjectSpawner>();
		players = new List<GameObject>();
		playerScores = new int[nrPlayers];
		for(int i = 0; i < nrPlayers; i++) {
			playerScores[i] = 0;
		}
	}

	void Start () {
		
	}

	void Update () {
		switch(gamestate) {
		case Gamestates.ROUND: {
				int numberOfPlayersAlive = 0;
				int lastStandingIndex = -1;
				foreach (var p in players) {
					Debug.Log(p);
					if(p.GetComponent<Player>().GetCurrentState()!=Player.PlayerState.DEAD) {
						numberOfPlayersAlive++;
						lastStandingIndex = p.GetComponent<Player>().GetNumber()-1;
					}
				}
				Debug.Log(string.Format("%d alive Players in scene.", numberOfPlayersAlive));
				if(numberOfPlayersAlive < 2) {
					playerScores[lastStandingIndex]++;
					foreach (var score in playerScores) {
						if(score >= WINNING_SCORE) {
							gamestate = Gamestates.WINNNGMATCH;
						} else {
							gamestate = Gamestates.WINNINGROUND;
						}
					}

				}
				break;
			}
		case Gamestates.LOBBY: {
				// TODO: implement lobby logic (adding players, waiting for all to wear a mask)
				ShowCountdown();
				Invoke("StartRound", 2.5f);
				gamestate = Gamestates.IDLE;
				break;
			}
		case Gamestates.WINNINGROUND: {
				// TODO: Show winning text/sprite, countdown from 3 to 1
				Invoke("StartRound", 3f);
				gamestate = Gamestates.IDLE;
				break;
			}
		case Gamestates.WINNNGMATCH: {
				// TODO: show winning player for 10 seconds
				Invoke("StartLobby", 10f);
				gamestate = Gamestates.IDLE;
				break;
			}
		}

	}

	private void ShowCountdown() {
		Countdown countdown = GameObject.Find ("Countdown").GetComponent<Countdown>();
		countdown.Show ();
	}

	private void SpawnPlayers() {
		for (int i = 0; i < nrPlayers; i++) {
			GameObject player = objectSpawner.SpawnObjectFixed(i);
			player.GetComponent<Player>().SetNumber(i+1);
			player.GetComponent<Player>().SetMaxNrLives(maxNrLives);
			player.GetComponent<Player>().SetLivesController(livesController);
			players.Add(player);
		}
	}

	private void SpawnUI() {
		livesController = Instantiate(uiHealthPrefab).GetComponent<LivesController>();
		livesController.SetMaxNrLives(maxNrLives);
	}

	private void StartRound() {
		foreach (var p in players) {
			Destroy(p.gameObject);
		}
		Destroy(livesController);
		gamestate = Gamestates.ROUND;
		SpawnUI();
		SpawnPlayers();

	}
	private void StartLobby() {
		gamestate = Gamestates.LOBBY;
	}
}
