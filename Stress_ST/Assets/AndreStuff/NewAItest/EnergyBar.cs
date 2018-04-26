using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script Gives The Creature More Energy.

//TODO Create Set Algorythms That Let Creatures Have EnergyRegen States. (Easy To Implement. It's Just A 1st World Problem :D) 
//One Creature Can Be Low On EnergyRegen And Suddenly Gain A Realy High Regen And Then Drop Down Again.
//One Might Be Constant
//One Might Be Abit Lower Then Constant And Have Some Small Energy Spikes

public class EnergyBar {

	public CreatureRoot myVariables;
	float TimeSaved = 0;

	public void RunEnergyBar(){//Just A Time Saver + If Current Time Is More Then Saved Time, Then The Creature Gain 1 Energy

		if (TimeSaved + (4 / myVariables.Stats.EnergyRegeneration) < ClockTest.TheTimes) {
		
			TimeSaved = ClockTest.TheTimes;
			myVariables.Stats.Energy += 1;
		}

	}

}
