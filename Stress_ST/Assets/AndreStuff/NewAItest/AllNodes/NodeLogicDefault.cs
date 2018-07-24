using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogicDefault : MonoBehaviour {

	public enum testEnum {sko, tare, tau };
	public enum SpellDamageEnum { Fire, Physical, Magic, };
	public enum SpellStateEnum {Heal, HealOT, Damage, DamageOT, Buff, BuffOT, Debuff, DebuffOT};

	//Default Node Logic. To Do Something With This Is To Check The State Of The Node.   Am I Inside A Wall, If So What Action Can I Do While On This Node
	
	//OnEnter And OnExit Buffs From Nodes.     While On Node (Sand) Object Is Slowed.  While On Node (Lava) Take Ticking Dmg.


}
