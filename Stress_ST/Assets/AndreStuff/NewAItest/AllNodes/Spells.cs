using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	

	public virtual void ApplySpellEffect() {

	}

	public virtual void ApplyTileEffect() {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
	}

}
