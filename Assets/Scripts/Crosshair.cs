using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	public bool canShoot;

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.transform.name == "HitArea")
			canShoot = true;
	}
	
	void OnCollisionLeave2D(Collision2D collision) {
		if (collision.transform.name == "HitArea")
			canShoot = false;
	}
}
