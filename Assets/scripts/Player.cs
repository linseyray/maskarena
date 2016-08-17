﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private string name;
	private int number;
	public List<Color> colors;

	private PlayerMovement playerMovement;

	void Awake() {
		playerMovement = gameObject.GetComponent<PlayerMovement>();
	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetNumber(int number) {
		this.number = number;
		playerMovement.SetupMovementLabels(number);
		gameObject.GetComponent<SpriteRenderer>().color = colors[number-1];
	}
}
