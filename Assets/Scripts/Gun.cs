using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Transform bulletHole;
	public Transform bullet;
	public int bullets = 20;

	int maxBullets;
	GUIManager guiManager;
	AudioManager audioManager;

	void Start () {
		maxBullets = bullets;
		guiManager = GameObject.Find ("GUIManager").GetComponent<GUIManager> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}

	public int MaxBullets {
		get { return maxBullets; }
	}

	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos = new Vector3 (mousePos.x, mousePos.y, -9f);
		transform.position = mousePos;
		Transform gun = GameObject.Find ("Gun").transform;
		gun.position = new Vector3 (mousePos.x + 0.7f, gun.position.y);

		if(Input.GetMouseButtonDown (0)) {
			mousePos = new Vector3 (mousePos.x, mousePos.y, 2.5f);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
			if(hit.collider != null) {
				if(hit.collider.tag == "Target"){
					if(GameObject.Find ("GUIManager").GetComponent<GUIManager>().InGame){
						if(bullets > 0) {
							// Play shoot sound
							audioManager.PlayShoot ();

							// Create bullet mark
							Transform bulletMark = Instantiate (bulletHole, mousePos, Quaternion.identity) as Transform;
							bulletMark.parent = hit.collider.transform;
							Vector3 newPos = hit.collider.transform.InverseTransformPoint (mousePos);
							bulletMark.localPosition = new Vector3(newPos.x, newPos.y, 0f);

							// Make target spin
							hit.collider.transform.GetComponent<Animator>().SetTrigger(hit.collider.transform.GetComponent<Target>().name);

							// Throw bullets
							Vector3 bulletPos = gun.position + new Vector3(0.4f, -0.6f);
							Transform bulletCase = Instantiate(bullet, bulletPos, Quaternion.identity) as Transform;
							bulletCase.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 1f) * 300f);
							bulletCase.GetComponent<Rigidbody2D>().AddTorque(-500f);
							bullets -= 1;

							//Add points
							string name = hit.collider.transform.GetComponent<Target>().name;
							mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
							mousePos = new Vector3 (mousePos.x, mousePos.y + 1f, -9f);
							if(name == "mallard 1")
								guiManager.AddScore(10);
							else if(name == "rubber duck")
								guiManager.AddScore(20);
						}
					}
				}
			}
		}
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(4f);
		Application.LoadLevel ("Restart");
	}

	public void InvokeRestart() {
		StartCoroutine ("Restart");
	}
}
