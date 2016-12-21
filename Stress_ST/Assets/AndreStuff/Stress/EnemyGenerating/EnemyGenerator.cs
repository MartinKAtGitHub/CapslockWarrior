using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : ListOfWords {

	[SerializeField]
	List<Transform> _SpawnPoints;

	EnemyAttacher _TheEnemyAttacher;

	[SerializeField]
	GameObject _EnemyObject = null;

	[SerializeField]
	GameObject _SpawnTimerBar;

	[SerializeField]
	List<Sprite> _TimerBarSprites;


	[SerializeField]
	float TimeBetweenSpawn = 5;

	[SerializeField]
	float EnemySpawnDelay = 0.3f;

	[SerializeField]
	float TimeBetweenWaves = 20f;

	[SerializeField]
	float TimeToStart = 10f;

	Queue<KeyValuePair<string,int>> _SpawnList;
	KeyValuePair<string, int> _CurrentEnemyObjects;
	Queue<KeyValuePair<string,int>> _WaveListHolder;

	bool _StartSpawn = false;
	float _SpawnDelay;
	float _TimeSaver;
	int _WaveNumber = 0;

	void Start () {
		_TimeSaver = TimeToStart;
		_SpawnDelay = EnemySpawnDelay;
		_StartSpawn = false;

		_SpawnPoints = new List<Transform> ();
		_SpawnList = new Queue<KeyValuePair<string, int>> ();

		_TheEnemyAttacher = GetComponent<EnemyAttacher> ();
		FillSpawnList ();
		for (int i = 0; i < transform.childCount; i++) {
			_SpawnPoints.Add (transform.GetChild (i)); 
		}
	}

	void Update () {
		if (_TimeSaver <= 0) {
			if (_WaveListHolder.Count > 0) {//if there are enemies left to spawn then this happens
				SendObjectsToSpawnTimerBar ();
				_TimeSaver = TimeBetweenSpawn;
			} else {//if the list is empty then it searches for more, if its still empty then level is complete
				FillSpawnList ();
				if (_WaveListHolder.Count == 0) {
			//		Debug.Log ("Nothing More To Spawn");
				} else {
					_TimeSaver = TimeBetweenWaves;
				}
			}
		} else {
			_TimeSaver -= Time.smoothDeltaTime;
		}

		if (_StartSpawn == true) {//enemy spawner

			if (_SpawnDelay <= 0) {
				_SpawnDelay = EnemySpawnDelay;

				if (_SpawnList.Count == 0 && _CurrentEnemyObjects.Value == 0) {//list is empty -> stop
					_StartSpawn = false;
				} else {
					if (_CurrentEnemyObjects.Value <= 0) {//if all enemies from list[0] is spawned then it move to the next list[1] TODO make a check to see if all enemies from that wave is dead?
						_CurrentEnemyObjects = _SpawnList.Dequeue ();
					} else {//spawning enemie and setting info to them
						_CurrentEnemyObjects = new KeyValuePair<string,int> (_CurrentEnemyObjects.Key, _CurrentEnemyObjects.Value - 1);

						EnemyBlueprint middleMan = null;
					
						if (_CurrentEnemyObjects.Key == "easy") {
							middleMan = (_TheEnemyAttacher.GetEasyEnemies () as EnemyBlueprint);		
						} else if (_CurrentEnemyObjects.Key == "medium") {
							middleMan = (_TheEnemyAttacher.GetMediumEnemies () as EnemyBlueprint);		
						} else if (_CurrentEnemyObjects.Key == "hard") {
							middleMan = (_TheEnemyAttacher.GetHardEnemies () as EnemyBlueprint);		
						} else if (_CurrentEnemyObjects.Key == "miniboss") {
							middleMan = (_TheEnemyAttacher.GetMiniBossEnemies () as EnemyBlueprint);	
						} else if (_CurrentEnemyObjects.Key == "boss") {
							middleMan = (_TheEnemyAttacher.GetBossEnemies () as EnemyBlueprint);	
						}
						if (middleMan.GetEnemyHealth () > 0) {//moved this from enemywordchecker because thought it might improve the perfromance, then if objects health is 0 it wont be spawned
							_EnemyObject.GetComponentInChildren<EnemyWordChecker> ()._EnemyHealth = GetRandomWords (middleMan.GetEnemyHealth ());
							//_EnemyObject.GetComponentInChildren<SpriteRenderer> ().sprite = middleMan.GetEnemySprite ();
							_EnemyObject.GetComponentInChildren<Animator> ().runtimeAnimatorController = middleMan.GetEnemyAnimator ();
							WhereToSpawnEnemies ();
						} else {
						}
					}
				}
			} else {
				_SpawnDelay -= Time.smoothDeltaTime;
			}
		}
	}

	void FillSpawnList(){//get enemies to spawn
		if (_TheEnemyAttacher.GetEnemyWave (_WaveNumber) != null) {
			_WaveListHolder = new Queue<KeyValuePair<string, int>> (); 
			foreach (KeyValuePair<string,int> s in _TheEnemyAttacher.GetEnemyWave (_WaveNumber++)) {
				_WaveListHolder.Enqueue (s);
			}
		}
	}


	void WhereToSpawnEnemies(){//TODO improve this, currently spawning enemies from 4 different positions, random.range to choose which to spawn from

		Transform s = _SpawnPoints [Random.Range (0, _SpawnPoints.Count)];

		GameObject saver = Instantiate (_EnemyObject, s.position, Quaternion.identity) as GameObject;
		saver.transform.SetParent (s);
//		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<InTheMiddleManager>().AddObject(saver);
	}



	public void StartSpawning(KeyValuePair<string,int> EnemyInfo){
		_StartSpawn = true;
		_SpawnList.Enqueue (EnemyInfo);
	}
		
	void SendObjectsToSpawnTimerBar(){//instantiating an oject to the spawnbar, giving it the amount of enemies to spawn of each difficulty, so basically the timer is the distance the object has to travel :D 

		KeyValuePair<string,int> DifficultyAndNumber = _WaveListHolder.Dequeue ();
		
		GameObject InstatiatedObject = Instantiate (Resources.Load ("Andre/Prefabs/TimeBarObject", typeof(GameObject)) as GameObject, _SpawnTimerBar.transform.FindChild ("StartPosition").transform.position, Quaternion.identity) as GameObject;
		InstatiatedObject.transform.SetParent (_SpawnTimerBar.transform);
	
		InstatiatedObject.transform.localScale = new Vector3 (15,15,15);//for some reason the scale changes depending on how much the camera is zoomed in/out so had to do this. TODO find a solution
		InstatiatedObject.GetComponent<MoveToEnd> ().SetDifficultyAndNumbers (DifficultyAndNumber, this.gameObject);
		InstatiatedObject.GetComponent<MoveToEnd> ().ObjectSpeed *= Random.Range (0.5f, 2.0f);

		if (DifficultyAndNumber.Key == "easy") {//TODO Make enum for faster comparison
			InstatiatedObject.GetComponent<SpriteRenderer> ().sprite = _TimerBarSprites [0];
		}else if (DifficultyAndNumber.Key == "medium") {
			InstatiatedObject.GetComponent<SpriteRenderer> ().sprite = _TimerBarSprites [1];	
		}else if (DifficultyAndNumber.Key == "hard") {
			InstatiatedObject.GetComponent<SpriteRenderer> ().sprite = _TimerBarSprites [2];
		}else if (DifficultyAndNumber.Key == "miniboss") {
			InstatiatedObject.GetComponent<SpriteRenderer> ().sprite = _TimerBarSprites [3];
		}else if (DifficultyAndNumber.Key == "boss") {
			InstatiatedObject.GetComponent<SpriteRenderer> ().sprite = _TimerBarSprites [4];
		}
	}
}
