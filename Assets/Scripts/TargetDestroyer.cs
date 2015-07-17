using UnityEngine;
using System.Collections;

public class TargetDestroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Target") {
			GameObject.Destroy (other.gameObject);
		}
	}
}
