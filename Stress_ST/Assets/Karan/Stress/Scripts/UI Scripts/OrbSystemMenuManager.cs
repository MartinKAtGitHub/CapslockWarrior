using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds All the Data about Orb System. 
/// </summary>
public class OrbSystemMenuManager : MonoBehaviour
{
    // public List<AbilityKeyDropZone> abilityKeyDropZones = new List<AbilityKeyDropZone>();
    [SerializeField]private GameObject orbMenuPnl;


    public AbilityKeyDropZone[] abilityKeyDropZones;

    public GameObject OrbMenuPnl { get => orbMenuPnl;}

    private void Awake()
    {
        abilityKeyDropZones = GetComponentsInChildren<AbilityKeyDropZone>();
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OrbMenu - OnSceneLoaded - " + scene.name);
        orbMenuPnl.SetActive(false);
    }
}
