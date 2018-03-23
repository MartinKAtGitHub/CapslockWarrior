using UnityEngine;
using System.Collections;

public class SpawnHero : MonoBehaviour {

	public GameObject MainHero;


	public void SpawnSinglePlayerHero()
	{

		GameMasterSpellDistributer GM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterSpellDistributer>();
		//TODO CAN ADD LOADE DATE IN HERE MAYBE?? so when he spawns he also gets all his spells
		Instantiate(MainHero);

		GM.LoadDataToHeroOnSceneLoad();
	}

}
