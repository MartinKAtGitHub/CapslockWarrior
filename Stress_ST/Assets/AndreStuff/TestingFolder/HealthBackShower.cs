using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBackShower : MonoBehaviour {//All Types Are Working Atm So For Better Performance Take Away The Other That Isnt Used
	public enum HealthDisplayType {CurrentEndabled, Testing1, Testing2, Testing3};
	public HealthDisplayType WhichDisplayType;

	public SpriteRenderer HealthSprite;
	public SpriteRenderer HealthSprite2;

	public float StartXPos = 0.595f;
	public float StartXScale = 1;
	public float StartXWidt = 0.04f;
	public int howmanybeforefull = 0;

	public Color[] TEST;

	public int ShieldPower = 0;
	public bool ActivateShields = false;
	public float DistanceBetween = 1;

	Vector3 myScale = Vector3.zero;
	Vector3 MyPosition = Vector3.zero;
	Vector2 SpriteWidth = Vector2.zero;
	Vector2 ShieldWidth = Vector2.zero;
	Vector2 ShieldXPos = Vector2.zero;

	CreatureRoot myinfo;

	float MaxHP = 0;
	float CurrentHP = 1;
	int Overflowing = 0;
	int CurrentColors = -1;



	void Start () {

		myinfo = transform.parent.parent.GetComponent<CreatureRoot> ();
		myScale = HealthSprite.transform.localScale;
		MyPosition = HealthSprite.transform.localPosition;
		SpriteWidth = HealthSprite.size;

		if (WhichDisplayType == HealthDisplayType.Testing1 || WhichDisplayType == HealthDisplayType.Testing3) {		
		} else {
			HealthSprite2.enabled = false;

			MaxHP = myinfo.Stats.Health;
			ShieldXPos = HealthSprite2.transform.localPosition;
			ShieldWidth = HealthSprite2.size;
		}
	}


	void Update(){

		StartHealthChange ();

	}





	public void StartHealthChange(){

		if (WhichDisplayType == HealthDisplayType.CurrentEndabled) {
			if (SpriteWidth.x > ((StartXPos * 2) + (StartXWidt * 1.5f))) {
				SpriteWidth.x = (StartXWidt * ((myinfo.Stats.Health )));
				MyPosition.x = 0;

			} else {
				SpriteWidth.x = (StartXWidt * ((myinfo.Stats.Health)));
				MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

			}

			HealthSprite.size = SpriteWidth;
			HealthSprite.transform.localPosition = MyPosition;

			if (ActivateShields == true) {
		
				if (HealthSprite2.enabled == false)
					HealthSprite2.enabled = true;

				if (SpriteWidth.x > ((StartXPos * 2) + (StartXWidt * 1.5f))) {
					ShieldXPos.x = (SpriteWidth.x / -2) - (StartXWidt * 0.5f) - ((myinfo.Stats.Shield * StartXWidt) * 0.5f);
				} else {
					ShieldXPos.x = StartXPos - SpriteWidth.x - (StartXWidt / DistanceBetween) - ((myinfo.Stats.Shield * StartXWidt) * 0.5f);
				}

				ShieldWidth.x = StartXWidt * myinfo.Stats.Shield;

				HealthSprite2.size = ShieldWidth;
				HealthSprite2.transform.localPosition = ShieldXPos;

			} else {
		
				if (HealthSprite2.enabled == true)
					HealthSprite2.enabled = false;

			}
		} else if (WhichDisplayType == HealthDisplayType.Testing1) {

			if (myinfo.Stats.Health < 31) {//Goes to 31

				if (myScale.x != 1) {
					myScale.x = 1;
					HealthSprite.transform.localScale = myScale;	
				}

			} else if (myinfo.Stats.Health > 30 && myinfo.Stats.Health < 62) {//goes to 62

				if (myScale.x != 0.5f) {
					myScale.x = 0.5f;
					HealthSprite.transform.localScale = myScale;	
				}

			} else if ((myinfo.Stats.Health > 61 && myinfo.Stats.Health < 124)) {//goes to 124

				if (myScale.x != 0.25f) {
					myScale.x = 0.25f;
					HealthSprite.transform.localScale = myScale;	
				}

			} else if (myinfo.Stats.Health > 123 && myinfo.Stats.Health < 248) {//goes to 248

				if (myScale.x != 0.1275f) {
					myScale.x = 0.1275f;
					HealthSprite.transform.localScale = myScale;	
				}

			}

			SpriteWidth.x = (StartXWidt * (myinfo.Stats.Health ));
			MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

			HealthSprite.size = SpriteWidth;
			HealthSprite.transform.localPosition = MyPosition;

		} else if (WhichDisplayType == HealthDisplayType.Testing2) {

			if (myinfo.Stats.Health > -1) {

				CurrentColors = Mathf.FloorToInt ((myinfo.Stats.Health ) / howmanybeforefull);
				Overflowing = Mathf.FloorToInt (CurrentColors / TEST.Length); 

				if (CurrentColors == 0) {
					HealthSprite.color = TEST [CurrentColors];

					if (HealthSprite2.enabled == true) {
						HealthSprite2.enabled = false;
					}

					SpriteWidth.x = (StartXWidt * ((myinfo.Stats.Health ) - (howmanybeforefull * CurrentColors)));
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

					SpriteWidth.x = (StartXWidt * ((myinfo.Stats.Health ) - (howmanybeforefull * CurrentColors)));
					MyPosition.x = StartXPos - (SpriteWidth.x * (myScale.x / 2)) + (StartXWidt * (myScale.x / (2 * myScale.x)));

					HealthSprite2.size = SpriteWidth;
					HealthSprite2.transform.localPosition = MyPosition;

				}
			} else {

				gameObject.SetActive (false);

			}

		} else if (WhichDisplayType == HealthDisplayType.Testing3) {
	
			if (MaxHP < myinfo.Stats.Health ) {
				MaxHP = myinfo.Stats.Health ;
				CurrentHP = 1;
			} else {
				CurrentHP = (myinfo.Stats.Health ) / MaxHP;
			}

			SpriteWidth.x = (StartXWidt * (CurrentHP));
			HealthSprite.size = SpriteWidth;

		}
	}
}