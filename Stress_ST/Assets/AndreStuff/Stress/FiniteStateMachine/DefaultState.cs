using UnityEngine;
using System.Collections;


public abstract class DefaultState {

		protected MonoBehaviour CreatureObject;//lar de som arver fra denne bruke MonoBehaviour, hvis de trenger det. for å forandre på objektets rotasjon,fart,osv

		public string Id { get; set; }//dette blir satt til den nye id'en til staten som arver denne klassen
		public string _ReturnState = "";
		public LayerMask _LineOfSight;
		public float _Range = 1;

		public DefaultState() {
			Id = "";
		}

		public virtual void Init(MonoBehaviour myRobot) {//her initialiseres Robot, som gjør det mulig å forandre på ting utenfor hoved klassen
			CreatureObject = myRobot;
		}

		public abstract string EnterState();//hva som skal skje når man først starter staten
		
		public abstract void ExitState ();//hva som skjer når man går ut av staten

		public abstract string ProcessState();//hva som skal skje vær oppdatering av game loopen 

}
