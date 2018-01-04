
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadeNewSceneScript: MonoBehaviour{

	void LoadeNewSceneWithIndex(int index)
	{
		SceneManager.LoadScene(index);
	}
	void LoadeNewSceneWithString(string index)
	{
		SceneManager.LoadScene(index);
	}

	void LoadeNewSceneWithIndexAsync(int index)
	{
		SceneManager.LoadSceneAsync(index);
	}
	void LoadeNewSceneWithStringAsync(string index)
	{
		SceneManager.LoadSceneAsync(index);
	}
}
