﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private string name;
	private int number;
	public List<Color> colors;

	private PlayerMovement playerMovement;

	public float defaultMaskTimeout = 5.0f;
	private bool hasMask = false;
	private float maskTimeLeft;
	private Mask.TYPES maskType;

	void Awake() {
		playerMovement = gameObject.GetComponent<PlayerMovement>();
	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(hasMask){
			maskTimeLeft -= Time.deltaTime;
			if(maskTimeLeft < 0) {
				removeMask();
			}
		}

	}

	public void SetNumber(int number) {
		this.number = number;
		playerMovement.SetupMovementLabels(number);
		gameObject.GetComponent<SpriteRenderer>().color = colors[number-1];
	}

	public void ReceiveMask(Mask.TYPES type){
		this.maskType = type;
		maskTimeLeft = defaultMaskTimeout;
		// idea: if(type == Mask.TYPES.specialLongDurationMask) maskTimeLeft = 3*defaultMaskTimeout;
		// TODO: add mask sprite to player object
		hasMask = true;
	}

	private void removeMask(){
		hasMask = false;
		// TODO: remove mask sprite from player object
	}

	public void specialAction(){
		if(hasMask) {
			switch(maskType) {
			default:
				break;
			}
		}
		
	}
}
