using UnityEngine;
using System.Collections;

public class FSM_Manager {

		readonly DefaultState[] _EveryStateToUse;//alle statene som denne FSM'en skal bytte mellom
		DefaultState _StateToRun;//den nye staten som skal kjøres neste runde
		DefaultState _CurrentRunningState;//den staten som kjøres nå
		string _StateId;//blir brukt til å se om staten skal oppdateres


		public FSM_Manager(DefaultState[] statesToUse) {//her blir alle statens som blir sendt inn lagt til i everyStateToUse
			_EveryStateToUse = statesToUse;
		}

		public void Init(MonoBehaviour myRobot) {//her initialiseres alle statene og setter hvilke state som skal gjøres til den første i rekken
			foreach (DefaultState state in _EveryStateToUse) {
				state.Init(myRobot);
			}
			_CurrentRunningState = _EveryStateToUse[0];
			_StateToRun = _EveryStateToUse[0];
			_CurrentRunningState.EnterState();//og starter den første statens Enterstate()
		}

		public string GetCurrentStateId() {//hvis kalt gir denne id'en til den staten som kjøres nå
			return _CurrentRunningState.Id;
		}

		public void TheNextState(string stateId) {//skjekker om stateId'en finner i noen av statenes Id, hvis det setter den nye staten til det stateId er
			foreach (DefaultState element in _EveryStateToUse) {
				if (stateId == element.Id) {
					_StateToRun = element;
					break;
				}
			}
		}

		public void Update() {//denne metoden er den jeg forandret mest på. kanskje ikke til det bedre men følte det passet bedre til det jeg prøvde å få til

			if (_CurrentRunningState.Id != _StateToRun.Id) {//denne skjekker om den staten som skal testen er lik den staten som kjøres, hvis ikke så blir kjørende state satt til staten som skulle testes
				_CurrentRunningState.ExitState();
				_CurrentRunningState = _StateToRun;
				_CurrentRunningState.EnterState();
			}

			_StateId = _CurrentRunningState.ProcessState();//her kjøres processstate i det kjørende staten. når processstaten er ferdig returnerer den null hvis ingen ting skal forandres, hvis ikke så skal staten byttes

			if (_StateId != null) {//hvis currentRunningState.ProcessState(); ikke returnerte null skal den neste statens som skal testes bli statId
				TheNextState(_StateId);
			}
		}
}