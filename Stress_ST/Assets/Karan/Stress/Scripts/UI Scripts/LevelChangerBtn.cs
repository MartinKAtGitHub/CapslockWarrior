using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Holds the Data on which scene to loade and Activates the OrbMenu for Players to change their setup
/// </summary>
public class LevelChangerBtn : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private string sceneName;

    private OrbSystemMenuManager orbSystemMenu;
    private GameManager_LoadLevel levelLoader;

    void Start()
    {
       
        orbSystemMenu = GameManager.Instance.OrbSystemMenuManager;
        levelLoader = GameManager.Instance.Manager_LoadLevel;

        if (sceneName == null)
        {
            Debug.LogError("Missing Scene Index  on " + name + "--> Need Index to loade level ");
        }
        if(orbSystemMenu == null)
        {
            Debug.LogError("OrbSystemMenuManager IS NULL on = " + name);
        }
    }

    public void OpenOrbMenuWindow()
    {
        orbSystemMenu.OrbMenuPnl.SetActive(true);
        //orbSystemMenu.OpenWindow(); // maybe this, i can even call events from this if need be
        levelLoader.SceneIndex = sceneIndex;
    }

    private void GetAllScenes()
    {
        List<string> scenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                scenes.Add(scene.path);
        }
    }
}
