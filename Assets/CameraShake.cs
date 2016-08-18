using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private float shakeAmount;
	private float shakeTime;

	private Vector3 originalPos;

	// Use this for initialization
	void Start () {
		originalPos = transform.localPosition;
	}

	public void startShake(float shakeTime, float shakeAmount) {
		this.shakeTime = shakeTime;
		this.shakeAmount = shakeAmount;
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeTime > 0) {
			Vector3 depth = new Vector3 (0, 0, -10);
			transform.localPosition = (Vector3)Random.insideUnitCircle * shakeAmount + depth;
			shakeTime -= Time.deltaTime;

		} else {
			shakeTime = 0.0f;
			transform.localPosition = originalPos;
		}	
	}
}
