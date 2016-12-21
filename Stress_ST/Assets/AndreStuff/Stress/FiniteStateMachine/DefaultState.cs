using UnityEngine;
using System.Collections;


	public abstract class DefaultState {

		protected MonoBehaviour CreatureObject;//lar de som arver fra denne bruke MonoBehaviour, hvis de trenger det. for å forandre på objektets rotasjon,fart,osv

		public string Id { get; set; }//dette blir satt til den nye id'en til staten som arver denne klassen

		public DefaultState() {
			Id = "";
		}

		public virtual void Init(MonoBehaviour myRobot) {//her initialiseres Robot, som gjør det mulig å forandre på ting utenfor hoved klassen
			CreatureObject = myRobot;
		}

		public abstract string EnterState();//hva som skal skje når man først starter staten
		
		public abstract void ExitState ();//hva som skjer når man går ut av staten

		public abstract string ProcessState();//hva som skal skje vær oppdatering av game loopen 

		public float distanceToTurn = 50; //distanceToTurn er avstanden fra veggen som roboten skal begynne å snu på

		public bool AvoidTheWall() {//denne skjekker om robotens posisjon er mindre eller større enn ønsket posisjon, hvis det så skal avoidthewall kjøres
	//		if (CreatureObject.Y > CreatureObject.BattleFieldHeight - distanceToTurn || CreatureObject.Y < distanceToTurn || CreatureObject.X > CreatureObject.BattleFieldWidth - distanceToTurn || CreatureObject.X < distanceToTurn)
	//			return true;
			return false;
		}
}
