using UnityEngine;
using System.Collections;

public class TargetGenerator : MonoBehaviour {

	public Transform[] targets;
	public float delay;
	float time;

	void Start () {
		time = 0f;
	}

	void Update () {
		time += Time.deltaTime;
		if (time >= delay) {
			time = 0f;
			Instantiate (targets[Random.Range (0, targets.Length)], transform.position, Quaternion.identity);
		}
	}
}
