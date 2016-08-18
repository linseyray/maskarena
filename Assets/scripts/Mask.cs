using UnityEngine;

public class Mask : MonoBehaviour{
	public enum TYPES {type1, type2};
	public TYPES type {
		get { return(this.type);}
		set { this.type = value;}
	}
	// type can be null atm
	// default fallback?
}
