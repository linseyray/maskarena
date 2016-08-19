using UnityEngine;
using System.Collections;

public class AudioLoop : MonoBehaviour {
	public enum Tracks {
		BASS_GUITAR, MAIN_HARMONY, MAIN_THEME, DRUM_KIT, SINGLE_DRUM, LASER, CHOIR, DRIVING_DRUM
	}

	public AudioSource[] sources;
	private bool[] sourcesMuted;

	private void InitialiseSources() {
		sources = new AudioSource[8];
		sourcesMuted = new bool[8];
		for (int i = 0; i < 8; i++) {
			sources[i] = transform.FindChild("Track" + (i + 1)).GetComponent<AudioSource>();
			sourcesMuted [i] = false;
		}
	}

	// Use this for initialization
	void Awake () {
		InitialiseSources ();
		DontDestroyOnLoad(this);
	}
	void Start () {
		mute (Tracks.MAIN_THEME);
		mute (Tracks.DRIVING_DRUM);
		mute (Tracks.MAIN_HARMONY);
		mute (Tracks.LASER);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UnmuteRandomTrack() {
		ChangeStateOfRandomTrack (false);
	}

	public void MuteRandomTrack() {
		ChangeStateOfRandomTrack (true);
	}

	private void ChangeStateOfRandomTrack(bool mute) {
		int counter = 0;
		while (true) {
			int rand = Random.Range (0, sources.Length - 1);
			if (sources [rand].mute != mute) {
				sources [rand].mute = mute;
				return;
			}
			counter += 1;
			if (counter > 3) {
				return;
			}
		}
	}

	public void mute (Tracks index) {
		mute ((int)index);
	}

	public void unmute (Tracks index) {
		unmute ((int)index);
	}

	public void mute (int index) {
		sources[index].mute = true;
	}
	public void unmute (int index) {
		sources[index].mute = false;
	}
}
