using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityKeyDropZone : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    //Key ID so i can sort it in a list maybe
    public bool IsKeyOccupied;

    private Draggable curretnDraggable;
    public OrbSystemAbilityIcon orbMenuAbility;

    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log("ON DROP ZONE");

        if (!IsKeyOccupied)
        {
            curretnDraggable = eventData.pointerDrag.GetComponent<Draggable>();
            orbMenuAbility = eventData.pointerDrag.GetComponent<OrbSystemAbilityIcon>();

            if (curretnDraggable != null)
            {
                curretnDraggable.ResetDropZone = transform;
                curretnDraggable.OnDropZone = true;
                curretnDraggable.keyDropZone = this;
                IsKeyOccupied = true;

               // SetAbilityToKey();

                // curretnDraggable.transform.position = transform.position;
            }
        }
        else
        {
            Debug.Log("Ability Key is occupied  = " + name);
        }
    }
}
