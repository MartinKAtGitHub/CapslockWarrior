using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBtnClickStartLevel : MonoBehaviour {

	public string LevelName;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void StartLevel()
	{
		SceneManager.LoadScene(LevelName);
		//SceneManager.LoadSceneAsync(LevelName);
	}

}
