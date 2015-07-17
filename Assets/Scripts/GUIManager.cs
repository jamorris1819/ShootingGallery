using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Sprite scoreLabel;
	public Sprite colon;
	public Transform tenUp;
	public Transform twentyUp;
	public Sprite[] numbers;
	public Sprite bulletSprite;
	public Sprite emptyBulletSprite;

	public int time;

	Gun gun;

	int score;
	float dtime;
	bool started = false;

	char[] charArray;
	char[] timeArray;

	void Start () {
		score = 0;
		dtime = time;
		charArray = score.ToString ().ToCharArray ();
		timeArray = time.ToString ().ToCharArray ();
		gun = GameObject.Find ("Crosshair").GetComponent<Gun> ();
	}

	void Update() {
		if (started) {
			dtime -= Time.deltaTime;
			time = Mathf.RoundToInt (dtime);
			timeArray = time.ToString ().ToCharArray ();
			if(time == 0) {
				started = false;
				GameObject.Find ("Crosshair").GetComponent<Gun>().InvokeRestart ();
			}
		}
	}

	public bool InGame {
		get { return (started && time >= 0); }
	}

	public void StartTimer() {
		started = true;
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, scoreLabel.texture.width, scoreLabel.texture.height), scoreLabel.texture);
		GUI.Label (new Rect (scoreLabel.texture.width - 7, 10, colon.texture.width, colon.texture.height), colon.texture);
		
		int tempWidth = 0;
		
		for (int i = 0; i < charArray.Length; i++) {
			if(i > 0) tempWidth += numbers[int.Parse (charArray[i - 1].ToString ())].texture.width - 7;
			Sprite charSprite = numbers[int.Parse (charArray[i].ToString ())];
			GUI.Label (new Rect (135 + tempWidth, 12, charSprite.texture.width, charSprite.texture.height), charSprite.texture);
		}

		tempWidth = 0;
		
		for (int i = 0; i < timeArray.Length; i++) {
			if(i > 0) tempWidth += numbers[int.Parse (timeArray[i - 1].ToString ())].texture.width - 7;
			Sprite charSprite = numbers[int.Parse (timeArray[i].ToString ())];
			GUI.Label (new Rect (Screen.width - 50 + tempWidth, 12, charSprite.texture.width, charSprite.texture.height), charSprite.texture);
		}
		
		for(int x = 0; x < gun.MaxBullets; x++)
			GUI.Label (new Rect (8 + x * (emptyBulletSprite.texture.width + 4), Screen.height - 40, emptyBulletSprite.texture.width, emptyBulletSprite.texture.height), emptyBulletSprite.texture);
		
		for(int x = 0; x < gun.bullets; x++)
			GUI.Label (new Rect (8 + x * (bulletSprite.texture.width + 4), Screen.height - 40, bulletSprite.texture.width, bulletSprite.texture.height), bulletSprite.texture);
	}

	public void AddScore(int addScore) {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos = new Vector3 (mousePos.x, mousePos.y + 1f, -9f);
		score += addScore;
		if (addScore == 10) {
			Transform scoreFloat = Instantiate (tenUp, mousePos, Quaternion.identity) as Transform;
			scoreFloat.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-0.2f, 0.2f), 2f) * 10f);
		} else if (addScore == 20) {
			Transform scoreFloat = Instantiate (twentyUp, mousePos, Quaternion.identity) as Transform;
			scoreFloat.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range (-0.2f, 0.2f), 2f) * 10f);
		}
		charArray = score.ToString ().ToCharArray ();
	}
}
