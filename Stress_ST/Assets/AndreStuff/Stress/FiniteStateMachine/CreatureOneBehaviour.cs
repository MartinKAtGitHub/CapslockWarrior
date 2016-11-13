using UnityEngine;
using System.Collections;

public class CreatureOneBehaviour : MonoBehaviour {

//	private FSM_Manager MovementFSM, RadarFSM; // dette er Final State Machinene jeg bruker, en for bevegelse av hjulene/beltene og en for radaren. jeg lagde ikke en for kanonen siden den bare hadde en state
//	public EnemyData Enemy { get; set; }  // denne blir oppdatert i OnScannedRobot, altså når radaren har landet på en fiende. blir Cleared vær gjennomgang av gameloopen når ett target er funner ellers bare fornyet i OnScannedRobot.
//	double vinkelA = 0, vinkelB = 0, vinkelC = 0, hypotenus = 0;//disse blir bukt til å finne posisjon til fienden, tror bare navnene er forklarende nokk. matten er kanskje feil men det fungerer til linear targeting
//	double newX = 0, newY = 0, bulletpower = 0;//newX og newY er den nye posisjonen til fienden og bulletpower er hvilke kule man skal skyte med

	void Start(){
////		RadarFSM = new FSM_Manager( new DefaultState[] { new FindTarget(), } );//definerer alle statene for de spesifikke fsm'ene. og den første i dette tilfeller "FindTarget og AttackState" vil bli kalt først/default state
////		MovementFSM = new FSM_Manager( new DefaultState[] { new FindTarget(), } );
	
////		RadarFSM.Init(this);
////		MovementFSM.Init(this);
	}
	
	void Update(){
	//	while (true) {//game loop
			//		if (Enemy.Name != null) {//fyrer av kanonen når en fiende er funnet
			//			SetFire(bulletpower);
			//		}
////			MovementFSM.Update();//oppdaterer fsm'ene
////			RadarFSM.Update();
			//		Execute();
	//	}
	}
		
	//siden jeg ikke lagde en egen FSM for kanonen så la jeg den bare til i OnScannedRobot. de tre første linjene finner informasjonen som er nødvendig for å sette Enemy() og resten er å sette kulekraft og linear targeting
/*	public override void OnScannedRobot(ScannedRobotEvent scanData) {
		Vector2D offset = CalculateTargetVector(HeadingRadians, scanData.BearingRadians, scanData.Distance);//disse tre kalkulerer nødvendig data til enemy
		Point2D position = new Point2D(offset.X + X, offset.Y + Y);
		Enemy.SetEnemyData(scanData, position);

		if (scanData.Distance <= 350)//denne kan gjøres bedre men følte lengdene var helt greie. setter kulens kraft etter avstanden fra fienden
			bulletpower = 3;
		else if (scanData.Distance > 350 && scanData.Distance <= 650)
			bulletpower = 2;
		else
			bulletpower = 1;

		//vinkelA finner den nødvendige vinkelen for å treffe fienden og vinkelB og C er nødvendige for å finne hypotenus og de nye posisjonene
		vinkelA = Utils.NormalRelativeAngle(Utils.NormalRelativeAngle(Math.Sin(Enemy.HeadingRadians - RadarHeadingRadians)) * Enemy.Velocity / (18 - (3 * bulletpower))); //finner vinkelen som spiller kommer til å ha (linear targeting). denne fungerer alene til linear targeting men ville ha posisjonen så måtte gjøre det andre også
		vinkelB = (Math.PI - (Math.PI - (Math.PI * 0.5f) - ((Math.PI * 0.5f) - Utils.NormalRelativeAngle(RadarHeadingRadians))) + Utils.NormalRelativeAngle(Enemy.HeadingRadians));//finner vinkelB tror jeg =D testet masse og plutselig fikk jeg det jeg ønsket, kanskje ikke riktig matte messig men det fungerer  
		vinkelC = (Math.PI + vinkelA - vinkelB); //finner den siste vinkelen i trekanten
		hypotenus = (Enemy.Distance / Math.Sin(vinkelC)) * Math.Sin(vinkelB);//finner hypotenusen til trekanten

		newY = hypotenus * Math.Cos(RadarHeadingRadians + vinkelA) + Y;//finner den nye Y koordinaten
		newX = hypotenus * Math.Sin(RadarHeadingRadians + vinkelA) + X;//finner den nye X koordinaten

		if (newX < 18)//hvis den nye X koordinaten er større eller mindre enn kartet +- størrelse av robot setter den til passende størrelse
			newX = 18;
		else if (newX > BattleFieldWidth - 18)
			newX = BattleFieldWidth - 18;

		if (newY < 18)//hvis den nye Y koordinaten er større eller mindre enn kartet +- størrelse av robot setter den til passende størrelse
			newY = 18;
		else if (newY > BattleFieldHeight - 18)
			newY = BattleFieldHeight - 18;

		SetTurnGunRightRadians(Utils.NormalRelativeAngle(Math.Atan2(newX - X, newY - Y) - (GunHeadingRadians)));
	}*/

	private void InitBot() {//her blir fsmene initialisert og setter ny farge på robot 

	//	SetColors(Color.Black, Color.Gray, Color.Black, Color.Red, Color.Black);
	//	IsAdjustGunForRobotTurn = true;
	//	IsAdjustRadarForGunTurn = true;
	}

	/*private Vector2D CalculateTargetVector(double ownHeadingRadians, double bearingToTargetRadians, double distance) {//denne finner vectoren til fienden
		double battlefieldRelativeTargetAngleRadians = Utils.NormalRelativeAngle(ownHeadingRadians + bearingToTargetRadians);
		Vector2D targetVector = new Vector2D(Math.Sin(battlefieldRelativeTargetAngleRadians) * distance, Math.Cos(battlefieldRelativeTargetAngleRadians) * distance);
		return targetVector;
	}*/

/*	public override void OnRobotDeath(RobotDeathEvent deadRobot) {//hvis fienden jeg har merket dør blir informasjonen cleared
		if (deadRobot.Name == Enemy.Name) {
			Enemy.Clear();
		}
	}

	public override void OnBulletHit(BulletHitEvent hitData) {//når min kule treffer fienden blir energien til fienden oppdatert
		if (Enemy.Name == hitData.VictimName) {
			Enemy.Energy = hitData.VictimEnergy;
		}
	}*/
}
