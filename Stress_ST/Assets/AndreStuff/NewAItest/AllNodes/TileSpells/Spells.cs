using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	

	public virtual void ApplySpellEffect(TestWalkScript target) {

	}

	public virtual void ApplyTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
	}

	public virtual void RemoveTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
	}

}
