using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	public string scene;

	void Update () {
		if (Input.GetMouseButtonUp (0))
			Application.LoadLevel (scene);
	}
}
