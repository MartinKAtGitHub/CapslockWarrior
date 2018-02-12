using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Events;

public class AbilityInfo : MonoBehaviour {

	[System.Serializable]
	public struct spellnumbers{

		[HeaderAttribute("Spell Criteria To Enter")]
		public UnityEvent CriteriaToBeMet;

		[HeaderAttribute("Spell Needed Numbers")]
		public float[] floatNumbers;
		public int[] intNmbers;
		public Vector3[] vector3Numbers;
	}





	public spellnumbers[] test11;
	public SpellCriteria test12;

	[Space(10)]
	public EnemyManaging MyManager;

	public float SpellCD = 10;
	public float SpellCurrentCD = 10;
	public float SpellCost = 4;

	public delegate void FiringDelegate();
	FiringDelegate firingMethod;

	public void test1 (float a){}
	public void test2 (){}

	public float GetCurrentTime (){
		return SpellCurrentCD;
	}

	public void SetCurrentTime (float val){
		SpellCurrentCD = val + SpellCD;
	}



	public SpriteRenderer FillingBar;
	public float FillingSpeed = 1;
	public float EnergyMax = 10;
	public float CurrentEnergy = 0;

	float FillingMaxSize = 0;
	Vector2 FillingSize = Vector2.zero;
	public bool StartFill = true;
	public bool PlayOnce = false;
	public float AnimatorStageSetTo = 0;

	// Use this for initialization
	void Start () {
		SpellCurrentCD = SpellCD;
		FillingSize = FillingBar.size;
		FillingMaxSize = FillingBar.size.x;
		FillingBar.size = FillingSize;
	}

	// Update is called once per frame
	bool ResetedOnce = false;
	public bool StartFilling = false;

	public bool RunSpellCheck(){

		//Logic
		//if true
		StartFilling = true;
		return true;
	}




	public bool CheckIfCriteriasMet(){
		//MethodsToRun.Invoke ();
		
		return false;
	}



	public void StartingAbilityLogic(){
		
		MyManager.MyAnimator.SetFloat ("AnimatorStage", AnimatorStageSetTo);

	}












}
// 1st iteration ability pseudocode logic
/*
 	public float SpellCD = 10;
	public float SpellCurrentCD = 10;
	public float SpellCost = 4;

	public float GetCurrentTime (){
		return SpellCurrentCD;
	}

	public void SetCurrentTime (float val){
		SpellCurrentCD = val + SpellCD;
	}



	public SpriteRenderer FillingBar;
	public float FillingSpeed = 1;
	public float EnergyMax = 10;
	public float CurrentEnergy = 0;

	float FillingMaxSize = 0;
	Vector2 FillingSize = Vector2.zero;
	public bool StartFill = true;
	public bool PlayOnce = false;

	// Use this for initialization
	void Start () {
		SpellCurrentCD = SpellCD;
		FillingSize = FillingBar.size;
		FillingMaxSize = FillingBar.size.x;
		FillingBar.size = FillingSize;
	}

	// Update is called once per frame
	bool ResetedOnce = false;
	public bool StartFilling = false;

	public bool RunSpellCheck(){

		//Logic

		//if true
		StartFilling = true;
		return true;
	}



	void Update () {

		if (StartFilling == true) {
			if (ResetedOnce == true) {
				if (FillingSize.x >= FillingMaxSize) {
					StartFilling = false;
					ResetedOnce = false;
					FillingSize.x = FillingMaxSize;
					FillingBar.size = FillingSize;
				} else {

					FillingSize.x += Time.deltaTime * FillingSpeed;
					FillingBar.size = FillingSize;

				}

			} else {

				if (FillingSize.x >= FillingMaxSize) {

					FillingSize.x = 0;
					if (CurrentEnergy < EnergyMax)
						CurrentEnergy++;
					ResetedOnce = true;
				}

				FillingSize.x += Time.deltaTime * FillingSpeed;
				FillingBar.size = FillingSize;

			}
		}
	}
 */