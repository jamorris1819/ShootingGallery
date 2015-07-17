using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip shoot;
	public AudioClip quack;

	AudioSource audioSource;
	float timer;
	float timerLimit;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		timer = 0;
		timerLimit = Random.Range (4, 8);
	}

	void Update() {
		timer += Time.deltaTime;
		if (timer > timerLimit) {
			audioSource.PlayOneShot (quack);
			timer = 0;
			timerLimit = Random.Range (4, 8);
		}
	}

	public void PlayShoot() {
		audioSource.PlayOneShot (shoot);
	}
}
