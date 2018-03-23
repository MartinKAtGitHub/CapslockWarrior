using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneBtn: MonoBehaviour {

	public void ChangeScene( string _sceneName)
	{
		if(_sceneName == null)
		{
			Debug.LogError("Need to enter Scene Name ");
		}
		
		SceneManager.LoadScene(_sceneName);
	}	

}
