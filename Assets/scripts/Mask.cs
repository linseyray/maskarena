using UnityEngine;
using System.Collections;

public class Mask : MonoBehaviour{
	public enum TYPES {CHICKEN, SATAN, BOMB};

	public TYPES type;



	void Awake() {
		//TODO: Test code, remove after mask spawning is implemented
		this.type = TYPES.CHICKEN;
	}
	// type can be null atm
	// default fallback?
}
