using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tile", menuName = "NewTile")]
public class TileObjects : ScriptableObject {

	[Tooltip("Tile ID:\n 0 - /*TODO All Tiles +1 Or Combine All Similar Tiles*/")]
	public int TileID = 0;
	public float TileWalkCost = 1;

	[Space(10)]
	public Spells TheSpell;

	[Tooltip("Values Are Different For EverySpell. To Know Which Order To Have Said Value, You Simply Have To Go To The (TheSpell.GetComponent<ChildSpell>() -> Like FireBall, FireDot ... Anything That Might Inherit From Spells) Script And Look Where The Values Are Used")]
	public float[] SpellIncreases;

	public bool ApplyOnEnter = false;
	[Tooltip("Some Over Time Effects Will Remove ThemSelves What Effect Time Is Over. If You Want To Remove On Exit Then -> True")]
	public bool RemoveOnExit = false;







	public void OnEnter(TestWalkScript enteringObject) {//What Entering A Tile, The Tile Can Have Effects On It, Like Lava -> Burning Spell.    Quicksand -> Slowing Spell.    ......
		if (ApplyOnEnter == true) {
			if (TheSpell != null) {
				TheSpell.ApplyTileEffect(enteringObject);
			} else {
				Debug.Log("Spell Missing");
			}
		}
	}

	public void OnExit(TestWalkScript exitingObject) {//What To Do When Exiting
		if (RemoveOnExit == true) {
			if (TheSpell != null) {
				TheSpell.RemoveTileEffect(exitingObject);
			} else {
				Debug.Log("Spell Missing");
			}
		}
	}

}
