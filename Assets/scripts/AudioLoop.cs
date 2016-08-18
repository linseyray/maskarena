﻿using UnityEngine;
using System.Collections;

public class AudioLoop : MonoBehaviour {

	public AudioSource[] sources;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		for (int i = 0; i < transform.childCount; i++){
			GameObject child = transform.GetChild(i).gameObject;
			DontDestroyOnLoad(child);
		}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void mute (int index) {
		sources[index].mute = true;
	}
	public void unmute (int index) {
		sources[index].mute = false;
	}

}