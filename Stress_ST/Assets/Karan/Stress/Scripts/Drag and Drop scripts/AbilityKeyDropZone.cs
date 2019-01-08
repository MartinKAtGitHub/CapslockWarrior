using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityKeyDropZone : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsKeyOccupied;

    private Draggable curretnDraggable;
    private OrbMenuAbilityElement orbMenuAbilityElement;
    private AbilityActivation ability;

    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log("ON DROP ZONE");

        if (!IsKeyOccupied)
        {
            curretnDraggable = eventData.pointerDrag.GetComponent<Draggable>();
            orbMenuAbilityElement = eventData.pointerDrag.GetComponent<OrbMenuAbilityElement>();

            if (curretnDraggable != null)
            {
                curretnDraggable.ResetDropZone = transform;
                curretnDraggable.OnDropZone = true;
                curretnDraggable.keyDropZone = this;
                IsKeyOccupied = true;

               // SetAbilityToKey();

                // curretnDraggable.transform.position = transform.position;
            }
            if (orbMenuAbilityElement != null)
            {
                ability = orbMenuAbilityElement.Ability;
            }

        }
        else
        {
            Debug.Log("Ability Key is occupied  = " + name);
        }
    }


    private void SetAbilityToKey()
    {
      //  onKeyAbility = curretnDraggable.Ability;
        // Set img icon
        // Send to AB controller 
    }
}
