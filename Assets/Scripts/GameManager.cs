using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform ready;
	public Transform go;
	public Transform timeUp;

	public float moveSpeed;

	int stage = 1;

	void Start () {
		Invoke ("NextStage", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		if (stage == 1) {
			ready.position = Vector3.Lerp (ready.position, new Vector3 (0f, 0f, -9f), moveSpeed);
		} else if (stage == 2) {
			ready.position = Vector3.Lerp (ready.position, new Vector3 (-10f, 0f, -9f), moveSpeed);
			go.position = Vector3.Lerp (go.position, new Vector3 (0f, 0f, -9f), moveSpeed);
		} else if (stage == 3) {
			go.position = Vector3.Lerp (go.position, new Vector3 (0f, 10f, -9f), moveSpeed);
		} else if (stage == 4) {
			timeUp.position = Vector3.Lerp (timeUp.position, new Vector3 (0f, 0f, -9f), moveSpeed);
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	public void NextStage() {
		stage += 1;
		float wait = 2f;
		switch (stage) {
		case 1:
			wait = 2f;
			break;
		case 2:
			wait = 1f;
			break;
		case 3:
			wait = GameObject.Find ("GUIManager").GetComponent<GUIManager>().time;
			GameObject.Find ("GUIManager").GetComponent<GUIManager>().StartTimer ();
			break;
		}
		Invoke ("NextStage", wait);
	}
}
