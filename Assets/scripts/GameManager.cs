using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int nrPlayers;
	public int maxNrLives;
	public Gamestates gamestate;
	public int WINNING_SCORE = 3;
	public GameObject uiHealthPrefab;
	public GameObject winnerBackgroundGameObject;
	public GameObject titleScreen;

	private Player winnerPlayer;
	private List<GameObject> players;
	private int[] playerScores;
	private ObjectSpawner objectSpawner;
	private LivesController livesController;
	private WinnerSplash winnerBackground;

	public enum Gamestates {
		LOBBY,
		ROUND,
		WINNINGROUND,
		WINNNGMATCH,
		IDLE,
		TITLE
	};

	void Awake() {
		objectSpawner = (ObjectSpawner) gameObject.GetComponent<ObjectSpawner>();
		players = new List<GameObject>();
		playerScores = new int[nrPlayers];
		for(int i = 0; i < nrPlayers; i++) {
			playerScores[i] = 0;
		}
		winnerBackground = (WinnerSplash)winnerBackgroundGameObject.gameObject.GetComponent<WinnerSplash> ();
	}

	void Start () {
	}

	void Update () {
		switch(gamestate) {
		case Gamestates.ROUND: {
				List<GameObject> alivePlayers = new List<GameObject>(); 
				int lastStandingIndex = -1;
				foreach (var p in players) {
					if(p.GetComponent<Player>().GetCurrentState()!=Player.PlayerState.DEAD) {
						alivePlayers.Add (p);
						lastStandingIndex = p.GetComponent<Player>().GetNumber()-1;
					}
				}
				int numberOfPlayersAlive = alivePlayers.Count;
				Debug.Log(string.Format("%d alive Players in scene.", numberOfPlayersAlive));
				if(numberOfPlayersAlive <= 1) {
					playerScores[lastStandingIndex]++;
					foreach (var score in playerScores) {
						if(score >= WINNING_SCORE) {
							winnerPlayer = alivePlayers [0].GetComponent<Player>();
							gamestate = Gamestates.WINNNGMATCH;
						} else {
							gamestate = Gamestates.WINNINGROUND;
						}
					}

				}
				break;
			}
		case Gamestates.LOBBY: {
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
				ShowWinner(winnerPlayer);
				Invoke("StartLobby", 10f);
				gamestate = Gamestates.IDLE;
				break;
			}
		case Gamestates.TITLE: {
				Invoke("StartLobby", 5f);
				gamestate = Gamestates.IDLE;
				break;
			}
		default:
			break;
		}

	}

	private void ShowCountdown() {
		Countdown countdown = GameObject.Find ("Countdown").GetComponent<Countdown>();
		countdown.Show ();
	}

	private void ShowWinner(Player winnerPlayer) {
		GameObject winnerScreen = winnerPlayer.GetWinnerScreen ();
		WinnerSplash splash = (WinnerSplash)winnerScreen.GetComponent("WinnerSplash");
		winnerBackground.Show ();
		splash.Show ();
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
		if(titleScreen.activeInHierarchy) {
			titleScreen.SetActive(false);
		}
		gamestate = Gamestates.LOBBY;
	}
}
