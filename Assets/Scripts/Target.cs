using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
	public float speed = 4f;
	public new string name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
	}
}
