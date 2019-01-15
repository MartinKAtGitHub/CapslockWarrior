using UnityEngine;

/// <summary>
/// Holds All the Data about Orb System. 
/// </summary>
public class OrbSystemMenuManager : MonoBehaviour
{
    // public List<AbilityKeyDropZone> abilityKeyDropZones = new List<AbilityKeyDropZone>();
    public AbilityKeyDropZone[] abilityKeyDropZones;

    private void Awake()
    {
        // Populate all 

        abilityKeyDropZones = GetComponentsInChildren<AbilityKeyDropZone>();

        
    }
}
