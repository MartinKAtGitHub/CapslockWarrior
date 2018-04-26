using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureStats {
	
	public int Health = 1;
	public int Shield = 1;
	[HideInInspector]
	public int Energy = 0;

	public StressCommonlyUsedInfo.WordDifficulty WordDifficulty;
//	public StressEnums.WordDifficulty[] WordDifficultyArray;//Atm Not Implemented Just Thought About That It Might Be Desireble With More Choices

	public float Healing = 1;//When Getting Healed This Increases Healing
	public float Shielding = 1;//When Getting A Sheild This Increases The Shield Amount

	public int EnergyRegeneration = 1;
	public float Speed = 1f;
	public float Range = 1;

	/// <summary>
	/// Velocity Resistence Is A Precentage Of The Dmg/PushBack Value.
	/// </summary>
	public float VelocityResist = 0.25f;
	/// <summary>
	/// Velocity Absorb Is The Rest Of The Dmg/PushBack After Resistance Is Calculated And Subtracted Of That Value.
	/// </summary>
	public float VelocityAbsorb = 1;//Used For Creatures With High Resist To Nullify Low PushBack Abilities

	public float PhysicalResistence = 1;


/*	[Space(25)]
	[Header("Attack")]//Maybe Just Physical And Magical. But Since The Player Have Those 4 Elements Then It Might Work
	public float Earth = 1;
	public float Air = 1;
	public float Water = 1;
	public float Fire = 1;
	public float Physical = 1;

	[Space(25)]
	[Header("Resistance")]
	public float EarthResistence = 1;
	public float AirResistence = 1;
	public float WaterResistence = 1;
	public float FireResistence = 1;
	public float PhysicalResistence = 1;*/

	public bool TotalImmunity = false;//For Scinematics And Stuff


	public bool HealthImmunity = false;//??
	public bool VelocityImmunity = false;//??

}
