using System.Collections;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] protected Player player;

    // ScriptedEvent CAM

    private void Awake()
    {
        playerInputManager = GameManager.Instance.PlayerInputManager;
    }

    public virtual void TriggerScriptedEvent()
    {
        DisablePlayerControlles(true);
        Debug.Log("Starting Intro cutscene");
        StartCoroutine(ScriptedEventScene());
        // DisablePlayerControlles(False); // Dose this line wait for scripted event. if so then i should wait
    }

    public virtual void StopScriptedEvent()
    {
        DisablePlayerControlles(false);
        StopCoroutine(ScriptedEventScene());
        Debug.Log("CutScene Skiped ---- What to do when you skipped ?");
    }


    private void DisablePlayerControlles(bool status)
    {
        playerInputManager.ScriptedEventActive = status;
    }

    public abstract IEnumerator ScriptedEventScene();

    //TODO check if i need anythign form the old ScriptedEvent SYSTEM
    #region OLDSYSTEM

    public delegate void OnScriptedEventEndDelegate();//TODO ScriptedEvent OnScriptedEventEndEvent sould be a Action or func
    public /*event*/ OnScriptedEventEndDelegate OnScriptedEventEndEvent;

    public abstract bool ScriptedEventEnd { get; set; }
    protected abstract void SetInitalRefs();

    //TODO In cut scene i just turn off all Componants(scripts), only igoring types maybe a better way
    protected virtual void AreComponentActiv(GameObject actorGameObject, bool status) // Can this be protected ?
    {
        foreach (MonoBehaviour Scripts in actorGameObject.GetComponents<MonoBehaviour>())
        {
            /*
			// If you ever need to turn of all but specific component ps: might not find componant
			if(Scripts.GetType() != gameObject.GetComponent<PlayerTyping>().GetType()) 
			{
				Scripts.enabled = false;
			}
			*/
            Scripts.enabled = status;
        }
    }
    //TODO In cut scene i just turn off all GOs, only igoring string "GFX" maybe a better way
    protected virtual void AreChildeGameObjectsActiv(GameObject actorGameObject, bool status, string ignore)
    {
        foreach (Transform Child in actorGameObject.transform)
        {

            // If you ever need to turn of all but specific component ps: might not find componant
            if (Child.gameObject.name != ignore)
            {
                Child.gameObject.SetActive(status);
            }

            //Child.gameObject.SetActive(status);
        }
    }


    #endregion
}
