using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Transform crosshair;
	public Transform gun;

	public Transform bulletHole;
	public Sprite bulletSprite;
	public Sprite emptyBulletSprite;

	public Transform bullet;
	public int bullets = 10;

	public Sprite scoreLabel;
	public Sprite colon;
	public Transform tenUp;
	public Transform twentyUp;
	public Sprite[] numbers;

	public AudioClip hitSound;

	AudioSource aSource;

	int score;
	int maxBullets;

	char[] charArray;

	void Start () {
		maxBullets = bullets;
		score = 0;
		charArray = score.ToString ().ToCharArray ();
		aSource = GetComponent<AudioSource> ();
	}

	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos = new Vector3 (mousePos.x, mousePos.y, -9f);
		crosshair.position = mousePos;
		gun.position = new Vector3 (mousePos.x + 0.7f, gun.position.y);

		if (Input.GetMouseButtonDown (0)) {
			mousePos = new Vector3 (mousePos.x, mousePos.y, 2.5f);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
			if(hit.collider != null){
				if(hit.collider.tag == "Target") {
					if(bullets != 0) {
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
						aSource.PlayOneShot (hitSound);
						if(name == "mallard 1") {
							mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
							mousePos = new Vector3 (mousePos.x, mousePos.y + 1f, -9f);
							Transform scoreFloat = Instantiate (tenUp, mousePos, Quaternion.identity) as Transform;
							scoreFloat.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-0.2f, 0.2f), 2f) * 10f);
							score += 10;
						}
						else if (name == "rubber duck"){
							mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
							mousePos = new Vector3 (mousePos.x, mousePos.y + 1f, -9f);
							Transform scoreFloat = Instantiate (twentyUp, mousePos, Quaternion.identity) as Transform;
							scoreFloat.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-0.2f, 0.2f), 2f) * 10f);
							score += 10;
						}
						charArray = score.ToString ().ToCharArray ();
					}
					else {

					}
				}
			}
		}
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, scoreLabel.texture.width, scoreLabel.texture.height), scoreLabel.texture);
		GUI.Label (new Rect (scoreLabel.texture.width - 7, 10, colon.texture.width, colon.texture.height), colon.texture);


		int tempWidth = 0;

		for (int i = 0; i < charArray.Length; i++) {
			if(i > 0) tempWidth += numbers[int.Parse (charArray[i - 1].ToString ())].texture.width * 2 / 5;
			GUI.Label (new Rect (135 + tempWidth, 13, 40, 30), numbers[int.Parse (charArray[i].ToString ())].texture);
		}

		for(int x = 0; x < maxBullets; x++)
			GUI.Label (new Rect (8 + x * (emptyBulletSprite.texture.width + 4), Screen.height - 40, emptyBulletSprite.texture.width, emptyBulletSprite.texture.height), emptyBulletSprite.texture);
		
		for(int x = 0; x < bullets; x++)
			GUI.Label (new Rect (8 + x * (bulletSprite.texture.width + 4), Screen.height - 40, bulletSprite.texture.width, bulletSprite.texture.height), bulletSprite.texture);
	}

}
