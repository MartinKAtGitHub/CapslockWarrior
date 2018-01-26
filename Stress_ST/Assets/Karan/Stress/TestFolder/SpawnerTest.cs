using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.IO;

public enum Enemies {Creep, Normal, Fast, Ranged}

[System.Serializable]
public class Wave
{
	public string name;
	public int amount = 1;
	public Enemies EnemyUnit;
}


public class SpawnerTest : MonoBehaviour {


	//public Wave test;
	public Wave[] Group1;
	public Wave[] Group2;
	// Use this for initialization
	void Start () 
	{
	Wave[][] FinalWave = new Wave[][] {Group1, Group2};

		//FinalWave = new Wave[][] {Group[0], Group[1]};
		for (int i = 0; i < FinalWave.Length; i++) {
			for(int j = 0; j < FinalWave[i].Length; j++){
				Debug.Log( FinalWave[i][j].name);
				Debug.Log( FinalWave[i][j].EnemyUnit);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}

/*public class TEST:EditorWindow
{
	[MenuItem("Window/WaveCreator")]

	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(TEST));
	}
	void OnGUI()
	{
		
	}
}*/

