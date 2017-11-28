using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCloneShadow : MonoBehaviour {


	public Vector2 offset;
	public Material ShadowMat;
	public Color ShadowColor;


	private SpriteRenderer mainSprite;
	private SpriteRenderer shadowCloneSprite;

	private GameObject MainSpriteObject;
	private GameObject ShadowCloneSpriteObject;

	void Start () 
	{
		MainSpriteObject = gameObject;

		ShadowCloneSpriteObject = new GameObject();
		ShadowCloneSpriteObject.transform.parent = MainSpriteObject.transform; 
		ShadowCloneSpriteObject.name = "Shadow";
		ShadowCloneSpriteObject.transform.localRotation = Quaternion.identity;

		mainSprite = GetComponent<SpriteRenderer>();
		shadowCloneSprite = ShadowCloneSpriteObject.AddComponent<SpriteRenderer>();


		shadowCloneSprite.material = ShadowMat;
		shadowCloneSprite.color = ShadowColor;


		shadowCloneSprite.sortingLayerName = mainSprite.sortingLayerName;
		shadowCloneSprite.sortingOrder = mainSprite.sortingOrder -1;
	}

	void LateUpdate()
	{
		ShadowCloneSpriteObject.transform.position = new Vector2(MainSpriteObject.transform.position.x + offset.x,
		 MainSpriteObject.transform.position.y + offset.y);
		shadowCloneSprite.sprite = mainSprite.sprite;

	}
}
