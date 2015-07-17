using UnityEngine;
using System.Collections;

public class Sway : MonoBehaviour {
	
	public float speed;
	public float floatSpan;
	public bool lockY;

	float initx;
	float inity;

	void Start () {
		initx = transform.position.x;
		inity = transform.position.y;
	}

	void Update () {
		float x = initx - Mathf.Cos (Time.time * speed) * floatSpan / 2.0f;
		float y = inity;
		if(!lockY) y = inity + Mathf.Sin (Time.time * speed) * floatSpan / 2.0f;
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
