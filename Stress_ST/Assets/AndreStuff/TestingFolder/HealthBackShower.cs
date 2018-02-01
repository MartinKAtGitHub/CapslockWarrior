using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBackShower : MonoBehaviour {

	public SpriteRenderer HealthSprite;
	public float StartXPos = 0.595f;
	public float StartXScale = 1;
	public float StartXWidt = 0.04f;

	float subtractionValue = 0;

	public Vector3 myScale = Vector3.zero;
public	Vector3 MyPosition = Vector3.zero;
	public 	Vector2 SpriteWidth = Vector2.zero;
	ObjectWords myinfo;

	public bool StandardHealth = false;
	float MaxHP = 0;
	float CurrentHP = 1;
	// Use this for initialization
	void Start () {

		myScale = HealthSprite.transform.localScale;
		subtractionValue = StartXWidt / StartXScale;
		MyPosition = HealthSprite.transform.localPosition;
		SpriteWidth = HealthSprite.size;
		myinfo = transform.parent.parent.GetComponent<ObjectWords> ();
		MaxHP = myinfo.HealthWords;
	}

	int CurrentColors = -1;

	public bool StartOnce = false;
	public int ColourBar = 0;
	public SpriteRenderer HealthSprite2;
	public Color[] TEST;

	void Update(){
	
	/*	if (StartOnce == true) {
		
			StartOnce = false;*/
			StartHealthChange ();
		
	//	}

	
	}

	public int howmanybeforefull = 0;
	int Overflowing = 0;

	public void StartHealthChange(){
	
		if (StandardHealth == false) {
			if (myinfo.HealthWords < 31) {//Goes to 31

				if (myScale.x != 1) {
					myScale.x = 1;
					HealthSprite.transform.localScale = myScale;	
				}

			} else if (myinfo.HealthWords > 30 && myinfo.HealthWords < 62) {//goes to 62

				if (myScale.x != 0.5f) {
					myScale.x = 0.5f;
					HealthSprite.transform.localScale = myScale;	
				}

			} else if ((myinfo.HealthWords > 61 && myinfo.HealthWords < 124)) {//goes to 124

				if (myScale.x != 0.25f) {
					myScale.x = 0.25f;
					HealthSprite.transform.localScale = myScale;	
				}
		
			} else if (myinfo.HealthWords > 123 && myinfo.HealthWords < 248) {//goes to 248

				if (myScale.x != 0.1275f) {
					myScale.x = 0.1275f;
					HealthSprite.transform.localScale = myScale;	
				}

			}



			SpriteWidth.x = (StartXWidt * (myinfo.HealthWords + 1));
			MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

			HealthSprite.size = SpriteWidth;
			HealthSprite.transform.localPosition = MyPosition;
		} else {

			if (ColourBar == 0) {
				
				if (MaxHP < myinfo.HealthWords + 1) {
					MaxHP = myinfo.HealthWords + 1;
					CurrentHP = 1;
				} else {
					CurrentHP = (myinfo.HealthWords + 1) / MaxHP;
				}

				SpriteWidth.x = (StartXWidt * (CurrentHP));
				HealthSprite.size = SpriteWidth;

			} else if(ColourBar == 1) {

				if (myinfo.HealthWords > -1) {
				
					CurrentColors = Mathf.FloorToInt ((myinfo.HealthWords + 1) / howmanybeforefull);
					Overflowing = Mathf.FloorToInt (CurrentColors / TEST.Length); 

					if (CurrentColors == 0) {
						HealthSprite.color = TEST [CurrentColors];

						if (HealthSprite2.enabled == true) {
							HealthSprite2.enabled = false;
						}
				
						SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite.size = SpriteWidth;
						HealthSprite.transform.localPosition = MyPosition;
				
					} else {
				
						if ((CurrentColors - (TEST.Length * Overflowing)) == 0) {
							HealthSprite.color = TEST [TEST.Length - 1];
							HealthSprite2.color = TEST [0];
						} else {
							HealthSprite.color = TEST [CurrentColors - 1 - (TEST.Length * Overflowing)];
							HealthSprite2.color = TEST [CurrentColors - (TEST.Length * Overflowing)];
						}

						if (HealthSprite2.enabled == false) {
							HealthSprite2.enabled = true;

							SpriteWidth.x = (StartXWidt * howmanybeforefull);
							MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

							HealthSprite.size = SpriteWidth;
							HealthSprite.transform.localPosition = MyPosition;

						}

						SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite2.size = SpriteWidth;
						HealthSprite2.transform.localPosition = MyPosition;

					}
				} else {
				
					gameObject.SetActive (false);

				}





			/*	if (myinfo.HealthWords < howmanybeforefull) {

					if (CurrentColors != 0) {
						CurrentColors = 0;
						HealthSprite.color = TEST [CurrentColors];
						HealthSprite2.enabled = false;
					}

					SpriteWidth.x = (StartXWidt * (myinfo.HealthWords + 1));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite.size = SpriteWidth;
					HealthSprite.transform.localPosition = MyPosition;

				} else if (myinfo.HealthWords > howmanybeforefull - 1 && myinfo.HealthWords < howmanybeforefull * 2) {

					if (CurrentColors != 1) {
						CurrentColors = 1;
						HealthSprite.color = TEST [CurrentColors - 1];
						HealthSprite2.enabled = true;
						HealthSprite2.color = TEST [CurrentColors];

						SpriteWidth.x = (StartXWidt * howmanybeforefull);
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite.size = SpriteWidth;
						HealthSprite.transform.localPosition = MyPosition;
					}

					SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite2.size = SpriteWidth;
					HealthSprite2.transform.localPosition = MyPosition;

				} else if (myinfo.HealthWords > howmanybeforefull * 2 - 1 && myinfo.HealthWords < howmanybeforefull * 3) {

					if (CurrentColors != 2) {
						CurrentColors = 2;
						HealthSprite.color = TEST [CurrentColors - 1];
						HealthSprite2.enabled = true;
						HealthSprite2.color = TEST [CurrentColors];

						SpriteWidth.x = (StartXWidt * howmanybeforefull);
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite.size = SpriteWidth;
						HealthSprite.transform.localPosition = MyPosition;
					}

					SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite2.size = SpriteWidth;
					HealthSprite2.transform.localPosition = MyPosition;

				} else if (myinfo.HealthWords > howmanybeforefull * 3 - 1 && myinfo.HealthWords < howmanybeforefull * 4) {

					if (CurrentColors != 3) {
						CurrentColors = 3;
						HealthSprite.color = TEST [CurrentColors - 1];
						HealthSprite2.enabled = true;
						HealthSprite2.color = TEST [CurrentColors];

						SpriteWidth.x = (StartXWidt * howmanybeforefull);
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite.size = SpriteWidth;
						HealthSprite.transform.localPosition = MyPosition;
					}

					SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite2.size = SpriteWidth;
					HealthSprite2.transform.localPosition = MyPosition;

				} else if (myinfo.HealthWords > howmanybeforefull * 4 - 1 && myinfo.HealthWords < howmanybeforefull * 5) {

					if (CurrentColors != 4) {
						CurrentColors = 4;
						HealthSprite.color = TEST [CurrentColors - 1];
						HealthSprite2.enabled = true;
						HealthSprite2.color = TEST [CurrentColors];

						SpriteWidth.x = (StartXWidt * howmanybeforefull);
						MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

						HealthSprite.size = SpriteWidth;
						HealthSprite.transform.localPosition = MyPosition;
					}

					SpriteWidth.x = (StartXWidt * ((myinfo.HealthWords + 1) - (howmanybeforefull * CurrentColors)));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite2.size = SpriteWidth;
					HealthSprite2.transform.localPosition = MyPosition;

				} */
			}
		}
	}

}
