using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SendSpellToGM : MonoBehaviour {

	GameManagerSpellDistributer GameManger;

	Transform Key;
	string parentName;


	void Start()
	{
		GameManger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSpellDistributer>();
		if(GameManger == null)
		{
			Debug.LogError("DID NOT FIND GAMEMANGER");
		}else
		{
		Debug.Log("I founf the GM");
		}

		Key = transform.parent.parent;

		// HACK if parent the brancher under button both buttons will be clickable witch is somthing i dont want to do
		//parentName = transform.parent.parent.parent.name;
		//Debug.Log("paraent name is = " + parentName);
		Debug.Log(Key.name);
	}


	public void SendingSpellToGM(GameObject spell)
	{

	if(spell == null)
	{
		Debug.Log("NO SPELL TO SEND");
	}

		// FIXME check parent button to see TAG||NAME some kind of ID to check witch button it is

		switch (Key.name) {
		case("BtnBrancher Key1"):
			GameManger.SpellOnKeyOne = spell;
			Debug.Log("Sending = " + spell.name + " |To KEY1");
		break;

		case("BtnBrancher Key2"):
			GameManger.SpellOnKeyTwo = spell;
			Debug.Log("Sending = " + spell.name + " |To KEY2");
		break;

		case("BtnBrancher Key3"):
			GameManger.SpellOnKeyThree = spell;
			Debug.Log("Sending = " + spell.name + " |To KEY3");
		break;

		case("BtnBrancher Key4"):
			GameManger.SpellOnKeyFour = spell;
			Debug.Log("Sending = " + spell.name + " |To KEY4");
		break;

		default:
			Debug.LogError("Did not find Rigth name / Key");
			break;

		}

		/*if(transform.parent.parent.name == "BtnBrancher Key1")
		{
			GameManger.SpellOnKeyOne = spell;
			Debug.Log("Sending = " + spell.name + " |To KEY1");

		}*/
		//FIXME Check the rest of the buttons and set the

	}
}
